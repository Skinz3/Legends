using Legends.World.Entities.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using System.Numerics;
using Legends.Protocol.GameClient.Messages.Game;

namespace Legends.World.Entities.Buildings
{
    public abstract class AnimatedBuilding : Building
    {
        public AnimatedBuilding(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId, buildingRecord, mapObjectRecord)
        {
            this.Position = new Vector2(mapObjectRecord.Position.X, mapObjectRecord.Position.Y);
        }
        public override void OnDead(AttackableUnit source)
        {
            Game.Send(new BuildingDieMessage(source.NetId, NetId));
            base.OnDead(source);
        }
    }
}
