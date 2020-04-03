using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Utils;
using Legends.Records;
using Legends.World;
using Legends.World.Entities.AI;
using Legends.World.Games;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Spells
{
    public class BuffScriptManager : Singleton<BuffScriptManager>
    {
        static Logger logger = new Logger();

        public const string RELATIVE_PATH = "\\Scripts\\Buffs\\";

        private Type[] Scripts = new Type[0];

        public const bool LoadFromAssembly = true;

        [StartupInvoke("BuffScripts", StartupInvokePriority.Third)]
        public void Initialize()
        {
            if (LoadFromAssembly)
            {
                Scripts = Assembly.GetAssembly(typeof(BuffScriptManager)).GetTypes().Where(x => x.BaseType == typeof(BuffScript)).ToArray();

            }
            else
            {
                Scripts = InjectionManager.Instance.GetScripts(Path.Combine(Environment.CurrentDirectory + RELATIVE_PATH));
            }
        }

        public BuffScript GetBuffScript<T>(AIUnit target, AIUnit source) where T : BuffScript
        {
            Type buffScriptType = Scripts.FirstOrDefault(x => x == typeof(T));

            if (buffScriptType == null)
            {
                return null;
            }
            return (BuffScript)Activator.CreateInstance(buffScriptType, new object[] { source, target });
        }
    }
}
