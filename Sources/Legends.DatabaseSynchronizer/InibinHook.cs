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

                var value = manager.GetFile(path);

                if (value != null && results.Find(x => x.Path == value.Path) == null)
                    results.Add(value);
            }











            return results.ToArray();
            foreach (ChampionEnum champion in Enum.GetValues(typeof(ChampionEnum)))
            {
                string path = string.Format("DATA/Characters/{0}/{0}.inibin", champion.ToString());
                var value = manager.GetFile(path);

                if (value == null)
                {
                    Console.WriteLine(path + " dont exist");
                }
                else
                {
                    results.Add(value);
                }
            }
            return results.ToArray();
        }
    }
}
