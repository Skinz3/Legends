using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.Core.Utils;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Champions
{
    public class ChampionManager : Singleton<ChampionManager>
    {
        private Logger logger = new Logger();

        private Dictionary<ChampionEnum, Type> Handlers = new Dictionary<ChampionEnum, Type>();

        [StartupInvoke("Champion Manager", StartupInvokePriority.Eighth)]
        public void Initialize()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                ChampionAttribute attribute = type.GetCustomAttribute<ChampionAttribute>();

                if (attribute != null)
                {
                    Handlers.Add(attribute.champion, type);
                }
            }
        }
        public Champion GetChampion(Player player, ChampionEnum champion)
        {
            if (Handlers.ContainsKey(champion))
            {
                return (Champion)Activator.CreateInstance(Handlers[champion], new object[] { player });
            }
            else
            {
                logger.Write("Player (" + player.Data.Name + ") request champion " + champion + " but its not implemented!",
                    MessageState.WARNING);
                return new Dummy(player);
            }
        }
    }
    public class ChampionAttribute : Attribute
    {
        public ChampionEnum champion;

        public ChampionAttribute(ChampionEnum champion)
        {
            this.champion = champion;
        }
    }
}
