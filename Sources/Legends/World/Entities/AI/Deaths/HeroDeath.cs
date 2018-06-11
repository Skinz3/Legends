using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Messages.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legends.Core;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Deaths
{
    public class HeroDeath : Death<AIHero>
    {
        public HeroDeath(AIHero hero) : base(hero)
        {
        }
        /// <summary>
        /// http://leagueoflegends.wikia.com/wiki/Death
        /// </summary>
        /// <returns></returns>
        [InDeveloppement(InDeveloppementState.STARTED, "Summoners Rifts Only, Howling abyss is different")]
        protected override float GetTimeLeftSeconds()
        {
            float level = Unit.Stats.Level;
            float minutes = Unit.Game.GameTimeMinutes;

            float brw = level * 2.5f + 7.5f;

            if (minutes >= 53.5)
            {
                return brw.GetValuePrct(150); // 150 % du result   
            }
            else if (minutes >= 45)
            {
                return brw + ((brw / 100f) * (minutes - 15f) * 2f * 0.425f) + ((brw / 100f) * (minutes - 30f) * 2f * 0.30f) + ((brw / 100f) * (minutes - 45f) * 2f * 1.45f);
            }
            else if (minutes >= 30)
            {
                return brw + ((brw / 100f) * (minutes - 15f) * 2 * 0.425f) + ((brw / 100f) * (minutes - 30f) * 2f * 0.30f);
            }
            else if (minutes >= 15)
            {
                return brw + ((brw / 100f) * (minutes - 15f) * 2f * 0.425f);
            }

            return brw;
        }

    }
}
