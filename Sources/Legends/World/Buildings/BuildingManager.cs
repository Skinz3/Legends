using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Utils;
using Legends.Records;
using Legends.World.Entities.AI;
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

        public static string TOWER_SUFFIX = TOWER_SEPARATOR + "A"; // ?

        public const string TOWER_BLUE_SIDE = "T1";
        public const string TOWER_RED_SIDE = "T2";

        public const char TOWER_SEPARATOR = '_';

        public const uint TOWER_NETID_X = 0xFF000000;

        public TeamId GetTeamId(string turretName)
        {
            string id = turretName.Split(TOWER_SEPARATOR)[1];

            switch (id)
            {
                case TOWER_BLUE_SIDE:
                    return TeamId.BLUE;
                case TOWER_RED_SIDE:
                    return TeamId.PURPLE;
            }

            if (turretName.ToLower().Contains("order"))
            {
                return TeamId.BLUE;
            }
            else if (turretName.ToLower().Contains("chaos"))
            {
                return TeamId.PURPLE;
            }

            return TeamId.UNKNOWN;
        }
    }
}
