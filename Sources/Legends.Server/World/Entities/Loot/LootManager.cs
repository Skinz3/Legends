using Legends.Core.DesignPattern;
using Legends.World.Entities.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Loot
{
    public class LootManager : Singleton<LootManager>
    {
        private Dictionary<string, AIUnitLoot> AIUnitGolds = new Dictionary<string, AIUnitLoot>()
        {
            { "SRU_OrderMinionMelee",  new AIUnitLoot(20, 0.5f) },
            { "SRU_OrderMinionRanged", new AIUnitLoot(15, 0.5f) },
            { "SRU_Blue", new AIUnitLoot(100, 0f) },
            { "SRU_Red", new AIUnitLoot(100, 0f) },
            { "SRU_Gromp", new AIUnitLoot(86, 0f) },
        };


        public float GetGoldLoot(AIUnit unit)
        {
            if (AIUnitGolds.ContainsKey(unit.Record.Name))
            {
                var loot = AIUnitGolds[unit.Record.Name];
                float gameTime = unit.Game.GameTimeMinutes;
                float num = gameTime / 3f;
                return loot.DefaultGold + (loot.AddionalGoldPer3Minutes * num);
            }
            else
            {
                return 0f;
            }
        }
    }
    public struct AIUnitLoot
    {
        public float DefaultGold;

        public float AddionalGoldPer3Minutes;

        public AIUnitLoot(float defaultGold, float additionalGoldPer3Minutes)
        {
            this.DefaultGold = defaultGold;
            this.AddionalGoldPer3Minutes = additionalGoldPer3Minutes;
        }
    }
}
