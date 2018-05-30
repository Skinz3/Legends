using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public abstract class ReplicationStats : Stats
    {
        public ReplicationManager ReplicationManager
        {
            get;
            private set;
        }
        public ReplicationStats(float baseHeath, float baseMana, float baseHpRegen,float baseArmor) : base(baseHeath, baseMana, baseHpRegen,baseArmor)
        {
            this.ReplicationManager = new ReplicationManager();
        }

        public abstract void UpdateReplication(bool partial = true);

    }
}
