using Legends.Core.Attributes;
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

        [InibinField(InibinHashEnum.SKINS_ChampionSkinName)]
        public string Name
        {
            get;
            set;
        }

        [InibinField(InibinHashEnum.SKINS_SkinScale)]
        public float Scale
        {
            get;
            set;
        }

        [Ignore]
        public int SkinId
        {
            get;
            private set;
        }

        [Ignore]
        public int ChampionId
        {
            get;
            private set;
        }
        public SkinRecord()
        {

        }
        public SkinRecord(string championSkinId, string name, float scale)
        {
            this.ChampionSkinId = championSkinId;
            this.Name = name;
            this.Scale = scale;

            string skinId = ChampionSkinId.Substring(ChampionSkinId.Length - 3);
            this.SkinId = int.Parse(skinId);
            this.ChampionId = int.Parse(new string(championSkinId.Take(championSkinId.Length - skinId.Length).ToArray()));
        }

        public static SkinRecord[] GetSkins(int championId)
        {
            return Skins.FindAll(x => x.ChampionId == championId).ToArray();
        }
    }
}
