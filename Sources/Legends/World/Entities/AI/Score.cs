using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI
{
    public class Score
    {
        public int DeathCount
        {
            get;
            set;
        }
        public int KillsCount
        {
            get;
            set;
        }
        public int AssitsCount
        {
            get;
            set;
        }
        public int ExecutionCount
        {
            get;
            set;
        }
        public Score()
        {
            this.DeathCount = 0;
            this.KillsCount = 0;
            this.AssitsCount = 0;
            this.ExecutionCount = 0;
        }
    }
}
