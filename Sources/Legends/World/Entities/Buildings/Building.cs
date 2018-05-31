using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Buildings
{
    public class Building : AttackableUnit
    {
        public override string Name => throw new NotImplementedException();

        public override float PerceptionBubbleRadius => throw new NotImplementedException();

        public override void OnUnitEnterVision(Unit unit)
        {
            throw new NotImplementedException();
        }

        public override void OnUnitLeaveVision(Unit unit)
        {
            throw new NotImplementedException();
        }

        public override void UpdateStats(bool partial)
        {
            throw new NotImplementedException();
        }
    }
}
