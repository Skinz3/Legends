using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Records;
using Legends.World.Buildings;
using Legends.World.Entities;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public class AITurret : AIUnit
    {
        public override string Name => MapObjectRecord.Name;

        public override float PerceptionBubbleRadius => ((TurretStats)Stats).PerceptionBubbleRadius.Total;

        private MapObjectRecord MapObjectRecord
        {
            get;
            set;
        }
        private string Suffix
        {
            get;
            set;
        }
        private AIUnitRecord AIUnitRecord
        {
            get;
            set;
        }
        private Unit Target
        {
            get;
            set;
        }
        private float AttackRange
        {
            get
            {
                return AIStats.AttackRange.Total + 150;
            }
        }
        private List<Unit> UnitsInRange
        {
            get;
            set;
        }
        public AITurret(int netId, MapObjectRecord mapObject, string suffix)
        {
            this.NetId = netId;
            this.MapObjectRecord = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);
            this.UnitsInRange = new List<Unit>();
            this.Suffix = suffix;
        }
        public override void Initialize()
        {
            AIUnitRecord = BuildingManager.Instance.GetAIUnitRecord(this);
            Stats = new TurretStats(AIUnitRecord);
            base.Initialize();
        }
        protected void SetTarget(Unit unit)
        {
            Target = unit;
            Game.Send(new SetTargetMessage(NetId, unit.NetId));
        }
        protected void UnsetTarget()
        {
            Target = null;
            Game.Send(new SetTargetMessage(NetId, 0));
        }

        public string GetClientName()
        {
            return Name + Suffix;
        }
        private Dictionary<Unit, float> GetUnitsInAttackRange()
        {
            Dictionary<Unit, float> results = new Dictionary<Unit, float>();

            foreach (var unit in GetOposedTeam().Units.Values)
            {
                float distance = this.GetDistanceTo(unit);
                if (distance <= AttackRange) // <= vs <
                {
                    results.Add(unit, distance);
                }
            }
            return results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);


            if (Target != null) // La cible n'est plus en portée de la tourelle
            {
                if (this.GetDistanceTo(Target) > AttackRange)
                    UnsetTarget();
                else
                    return;
            }
        

            var unitsInRange = GetUnitsInAttackRange(); // on cherche les autres entitées a portée de la tourelle

            if (Target == null && unitsInRange.Count > 0)
            {
                SetTarget(unitsInRange.Last().Key);
            }



        }
        public override void UpdateStats(bool partial)
        {
            Game.Send(new UpdateStatsMessage(0, NetId, ((TurretStats)Stats).ReplicationManager.Values, partial));
        }

        public override void OnUnitEnterVision(Unit unit)
        {

        }

        public override void OnUnitLeaveVision(Unit unit)
        {

        }
    }
}
