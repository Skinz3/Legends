using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Games;
using System.Numerics;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Scripts.Maps
{
    public class SummonersRiftUpdatedScript : MapScript
    {
        public const MapIdEnum MAP_ID = MapIdEnum.SummonersRiftUpdated;
		
		public override float GoldsPerSeconds
		{
			get
			{
				return 2.04f;
			}
		}
		
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
            SpawnNexus("HQ_T1");
            SpawnNexus("HQ_T2");

            SpawnInhibitor("Barracks_T2_L1");
            SpawnInhibitor("Barracks_T2_C1");
            SpawnInhibitor("Barracks_T2_R1");
            SpawnInhibitor("Barracks_T1_R1");
            SpawnInhibitor("Barracks_T1_C1");
            SpawnInhibitor("Barracks_T1_L1");



            SpawnAITurret("Turret_T1_C_07", "SRUAP_Turret_Order3_Test");
            SpawnAITurret("Turret_T1_R_02", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_R_03", "SRUAP_Turret_Order1");




            SpawnAITurret("Turret_T2_R_03", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_R_02", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_R_01", "SRUAP_Turret_Chaos3_Test","SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T1_C_05", "SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_C_04", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_C_03", "SRUAP_Turret_Order3_Test","SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_C_01", "SRUAP_Turret_Order4","SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_C_02", "SRUAP_Turret_Order4","SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T2_C_05", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_C_04", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_C_03", "SRUAP_Turret_Chaos3_Test","SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_C_01", "SRUAP_Turret_Chaos4","SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_C_02", "SRUAP_Turret_Chaos4","SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_OrderTurretShrine", "SRUAP_Turret_Order5");
            SpawnAITurret("Turret_ChaosTurretShrine", "SRUAP_MageCrystal");
            SpawnAITurret("Turret_T1_L_03", "SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T1_L_02", "SRUAP_Turret_Order2");
            SpawnAITurret("Turret_T1_C_06", "SRUAP_Turret_Order3_Test","SRUAP_Turret_Order1");
            SpawnAITurret("Turret_T2_L_03", "SRUAP_Turret_Chaos1");
            SpawnAITurret("Turret_T2_L_02", "SRUAP_Turret_Chaos2");
            SpawnAITurret("Turret_T2_L_01", "SRUAP_Turret_Chaos3_Test","SRUAP_Turret_Chaos1");
			
			
			
			SpawnMonster("SRU_Baron",new Vector2(4945.7f,10375.5f),0);
			SpawnMonster("SRU_Dragon",new Vector2(9848.477f,4282.852f),0);
			
			
			/* Blue Side */
			SpawnMonster("SRU_Gromp",new Vector2(2121.7f,8439.49f),0);
			SpawnMonster("SRU_Blue",new Vector2(3847.7f,7863.495f),0);
			SpawnMonster("SRU_Red",new Vector2(7757.7f,4027.495f),0);
			
			
			/* Purple Side */
			SpawnMonster("SRU_Blue",new Vector2(10917.93f,7015.551f),0);
			SpawnMonster("SRU_Red",new Vector2(7057f,10777.5f),0);
			
			
			/* Tests */
			SpawnMonster("SRU_OrderMinionMelee",new Vector2(6945.7f,7177.495f),0);
			SpawnMonster("SRU_OrderMinionRanged",new Vector2(7179.7f,7025.495f),0);
			
			
			
			
        }

        public override void CreateBindings()
        {
            /* Top lane */
            AddBinding(new string[] { "Turret_T2_L_01" }, new string[] { "Barracks_T2_L1" }, true);
            AddBinding(new string[] { "Turret_T2_L_02" }, new string[] { "Turret_T2_L_01" }, true);
            AddBinding(new string[] { "Turret_T2_L_03" }, new string[] { "Turret_T2_L_02" }, true);

            AddBinding(new string[] { "Turret_T1_L_03" }, new string[] { "Turret_T1_L_02" }, true);
            AddBinding(new string[] { "Turret_T1_L_02" }, new string[] { "Turret_T1_C_06" }, true);
            AddBinding(new string[] { "Turret_T1_C_06" }, new string[] { "Barracks_T1_L1" }, true);

            /* Mid lane */
            AddBinding(new string[] { "Turret_T2_C_03" }, new string[] { "Barracks_T2_C1" }, true);
            AddBinding(new string[] { "Turret_T2_C_04" }, new string[] { "Turret_T2_C_03" }, true);
            AddBinding(new string[] { "Turret_T2_C_05" }, new string[] { "Turret_T2_C_04" }, true);

            AddBinding(new string[] { "Turret_T1_C_05" }, new string[] { "Turret_T1_C_04" }, true);
            AddBinding(new string[] { "Turret_T1_C_04" }, new string[] { "Turret_T1_C_03" }, true);
            AddBinding(new string[] { "Turret_T1_C_03" }, new string[] { "Barracks_T1_C1" }, true);

            /* Bot lane */
            AddBinding(new string[] { "Turret_T1_R_03" }, new string[] { "Turret_T1_R_02" }, true);
            AddBinding(new string[] { "Turret_T1_R_02" }, new string[] { "Turret_T1_C_07" }, true);
            AddBinding(new string[] { "Turret_T1_C_07" }, new string[] { "Barracks_T1_R1" }, true);

            AddBinding(new string[] { "Turret_T2_R_03" }, new string[] { "Turret_T2_R_02" }, true);
            AddBinding(new string[] { "Turret_T2_R_02" }, new string[] { "Turret_T2_R_01" }, true);
            AddBinding(new string[] { "Turret_T2_R_01" }, new string[] { "Barracks_T2_R1" }, true);

            /* Inhibitors */
            AddBinding(new string[] { "Barracks_T1_R1", "Barracks_T1_C1", "Barracks_T1_L1" }, new string[] { "Turret_T1_C_01", "Turret_T1_C_02" }, false);
            AddBinding(new string[] { "Barracks_T2_R1", "Barracks_T2_C1", "Barracks_T2_L1" }, new string[] { "Turret_T2_C_01", "Turret_T2_C_02" }, false);

            /* Nexus */
            AddBinding(new string[] { "Turret_T1_C_01", "Turret_T1_C_02" }, new string[] { "HQ_T1" }, true);
            AddBinding(new string[] { "Turret_T2_C_01", "Turret_T2_C_02" }, new string[] { "HQ_T2" }, true);

        }
    }
}
