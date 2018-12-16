using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public class StatMod
    {
        public virtual float BaseBonus
        {
            get;
            set;
        }
        public virtual float PercentBaseBonus
        {
            get;
            set;
        }

        public virtual float FlatBonus
        {
            get;
            set;
        }
        public virtual float PercentBonus
        {
            get;
            set;
        }
    }
}
