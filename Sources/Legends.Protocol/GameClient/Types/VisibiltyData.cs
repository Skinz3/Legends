using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;
using System.IO;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Types
{
    /// <summary>
    /// Thanks to Moonshadow565 (see readme.md)
    /// </summary>
    public abstract class VisibilityData
    {
        public abstract void Deserialize(LittleEndianReader reader);
        public abstract void Serialize(LittleEndianWriter writer);
    }


    public class VisibilityDataUnknown : VisibilityData
    {
        public byte[] Data { get; set; } = new byte[0];

        public override void Deserialize(LittleEndianReader reader)
        {
            Data = reader.ReadLeft();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteBytes(Data);
        }
    }

    public class VisibilityDataNeutralMinionHUD : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader)
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }

    public class VisibilityDataSpellMissile : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader)
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }

    public class VisibilityDataBuilding : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader)
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }

    public class VisibilityDataAILevelProp : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader)
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {

        }
    }




    public class VisibilityDataAIBase : VisibilityData
    {
        public List<ItemData> Items
        {
            get;
            set;
        } = new List<ItemData>();
        public ShieldValues ShieldValues
        {
            get;
            set;
        }
        public List<CharacterStackData> CharacterDataStack
        {
            get; set;
        } = new List<CharacterStackData>();

        public uint LookAtNetId
        {
            get; set;
        }
        public LookAtType LookAtType
        {
            get;
            set;
        }
        public Vector3 LookAtPosition
        {
            get; set;
        }
        public List<KeyValuePair<byte, int>> BuffCount
        {
            get;
            set;
        } = new List<KeyValuePair<byte, int>>();

        public bool UnknownIsHero
        {
            get;
            set;
        }


        public override void Deserialize(LittleEndianReader reader)
        {
            byte itemCount = reader.ReadByte();
            for (int i = 0; i < itemCount; i++)
            {
                var item = new ItemData();
                item.Deserialize(reader);
                Items.Add(item);
            }

            bool hasShield = reader.ReadBool();
            if (hasShield)
            {
                ShieldValues = new ShieldValues();
                ShieldValues.Deserialize(reader);
            }

            int countCharStack = reader.ReadInt();
            for (int i = 0; i < countCharStack; i++)
            {
                var data = new CharacterStackData();
                data.Deserialize(reader);
                CharacterDataStack.Add(data);
            }

            LookAtNetId = reader.ReadUInt();
            LookAtType = (LookAtType)reader.ReadByte();
            LookAtPosition = Core.Extensions.DeserializeVector3(reader);

            int numOfBuffCount = reader.ReadInt();
            for (int i = 0; i < numOfBuffCount; i++)
            {
                byte slot = reader.ReadByte();
                int count = reader.ReadInt();
                BuffCount.Add(new KeyValuePair<byte, int>(slot, count));
            }

            UnknownIsHero = reader.ReadBool();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            int itemCount = Items.Count;
            if (itemCount > 0xFF)
            {
                throw new IOException("More than 255 items!");
            }

            writer.WriteByte((byte)itemCount);
            foreach (var item in Items)
            {
                item.Serialize(writer);
            }

            if (ShieldValues != null)
            {
                writer.WriteBool(true);

            }
            else
            {
                writer.WriteBool(false);
            }

            writer.WriteInt(CharacterDataStack.Count);

            foreach (var data in CharacterDataStack)
            {
                data.Serialize(writer);
            }

            writer.WriteUInt(LookAtNetId);
            writer.WriteByte((byte)LookAtType);
            LookAtPosition.Serialize(writer);

            writer.WriteInt(BuffCount.Count);

            foreach (var kvp in BuffCount)
            {
                writer.WriteByte(kvp.Key);
                writer.WriteInt(kvp.Value);
            }

            writer.WriteBool(UnknownIsHero);
        }
    }

    public class VisibilityDataAIBaseWithMovement : VisibilityDataAIBase
    {
        public int MovementSyncID { get; set; }
        public MovementData MovementData { get; set; } = new MovementDataNone();

        public override void Deserialize(LittleEndianReader reader)
        {
            base.Deserialize(reader);
            MovementDataType movementType = reader.ReadMovementDataType();
            MovementSyncID = reader.ReadInt();
            MovementData = reader.ReadMovementData(movementType);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteMovementDataType(MovementData.Type);
            writer.WriteInt(MovementSyncID);
            writer.WriteMovementData(MovementData);
        }
    }


}
