using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using Legends.Protocol.GameClient.Enum;

namespace Legends.World.Entities.Buildings
{
    public class Inhibitor : AnimatedBuilding
    {
        public override bool AddFogUpdate => true;

        public Inhibitor(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId,buildingRecord, mapObjectRecord)
        {

        }
        public override void OnDead(AttackableUnit source)
        {
            Game.UnitAnnounce(UnitAnnounceEnum.InhibitorDestroyed, NetId, source.NetId, new uint[0]);
            base.OnDead(source);
        }

    }
}
