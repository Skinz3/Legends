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
    public class SpellScriptManager : Singleton<SpellScriptManager>
    {
        static Logger logger = new Logger();

        public const string RELATIVE_PATH = "\\Scripts\\Spells\\";

        public const string SPELL_NAME_FIELD_NAME = "SPELL_NAME";

        private Dictionary<string, Type> Scripts = new Dictionary<string, Type>();

        public const bool LoadFromAssembly = true;

        [StartupInvoke("SpellScripts", StartupInvokePriority.Third)]
        public void Initialize()
        {
            Type[] types = null;

            if (LoadFromAssembly)
            {
                types = Assembly.GetAssembly(typeof(SpellScriptManager)).GetTypes().Where(x => x.BaseType == typeof(SpellScript)).ToArray();

            }
            else
            {
                types = InjectionManager.Instance.GetScripts(Path.Combine(Environment.CurrentDirectory + RELATIVE_PATH));

            }


            foreach (var type in types)
            {
                var spellName = (string)type.GetField(SPELL_NAME_FIELD_NAME).GetValue(null);
                Scripts.Add(spellName, type);
            }



        }

        public SpellScript GetSpellScript(SpellRecord record, AIUnit owner)
        {
            if (Scripts.ContainsKey(record.Name) == false)
            {
                return null;
            }
            return (SpellScript)Activator.CreateInstance(Scripts[record.Name], new object[] { owner, record });
        }
    }
}
