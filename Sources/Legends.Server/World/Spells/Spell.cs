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
        public SpellScript Script
        {
            get;
            private set;
        }
        private UpdateTimer ChannelTimer;
        private UpdateTimer CooldownTimer;

        private Vector2 castPosition;
        private Vector2 castEndPosition;
        private AttackableUnit target;

        private Action onChannelOverAction;

        public bool IsSummonerSpell
        {
            get;
            private set;
        }
        private uint NextProjectileId
        {
            get;
            set;
        }
        public Spell(AIUnit owner, SpellRecord record, byte slot, SpellScript script, bool isSummonerSpell)
        {
            this.Owner = owner;
            this.Record = record;
            this.Slot = slot;
            this.Script = script;
            this.Script?.Bind(this);
            this.State = SpellStateEnum.STATE_READY;
            this.IsSummonerSpell = isSummonerSpell;
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
                NotifyCooldownCurrent();
            }
        }
        public void NotifyCooldownCurrent()
        {
            if (State == SpellStateEnum.STATE_COOLDOWN)
            {
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
                    OnChannelOver();
                }
            }
        }
        private void OnChannelOver()
        {
            Script.OnFinishCasting(castPosition, castEndPosition, target);
            ChannelTimer = null;

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
            target = null;

            if (onChannelOverAction != null)
            {
                onChannelOverAction();
                onChannelOverAction = null;
            }
        }
        public void Update(float deltaTime)
        {
            UpdateCooldown(deltaTime);
            UpdateChanneling(deltaTime);
            Script?.Update(deltaTime);
        }
        public uint GetNextProjectileId()
        {
            return NextProjectileId;
        }
        public CastInformations GetCastInformations(Vector3 position, Vector3 endPosition, string spellName, uint missileNetId = 0, AttackableUnit[] targets = null)
        {
            var castTime = Record.GetCastTime();
            var infos = new CastInformations()
            {
                AmmoRechargeTime = 1f,
                AmmoUsed = 1, // ??
                AttackSpeedModifier = 1f,
                Cooldown = GetTotalCooldown(), // fonctionne avec le slot
                CasterNetID = Owner.NetId,
                IsAutoAttack = Script.AutoAttackAnimation,
                IsSecondAutoAttack = false,
                DesignerCastTime = castTime,
                DesignerTotalTime = castTime,
                ExtraCastTime = 0f,
                IsClickCasted = false,
                IsForceCastingOrChannel = false,
                IsOverrideCastPosition = false,
                ManaCost = 1f,
                MissileNetID = missileNetId,
                PackageHash =(uint)Owner.GetHash(),
                SpellCastLaunchPosition = new Vector3(Owner.Position.X, Owner.Position.Y, 100),// Owner.GetPositionVector3(),
                SpellChainOwnerNetID = Owner.NetId,
                SpellHash = spellName.HashString(),
                SpellLevel = Level,
                SpellNetID = Owner.Game.NetIdProvider.Pop(),
                SpellSlot = Slot, // 3 = R
                StartCastTime = 0, // animation current, ou en est?
                TargetPosition = position,
                TargetPositionEnd = endPosition,
                Targets = new List<Tuple<uint, HitResultEnum>>()
            };

            if (targets != null)
            {
                foreach (var target in targets)
                {
                    infos.Targets.Add(new Tuple<uint, HitResultEnum>(target.NetId, HitResultEnum.Normal));
                }
            }
            return infos;
        }
        [InDevelopment(InDevelopmentState.TEMPORARY, "if its summoner spell, not affected from cdr, only for tests ;) so uncomment // if (!IsSummonerSpell)")]
        public float GetTotalCooldown()
        {
            float cd = Record.GetCooldown(Level);

            //if (!IsSummonerSpell)
            cd *= (1 - (Owner.Stats.CooldownReduction.TotalSafe / 100));
            return cd;
        }
        public float GetChannelDuration()
        {
            return Script.OverrideCastTime != -1 ? Script.OverrideCastTime : Record.GetCastTime();
        }
        public SpellCastResultEnum Cast(Vector2 position, Vector2 endPosition, AttackableUnit target, Action onChannelOverAction)
        {
            if (Owner.SpellManager.IsChanneling() && !IsSummonerSpell)
            {
                return SpellCastResultEnum.Failed_CastingOrChanneling;
            }
            if (Owner.DashManager.IsDashing)
            {
                return SpellCastResultEnum.Failed_Dashing;
            }
            if (State == SpellStateEnum.STATE_READY)
            {
                if (Script != null)
                {
                    if (!Script.CanCast())
                    {
                        return SpellCastResultEnum.Failed_ScriptCriteria;
                    }
                    if (Script.StopMovement)
                    {
                        Owner.StopMove(true, false);
                    }

                    this.onChannelOverAction = onChannelOverAction;
                    castPosition = position;
                    this.target = target;
                    castEndPosition = endPosition;
                    var castTime = GetChannelDuration();
                  
                    if (castTime > 0)
                    {
                        ChannelTimer = new UpdateTimer((long)(castTime * 1000));
                        ChannelTimer.Start();
                        State = SpellStateEnum.STATE_CHANNELING;
                    }

                    NextProjectileId = Owner.Game.NetIdProvider.Pop();


                    Owner.EventsBinder.OnSpellStartCasting(this, position, endPosition);
                    Script.OnStartCasting(position, endPosition, target);

                    if (castTime == 0)
                    {
                        OnChannelOver();
                    }



                    return SpellCastResultEnum.OK;
                }
                else
                {
                    logger.Write("No script for spell:" + Record.Name, MessageState.WARNING);
                    return SpellCastResultEnum.Failed_NoScript;
                }
            }
            else
            {
                return SpellCastResultEnum.Failed_Cooldown;
            }
        }
        public override string ToString()
        {
            return Record.Name;
        }

    }
}
