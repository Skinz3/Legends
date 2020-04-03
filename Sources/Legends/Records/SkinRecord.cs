using Legends.Core.Attributes;
using Legends.Core.DesignPattern;
using Legends.Core.IO.Inibin;
using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("skins")]
    public class SkinRecord : ITable
    {
        private static List<SkinRecord> Skins = new List<SkinRecord>();

        [InibinField(InibinHashEnum.SKINS_ChampionSkinID)]
        public string ChampionSkinId
        {
            get;
            set;
        }

        [Primary]
        [InibinField(InibinHashEnum.SKINS_ChampionSkinName)]
        public string Name
        {
            get;
            set;
        }

        [JsonIgnore]
        public int SkinId
        {
            get;
            private set;
        }

        [JsonIgnore]
        public int ChampionId
        {
            get;
            private set;
        }
        [StartupInvoke(StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            foreach (var record in Skins)
            {
                string skinId = record.ChampionSkinId.Substring(record.ChampionSkinId.Length - 3);
                record.SkinId = int.Parse(skinId);
                record.ChampionId = int.Parse(new string(record.ChampionSkinId.Take(record.ChampionSkinId.Length - skinId.Length).ToArray()));
            }

        }


        public static SkinRecord[] GetSkins(int championId)
        {
            return Skins.FindAll(x => x.ChampionId == championId).ToArray();
        }
    }
}
