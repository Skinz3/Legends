using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public interface IChangeSpellData
    {
        ChangeSlotSpellDataType ChangeSlotSpellDataType { get; }
        void Deserialize(LittleEndianReader reader);
        void Serialize(LittleEndianWriter writer);
    }

    public static class ChangeSpellDataExtension
    {
        public static IChangeSpellData ReadChangeSpellData(this LittleEndianReader reader)
        {
            IChangeSpellData data;
            ChangeSlotSpellDataType type = (ChangeSlotSpellDataType)reader.ReadByte();
            switch (type)
            {
                case ChangeSlotSpellDataType.TargetingType:
                    data = new ChangeSpellDataTargetingType();
                    break;
                case ChangeSlotSpellDataType.SpellName:
                    data = new ChangeSpellDataSpellName();
                    break;
                case ChangeSlotSpellDataType.Range:
                    data = new ChangeSpellDataRange();
                    break;
                case ChangeSlotSpellDataType.MaxGrowthRange:
                    data = new ChangeSpellDataMaxGrowthRange();
                    break;
                case ChangeSlotSpellDataType.RangeDisplay:
                    data = new ChangeSpellDataRangeDisplay();
                    break;
                case ChangeSlotSpellDataType.IconIndex:
                    data = new ChangeSpellDataIconIndex();
                    break;
                case ChangeSlotSpellDataType.OffsetTarget:
                    data = new ChangeSpellDataOffsetTarget();
                    break;
                default:
                    data = new ChangeSpellDataUnknown();
                    break;
            }
            data.Deserialize(reader);
            return data;
        }
        public static void WriteChangeSpellData(this LittleEndianWriter writer, IChangeSpellData data)
        {
            data.Serialize(writer);
        }
    }

    public class ChangeSpellDataUnknown : IChangeSpellData
    {
        private ChangeSlotSpellDataType _changeSlotSpellDataType;
        public ChangeSpellDataUnknown() { }
        public ChangeSpellDataUnknown(ChangeSlotSpellDataType type) => _changeSlotSpellDataType = type;
        public ChangeSlotSpellDataType ChangeSlotSpellDataTypeRaw
        {
            get => _changeSlotSpellDataType;
            set => _changeSlotSpellDataType = value;
        }
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => _changeSlotSpellDataType;
        public void Deserialize(LittleEndianReader reader) { }
        public void Serialize(LittleEndianWriter writer) { }
    }

    public class ChangeSpellDataTargetingType : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.TargetingType;
        public TargetingType TargetingType { get; set; }
        public void Deserialize(LittleEndianReader reader)
        {
            TargetingType = (TargetingType)reader.ReadByte();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)TargetingType);
        }
    }

    public class ChangeSpellDataSpellName : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.SpellName;
        public string SpellName { get; set; } = "";
        public void Deserialize(LittleEndianReader reader)
        {
            SpellName = reader.ReadZeroTerminatedString();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteZeroTerminatedString(SpellName);
        }
    }

    public class ChangeSpellDataRange : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.Range;
        public float CastRange { get; set; }
        public void Deserialize(LittleEndianReader reader)
        {
            CastRange = reader.ReadFloat();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(CastRange);
        }
    }

    public class ChangeSpellDataMaxGrowthRange : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.MaxGrowthRange;
        public float OverrideMaxCastRange { get; set; }
        public void Deserialize(LittleEndianReader reader)
        {
            OverrideMaxCastRange = reader.ReadFloat();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(OverrideMaxCastRange);
        }
    }

    public class ChangeSpellDataRangeDisplay : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.RangeDisplay;
        public float OverrideCastRangeDisplay { get; set; }
        public void Deserialize(LittleEndianReader reader)
        {
            OverrideCastRangeDisplay = reader.ReadFloat();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(OverrideCastRangeDisplay);
        }
    }

    public class ChangeSpellDataIconIndex : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.IconIndex;
        public byte IconIndex { get; set; }
        public void Deserialize(LittleEndianReader reader)
        {
            IconIndex = reader.ReadByte();
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(IconIndex);
        }
    }

    public class ChangeSpellDataOffsetTarget : IChangeSpellData
    {
        public ChangeSlotSpellDataType ChangeSlotSpellDataType => ChangeSlotSpellDataType.OffsetTarget;
        public List<uint> Targets { get; set; } = new List<uint>();
        public void Deserialize(LittleEndianReader reader)
        {
            int count = reader.ReadByte();
            for (int i = 0; i < count; i++)
            {
                Targets.Add(reader.ReadUInt());
            }
        }
        public void Serialize(LittleEndianWriter writer)
        {
            var count = Targets.Count;
            if (count > 0xFF)
            {
                throw new IOException("Too many targets!");
            }
            writer.WriteByte((byte)count);
            for (var i = 0; i < count; i++)
            {
                writer.WriteUInt(Targets[i]);
            }
        }
    }
}
