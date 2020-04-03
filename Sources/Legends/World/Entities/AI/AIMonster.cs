﻿using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Geometry;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World.Entities.Statistics;
using Legends.World.Spells;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Core.DesignPattern;

namespace Legends.World.Entities.AI
{
    public class AIMonster : AIMinion
    {
        public AIMonster(uint netId, AIUnitRecord record, float spawnDelay) : base(netId, record)
        {

        }

        public override bool DefaultAutoattackActivated => false;

        public override string Name => Record.Name;

        public override bool AddFogUpdate => false;

        public override float PerceptionBubbleRadius => (float)Record.PerceptionBubbleRadius;

        public override void InflictDamages(Damages damages)
        {
            AttackTarget(damages.Source);
            base.InflictDamages(damages);
        }
        public override void OnDead(AttackableUnit source)
        {
            base.OnDead(source);
        }
        private void AttackTarget(AttackableUnit target)
        {
            TryBasicAttack(target);
            RoamState = MinionRoamState.Hostile;
        }

        private void ReturnToCamp()
        {
            RoamState = MinionRoamState.Running;
            MoveTo(SpawnPosition);
        }
        public override void Update(float deltaTime)
        {
            if (Geo.GetDistance(SpawnPosition, Position) >= 1000)
            {
                if (RoamState == MinionRoamState.Hostile)
                {
                    ReturnToCamp();
                }
            }
            base.Update(deltaTime);
        }
      
        public override void Create()
        {
            Game.Send(new CreateNeutralMessage(NetId, NetNodeEnum.Spawned, GetPositionVector3(), GetPositionVector3(),
                      new Vector3(0f, 0f, 0f), Name, Name, "wtf", "", TeamId.NEUTRAL, 0, 0, RoamState,
                      0, 0, 0, Stats.Level, 5, 20, 0, ""));

            UpdateStats(false);
            UpdateHeath();
        }
    }
}
