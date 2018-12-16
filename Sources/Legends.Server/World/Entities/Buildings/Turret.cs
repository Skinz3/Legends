using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;

namespace Legends.World.Entities.Buildings
{
    public class Turret : AnimatedBuilding
    {
        public Turret(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId, buildingRecord, mapObjectRecord)
        {
        }

        public override bool AddFogUpdate => true;

    }
}
