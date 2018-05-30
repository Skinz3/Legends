using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities
{
    public abstract class MovableUnit : AttackableUnit
    {
        public WaypointsCollection WaypointsCollection
        {
            get;
            private set;
        }

        public override bool IsMoving => WaypointsCollection.TargetPosition != null && WaypointsCollection.TargetPosition != Position;

        public MovableUnit()
        {
            this.WaypointsCollection = new WaypointsCollection(this);
        }



    }
}
