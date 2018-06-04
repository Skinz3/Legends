using Legends.Core.IO.RAF;
using Legends.Core.Protocol.Enum;
using Legends.DatabaseSynchronizer.Attributes;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer
{
    public class InibinHook
    {
        [InibinMethod(typeof(SkinRecord))]
        public static RAFFileEntry[] GetSkinsInibin(RafManager manager)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            foreach (ChampionEnum champion in Enum.GetValues(typeof(ChampionEnum)))
            {
                string path = string.Format("DATA/Characters/{0}/Skins/", champion.ToString());
                var values = Array.FindAll(manager.GetFiles(path), x => x.Path.Contains(".inibin"));
                results.AddRange(values);


            }
            return results.ToArray();
        }
        [InibinMethod(typeof(SpellRecord))]
        public static RAFFileEntry[] GetSpellsInibin(RafManager manager)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();
            return Array.FindAll(manager.GetFiles("DATA/Spells/"), x => x.Path.Contains(".inibin"));
        }
        [InibinMethod(typeof(AIUnitRecord))]
        public static RAFFileEntry[] GetChampionsInibin(RafManager manager)
        {
            List<RAFFileEntry> results = new List<RAFFileEntry>();

            var rf = manager.GetFiles("DATA/Characters/");
            rf = rf.ToList().FindAll(x => x.Path.Contains(".inibin")).ToArray();



            foreach (var f in rf)
            {
                string aiName = f.Path.Split('/')[2];

                string path = string.Format("DATA/Characters/{0}/{0}.inibin", aiName);


                if (manager.Exists(path) && results.Find(x => x.Path == path) == null)
                    results.Add(manager.GetFile(path));
            }
            return results.ToArray();
       
        }
    }
}
