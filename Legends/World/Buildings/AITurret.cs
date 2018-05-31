using Legends.Core.Protocol.Messages.Game;
using Legends.Records;
using Legends.World.Entities;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Buildings
{
    public class AITurret : MovableUnit
    {
        public override string Name => MapObject.Name;

        public override float PerceptionBubbleRadius => ((TurretStats)Stats).PerceptionBubbleRadius.Total;

        private MapObjectRecord MapObject
        {
            get;
            set;
        }
        public AITurret(int netId,MapObjectRecord mapObject)
        {
            this.NetId = netId;
            this.MapObject = mapObject;
            this.Position = new Vector2(mapObject.Position.X, mapObject.Position.Y);
        }
        public override void Initialize()
        {
            Stats = BuildingManager.Instance.GetDefaultStatsForAITurret();
            base.Initialize();
        }
        public override void OnUnitEnterVision(Unit unit)
        {
            var t = unit as Player;

            Console.WriteLine(t + " touched by " + this);
        }

        public override void OnUnitLeaveVision(Unit unit)
        {
            
        }

        public override void UpdateStats(bool partial)
        {
            Game.Send(new UpdateStatsMessage(0, NetId, ((TurretStats)Stats).ReplicationManager.Values, partial));
        }
    }
}
