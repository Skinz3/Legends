using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        public override void Deserialize(LittleEndianReader reader) { }
        public override void Serialize(LittleEndianWriter writer) { }
    }

    public class VisibilityDataSpellMissile : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader) { }
        public override void Serialize(LittleEndianWriter writer) { }
    }

    public class VisibilityDataBuilding : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader) { }
        public override void Serialize(LittleEndianWriter writer) { }
    }

    public class VisibilityDataAILevelProp : VisibilityData
    {
        public override void Deserialize(LittleEndianReader reader) { }
        public override void Serialize(LittleEndianWriter writer) { }
    }


    public class ShieldValues
    {
        public float Magical { get; set; }
        public float Phyisical { get; set; }
        public float MagicalAndPhysical { get; set; }
    }

    public class VisibilityDataAIBase : VisibilityData
    {
        public List<ItemData> Items { get; set; } = new List<ItemData>();
        public ShieldValues ShieldValues { get; set; }
        //TODO: should be same structure as in S2C_ChangeCharacterData
        public List<CharacterStackData> CharacterDataStack { get; set; } = new List<CharacterStackData>();

        public uint LookAtNetId
        {
            get; set;
        }
        public LookAtType LookAtType
        {
            get; set;
        }
        public Vector3 LookAtPosition
        {
            get; set;
        }
        public List<KeyValuePair<byte, int>> BuffCount { get; set; } = new List<KeyValuePair<byte, int>>();

        public bool UnknownIsHero { get; set; }


        public override void Deserialize(LittleEndianReader reader)
        {
            byte itemCount = reader.ReadByte();
            for (int i = 0; i < itemCount; i++)
            {
                var item = new ItemData();
                item.Deserialize(reader)
                Items.Add(item);
                item.Slot = reader.ReadByte();
                item.ItemsInSlot = reader.ReadByte();
                item.SpellCharges = reader.ReadByte();
                item.ItemID = reader.ReadItemID();


                ItemId = reader.ReadUInt();
                Slot = reader.ReadByte();
                ItemsInSlot = reader.ReadByte();
                SpellCharges = reader.ReadByte();
            }

            bool hasShield = reader.ReadBool();
            if (hasShield)
            {
                ShieldValues = new ShieldValues();
                ShieldValues.Magical = reader.ReadFloat();
                ShieldValues.Phyisical = reader.ReadFloat();
                ShieldValues.MagicalAndPhysical = reader.ReadFloat();
            }

            int countCharStack = reader.ReadInt();
            for (int i = 0; i < countCharStack; i++)
            {
                var data = new CharacterStackData();
                data.Deserialize(reader);
                CharacterDataStack.Add(data);
            }

            LookAtNetId = reader.ReadNetID();
            LookAtType = reader.ReadLookAtType();
            LookAtPosition = reader.ReadVector3();

            int numOfBuffCount = reader.ReadInt32();
            for (int i = 0; i < numOfBuffCount; i++)
            {
                byte slot = reader.ReadByte();
                int count = reader.ReadInt32();
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
                writer.WriteByte(item.Slot);
                writer.WriteByte(item.ItemsInSlot);
                writer.WriteByte(item.SpellCharges);
                writer.WriteItemID(item.ItemID);
            }

            if (ShieldValues != null)
            {
                writer.WriteBool(true);
                writer.WriteFloat(ShieldValues.Magical);
                writer.WriteFloat(ShieldValues.Phyisical);
                writer.WriteFloat(ShieldValues.MagicalAndPhysical);
            }
            else
            {
                writer.WriteBool(false);
            }

            writer.WriteInt32(CharacterDataStack.Count);
            foreach (var data in CharacterDataStack)
            {
                writer.WriteSizedString(data.SkinName);
                writer.WriteUInt32(data.SkinId);
                byte bitfield = 0;
                if (data.OverrideSpells)
                    bitfield |= 1;
                if (data.ModelOnly)
                    bitfield |= 2;
                if (data.ReplaceCharacterPackage)
                {
                    bitfield |= 4;
                }
                writer.WriteByte(bitfield);
                writer.WriteUInt32(data.Id);
            }

            writer.WriteNetID(LookAtNetId);
            writer.WriteLookAtType(LookAtType);
            writer.WriteVector3(LookAtPosition);

            writer.WriteInt32(BuffCount.Count);
            foreach (var kvp in BuffCount)
            {
                writer.WriteByte(kvp.Key);
                writer.WriteInt32(kvp.Value);
            }

            writer.WriteBool(UnknownIsHero);
        }
    }

    public abstract class VisibilityDataAIBaseWithMovement : VisibilityDataAIBase
    {
        public int MovementSyncID { get; set; }
        public MovementData MovementData { get; set; } = new MovementDataNone();

        public override void Deserialize(LittleEndianReader reader)
        {
            base.Deserialize(reader);
            MovementDataType movementType = reader.ReadMovementDataType();
            MovementSyncID = reader.ReadInt32();
            MovementData = reader.ReadMovementData(movementType);
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteMovementDataType(MovementData.Type);
            writer.WriteInt32(MovementSyncID);
            writer.WriteMovementData(MovementData);
        }
    }

    public class VisibilityDataAIHero : VisibilityDataAIBaseWithMovement
    {
    }

    public class VisibilityDataAIMinion : VisibilityDataAIBaseWithMovement
    {
    }
}
