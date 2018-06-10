using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer.CustomSyncs
{
    class Helper
    {
        public static int GetMapId(string path)
        {
            return int.Parse(path.ToLower().Split(new string[] { "map" }, StringSplitOptions.RemoveEmptyEntries).Last().Split('/')[0]);
        }
    }
}
