using Legends.Core;
using Legends.Core.DesignPattern;
using Legends.Core.Time;
using Legends.Core.Utils;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities.AI;
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
        protected SpellStateEnum State
        {
            get;
            set;
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

        private Vector2 castPosition;
        private Vector2 castEndPosition;

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
        public void Update(float deltaTime)
        {
            if (State == SpellStateEnum.STATE_CHANNELING)
            {
                ChannelTimer.Update(deltaTime);
                if (ChannelTimer.Finished())
                {
                    State = SpellStateEnum.STATE_READY;
                    Script.OnFinishCasting(castPosition, castEndPosition);
                    ChannelTimer = null;

                    castPosition = new Vector2();
                    castEndPosition = new Vector2();
                }
            }

            Script?.Update(deltaTime);
        }
        public CastInformations GetCastInformations(Vector3 position, Vector3 endPosition, string spellName)
        {
            return new CastInformations()
            {
                AmmoRechargeTime = 1f,
                AmmoUsed = 1, // ??
                AttackSpeedModifier = 1f,
                Cooldown = 2f, // fonctionne avec le slot
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
                MissileNetID = Owner.Game.NetIdProvider.PopNextNetId(),
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

        public void Cast(Vector2 position, Vector2 endPosition)
        {
            if (State == SpellStateEnum.STATE_READY)
            {
                if (Script != null)
                {
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
