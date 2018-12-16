using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Enum;
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
    public class BuildingProvider : Singleton<BuildingProvider>
    {

        public static string TOWER_SUFFIX = BUILDING_SEPARATOR + "A"; // ?

        public const string BUILDING_BLUE_SIDE = "T1";
        public const string BUILDING_RED_SIDE = "T2";

        public const char BUILDING_SEPARATOR = '_';

        public const uint BUILDING_NETID_X = 0xFF000000;

        public TeamId GetTeamId(string buildingName)
        {
            string id = buildingName.Split(BUILDING_SEPARATOR)[1];

            switch (id)
            {
                case BUILDING_BLUE_SIDE:
                    return TeamId.BLUE;
                case BUILDING_RED_SIDE:
                    return TeamId.PURPLE;
            }

            if (buildingName.ToLower().Contains("order"))
            {
                return TeamId.BLUE;
            }
            else if (buildingName.ToLower().Contains("chaos"))
            {
                return TeamId.PURPLE;
            }

            return TeamId.UNKNOWN;
        }
    }
}
