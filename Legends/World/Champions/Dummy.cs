using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;

namespace Legends.World.Champions
{
    public class Dummy : Champion
    {
        public Dummy(Player player) : base(player)
        {
        }

        public override void ApplyE()
        {
            throw new NotImplementedException();
        }

        public override void ApplyQ()
        {
            throw new NotImplementedException();
        }

        public override void ApplyR()
        {
            throw new NotImplementedException();
        }

        public override void ApplyW()
        {
            throw new NotImplementedException();
        }
    }
}
