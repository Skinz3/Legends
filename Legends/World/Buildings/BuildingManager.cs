using Legends.Core.Utils;
using Legends.World.Entities.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Buildings
{
    public class BuildingManager : Singleton<BuildingManager>
    {
        public Stats GetDefaultStatsForAITurret()
        {
            var stats = new TurretStats(1500, 0, 0, 30, 30, 0, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0, 0, 0,500,0, 0);
            stats.IsTargetable = false;
            return stats;
        }
    }
}
