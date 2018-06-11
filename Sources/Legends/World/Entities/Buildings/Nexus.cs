using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;

namespace Legends.World.Entities.Buildings
{
    public class Nexus : AnimatedBuilding
    {
        public override bool AddFogUpdate => true;

        public Nexus(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId, buildingRecord, mapObjectRecord)
        {
        }

        public override void OnRevive(AttackableUnit source)
        {
            base.OnRevive(source); // lol ? x)

        }
    }
}
