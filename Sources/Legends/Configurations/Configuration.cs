using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Configurations
{
    public class Configuration
    {
        public ushort ServerPort
        {
            get;
            set;
        }

        public List<PlayerData> Players
        {
            get;
            set;
        }

    }
}
