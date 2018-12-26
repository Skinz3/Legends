using Legends.Core;
using Legends.Core.DesignPattern;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class Spell : IUpdatable
    {
        static Logger logger = new Logger();

        public const byte NORMAL_SPELL_LEVELS = 5;

        public const byte ULTIMATE_SPELL_LEVELS = 3;

        public SpellRecord Record
        {
            get;
            private set;
        }
        private byte Slot
        {
            get;
            set;
        }
        public SpellStateEnum State
        {
            get;
            private set;
        }
        public AIUnit Owner
        {
            get;
            private set;
        }
        public byte Level
        {
            get;
            private set;
        }
        private SpellScript Script
        {
            get;
            set;
        }
        private UpdateTimer ChannelTimer;
        private UpdateTimer CooldownTimer;

        private Vector2 castPosition;
        private Vector2 castEndPosition;

        private AttackableUnit autoAttackTarget;

        public Spell(AIUnit owner, SpellRecord record, byte slot, SpellScript script)
        {
            this.Owner = owner;
            this.Record = record;
            this.Slot = slot;
            this.Script = script;
            this.Script?.Bind(this);
            this.State = SpellStateEnum.STATE_READY;
        }
        [InDevelopment(InDevelopmentState.STARTED, "Slot max")]
        public bool Upgrade(byte id)
        {
            // 3 = Ultimate Spell.
            byte maxLevel = id == 3 ? ULTIMATE_SPELL_LEVELS : NORMAL_SPELL_LEVELS;

            if (Level + 1 <= maxLevel)
            {
                Level++;
                this.Owner.Stats.SkillPoints--;
                return true;
            }
            return false;
        }
        public void LowerCooldown(float value)
        {
            if (State == SpellStateEnum.STATE_COOLDOWN)
            {
                CooldownTimer.Current -= value;

                if (CooldownTimer.Current <= 0)
                {
                    CooldownTimer = null;
                    State = SpellStateEnum.STATE_READY;
                    Owner.Team.Send(new SetCooldownMessage(Owner.NetId, Slot, 0f, GetTotalCooldown()));
                }
                else
                {
                    Owner.Team.Send(new SetCooldownMessage(Owner.NetId, Slot, CooldownTimer.Current, GetTotalCooldown()));
                }
            }
        }
        private void UpdateCooldown(float deltaTime)
        {
            if (State == SpellStateEnum.STATE_COOLDOWN)
            {
                CooldownTimer.Update(deltaTime);

                if (CooldownTimer.Finished())
                {
                    CooldownTimer = null;
                    State = SpellStateEnum.STATE_READY;
                }
            }
        }
        private void UpdateChanneling(float deltaTime)
        {
            if (State == SpellStateEnum.STATE_CHANNELING)
            {
                ChannelTimer.Update(deltaTime);

                if (ChannelTimer.Finished())
                {
                    Script.OnFinishCasting(castPosition, castEndPosition);
                    ChannelTimer = null;

                    if (autoAttackTarget != null)
                    {
                        Owner.TryBasicAttack(autoAttackTarget);
                        autoAttackTarget = null;
                    }
                    
                    if (GetTotalCooldown() > 0f)
                    {
                        CooldownTimer = new UpdateTimer(GetTotalCooldown() * 1000f);
                        CooldownTimer.Start();
                        State = SpellStateEnum.STATE_COOLDOWN;
                    }
                    else
                    {
                        State = SpellStateEnum.STATE_READY;
                    }
                    castPosition = new Vector2();
                    castEndPosition = new Vector2();
                }
            }
        }
        public void Update(float deltaTime)
        {
            UpdateCooldown(deltaTime);
            UpdateChanneling(deltaTime);

            Script?.Update(deltaTime);
        }
        public CastInformations GetCastInformations(Vector3 position, Vector3 endPosition, string spellName, uint missileNetId = 0)
        {
            if (missileNetId == 0)
            {
                missileNetId = Owner.Game.NetIdProvider.PopNextNetId();
            }
            return new CastInformations()
            {
                AmmoRechargeTime = 1f,
                AmmoUsed = 1, // ??
                AttackSpeedModifier = 1f,
                Cooldown = GetTotalCooldown(), // fonctionne avec le slot
                CasterNetID = Owner.NetId,
                IsAutoAttack = false,
                IsSecondAutoAttack = false,
                DesignerCastTime = Record.GetCastTime(),
                DesignerTotalTime = Record.GetCastTime(),
                ExtraCastTime = 0f,
                IsClickCasted = false,
                IsForceCastingOrChannel = true,
                IsOverrideCastPosition = false,
                ManaCost = 10f,
                MissileNetID = missileNetId,
                PackageHash = Owner.Record.Name.HashString(),
                SpellCastLaunchPosition = Owner.GetPositionVector3(),
                SpellChainOwnerNetID = Owner.NetId,
                SpellHash = spellName.HashString(),
                SpellLevel = Level,
                SpellNetID = 0,
                SpellSlot = Slot, // 3 = R
                StartCastTime = 0, // animation current, ou en est?
                TargetPosition = position,
                TargetPositionEnd = endPosition,
                Targets = new List<Tuple<uint, HitResultEnum>>(),
            };
        }
        private float GetTotalCooldown()
        {
            float cd = Record.GetCooldown(Level);
            cd *= (1 - (Owner.Stats.CooldownReduction.TotalSafe / 100));
            return cd;
        }
        public void Cast(Vector2 position, Vector2 endPosition, AttackableUnit autoAttackTarget = null)
        {
            if (State == SpellStateEnum.STATE_READY)
            {
                if (Script != null)
                {
                    if (this.autoAttackTarget != null)
                    {
                        throw new Exception("wtf?");
                    }
                    this.autoAttackTarget = autoAttackTarget;
                    castPosition = position;
                    castEndPosition = endPosition;
                    var castTime = Record.GetCastTime();
                    ChannelTimer = new UpdateTimer((long)(castTime * 1000));
                    ChannelTimer.Start();
                    State = SpellStateEnum.STATE_CHANNELING;
                    Script.OnStartCasting(position, endPosition);
                }
                else
                {
                    logger.Write("No script for spell:" + Record.Name, MessageState.WARNING);
                }
            }

        }


    }
}
