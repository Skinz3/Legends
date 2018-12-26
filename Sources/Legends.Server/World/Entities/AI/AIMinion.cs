using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World.Entities.Loot;
using Legends.World.Entities.Statistics;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public class AIMinion : AIUnit
    {
        public AIMinion(uint netId, AIUnitRecord record) : base(netId, record)
        {

        }

        public override bool DefaultAutoattackActivated => false;

        public override string Name => Record.Name;

        public override bool AddFogUpdate => false;

        public override float PerceptionBubbleRadius => (float)Record.PerceptionBubbleRadius;

        public MinionRoamState RoamState
        {
            get;
            protected set;
        }
        public override VisibilityData GetVisibilityData()
        {

            return new VisibilityDataAIMinion()
            {
                BuffCount = new List<KeyValuePair<byte, int>>(),
                CharacterDataStack = GetCharacterStackDatas(),
                Items = Inventory.GetItemDatas(),
                LookAtNetId = 0,
                LookAtPosition = new Vector3(),
                LookAtType = LookAtType.Direction,
                MovementData = GetMovementData(),
                MovementSyncID = Environment.TickCount,
                ShieldValues = GetShieldValues(),
                UnknownIsHero = false,
            };
        }
        public override void OnDead(AttackableUnit source)
        {
            Game.Send(new DieMessage(source.NetId, NetId));
            base.OnDead(source);
            Game.DestroyUnit(this);
        }
        public override void Initialize()
        {
            RoamState = MinionRoamState.Inactive;
            Stats = new MinionStats(Record);
            base.Initialize();
        }
        public override void UpdateStats(bool partial = true)
        {
            UpdateHeath();
            // base.UpdateStats(partial);
        }
        public override void OnSpellUpgraded(byte spellId, Spell targetSpell)
        {
            throw new NotImplementedException();
        }



        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }

        public override void Create()
        {
            throw new NotImplementedException();
        }
    }
}

