using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Games;
using Legends.Core.Protocol.Enum;

namespace Legends.Scripts.Maps
{
    public class SummonersRiftUpdatedScript : MapScript
    {
		public const MapIdEnum MAP_ID = MapIdEnum.SummonersRiftUpdated;
		
        public SummonersRiftUpdatedScript(Game game) : base(game)
        {

        }
        /// <summary>
        /// Called when GameTime = 0. And Callback start.
        /// </summary>
        public override void OnStart()
        {
            Announce(AnnounceEnum.WelcomeToSR, 25);
            Announce(AnnounceEnum.ThirySecondsToMinionsSpawn, 35);
            Announce(AnnounceEnum.MinionsHaveSpawned, 65);
        }

        public override void OnSpawn()
        {
            SpawnAITurret("Turret_T1_R_03", "SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_R_02", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_C_07", "SRUAP_Turret_Order3_Test");
            SpawnAITurret("Turret_T2_R_03", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_R_02", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_R_01", "SRUAP_Turret_Chaos3_Test");
            SpawnAITurret("Turret_T1_C_05", "SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_C_04", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_C_03", "SRUAP_Turret_Order3_Test");
            SpawnAITurret("Turret_T1_C_01", "SRUAP_Turret_Order4");
            SpawnAITurret("Turret_T1_C_02", "SRUAP_Turret_Order4");
            SpawnAITurret("Turret_T2_C_05", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_C_04", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_C_03", "SRUAP_Turret_Chaos3_Test");
            SpawnAITurret("Turret_T2_C_01", "SRUAP_Turret_Chaos4");
            SpawnAITurret("Turret_T2_C_02", "SRUAP_Turret_Chaos4");
            SpawnAITurret("Turret_OrderTurretShrine", "SRUAP_Turret_Order5");
            SpawnAITurret("Turret_ChaosTurretShrine", "SRUAP_MageCrystal");
            SpawnAITurret("Turret_T1_L_03", "SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_L_02", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_C_06", "SRUAP_Turret_Order3_Test");
            SpawnAITurret("Turret_T2_L_03", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_L_02", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_L_01", "SRUAP_Turret_Chaos3_Test");
        }
    }
}
