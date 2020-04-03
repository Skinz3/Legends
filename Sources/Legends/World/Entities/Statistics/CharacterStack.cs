using Legends.Protocol.GameClient.Types;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics
{
    public class CharacterStack : IProtocolable<CharacterStackData>
    {
        private uint Id
        {
            get;
            set;
        }
        private bool ModelOnly
        {
            get;
            set;
        }
        private bool OverrideSpells
        {
            get;
            set;
        }
        private bool ReplaceCharacterPackage
        {
            get;
            set;
        }
        private uint SkinId
        {
            get;
            set;
        }
        private AIUnitRecord StackUnitRecord
        {
            get;
            set;
        }
        public CharacterStack(AIUnitRecord stackUnitRecord,uint id,bool modelOnly,bool overrideSpells,
            bool replaceCharacterPackage,uint skinId)
        {
            this.StackUnitRecord = stackUnitRecord;
            this.Id = id;
            this.ModelOnly = modelOnly;
            this.OverrideSpells = overrideSpells;
            this.ReplaceCharacterPackage = replaceCharacterPackage;
            this.SkinId = skinId;
        }

        public CharacterStackData GetProtocolObject()
        {
            return new CharacterStackData()
            {
                Id = Id,
                ModelOnly = ModelOnly,
                OverrideSpells = OverrideSpells,
                ReplaceCharacterPackage = ReplaceCharacterPackage,
                SkinName = StackUnitRecord.Name,
                SkinId = SkinId,
            };
        }
    }
}
