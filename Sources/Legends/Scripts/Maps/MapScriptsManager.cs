using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Enum;
using Legends.World.Games;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Maps
{
    public class MapScriptsManager : Singleton<MapScriptsManager>
    {
        public const string RELATIVE_PATH = "\\Scripts\\Maps\\";

        public const string MAP_ID_FIELD_NAME = "MAP_ID";

        private Dictionary<MapIdEnum, Type> Scripts = new Dictionary<MapIdEnum, Type>();

        [StartupInvoke("Map Scripts", StartupInvokePriority.Third)]
        public void Initialize()
        {
            var types = InjectionManager.Instance.GetScripts(Path.Combine(Environment.CurrentDirectory + RELATIVE_PATH));

            foreach (var type in types)
            {
                var mapId = (MapIdEnum)type.GetField(MAP_ID_FIELD_NAME).GetValue(null);

                Scripts.Add(mapId, type);
            }
        }

        public MapScript GetMapScript(MapIdEnum mapIdEnum, Game game)
        {
            if (Scripts.ContainsKey(mapIdEnum) == false)
            {
                throw new Exception("MapScripts is not defined for " + mapIdEnum);
            }
            return (MapScript)Activator.CreateInstance(Scripts[mapIdEnum], new object[] { game });
        }
    }
}
