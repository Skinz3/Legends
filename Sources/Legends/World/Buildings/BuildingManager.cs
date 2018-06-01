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
        public Dictionary<string, string> AIHeroBinder = new Dictionary<string,string>()
        {
              {"Turret_T1_R_03", "SRUAP_Turret_Order1" },
              {"Turret_T1_R_02", "SRUAP_Turret_Order2" },
              {"Turret_T1_C_07", "SRUAP_Turret_Order3_Test"},
              {"Turret_T2_R_03", "SRUAP_Turret_Chaos1"},
              {"Turret_T2_R_02", "SRUAP_Turret_Chaos2" },
              {"Turret_T2_R_01", "SRUAP_Turret_Chaos3_Test" },
              {"Turret_T1_C_05", "SRUAP_Turret_Order1" },
              {"Turret_T1_C_04", "SRUAP_Turret_Order2" },
              {"Turret_T1_C_03", "SRUAP_Turret_Order3_Test"},
              {"Turret_T1_C_01", "SRUAP_Turret_Order4"},
              {"Turret_T1_C_02", "SRUAP_Turret_Order4"},
              {"Turret_T2_C_05", "SRUAP_Turret_Chaos1"},
              {"Turret_T2_C_04", "SRUAP_Turret_Chaos2"},
              {"Turret_T2_C_03", "SRUAP_Turret_Chaos3_Test"},
              {"Turret_T2_C_01", "SRUAP_Turret_Chaos4"},
              {"Turret_T2_C_02", "SRUAP_Turret_Chaos4"},
              {"Turret_OrderTurretShrine", "SRUAP_Turret_Order5"},
              {"Turret_ChaosTurretShrine", "SRUAP_MageCrystal"},
              {"Turret_T1_L_03", "SRUAP_Turret_Order1"},
              {"Turret_T1_L_02", "SRUAP_Turret_Order2"},
              {"Turret_T1_C_06", "SRUAP_Turret_Order3_Test"},
              {"Turret_T2_L_03", "SRUAP_Turret_Chaos1"},
              {"Turret_T2_L_02", "SRUAP_Turret_Chaos2"},
              {"Turret_T2_L_01", "SRUAP_Turret_Chaos3_Test"},
        };
        public static string TOWER_SUFFIX = TOWER_SEPARATOR + "A"; // ?

        public const string TOWER_BLUE_SIDE = "T1";
        public const string TOWER_RED_SIDE = "T2";

        public const char TOWER_SEPARATOR = '_';

        public const uint TOWER_NETID_X = 0xFF000000;

        public AIUnitRecord GetAIUnitRecord(AITurret turret)
        {
            string aiHeroName = AIHeroBinder[turret.Name];
            return AIUnitRecord.GetAIUnitRecord(aiHeroName);
        }
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

            return TeamId.UNKNOWN;
        }
    }
}
