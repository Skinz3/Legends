using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class CharacterStackData
    {
        public string SkinName
        {
            get;
            set;
        }
        public uint SkinId
        {
            get;
            set;
        }
        public bool OverrideSpells
        {
            get;
            set;
        }
        public bool ModelOnly
        {
            get;
            set;
        }
        public bool ReplaceCharacterPackage
        {
            get;
            set;
        }
        public uint Id
        {
            get;
            set;
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteSizedString(SkinName);
            writer.WriteUInt(SkinId);
            byte bitfield = 0;
            if (OverrideSpells)
                bitfield |= 1;
            if (ModelOnly)
                bitfield |= 2;
            if (ReplaceCharacterPackage)
            {
                bitfield |= 4;
            }
            writer.WriteByte(bitfield);
            writer.WriteUInt(Id);

        }
        public void Deserialize(LittleEndianReader reader)
        {
            SkinName = reader.ReadSizedString();
            SkinId = reader.ReadUInt();
            byte bitfield = reader.ReadByte();
            OverrideSpells = (bitfield & 1) != 0;
            ModelOnly = (bitfield & 2) != 0;
            ReplaceCharacterPackage = (bitfield & 4) != 0;
            Id = reader.ReadUInt();



        }
    }
}
