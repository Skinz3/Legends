using Legends.Core;
using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class FXCreateGroupData
    {
        public uint PackageHash { get; set; }
        public uint EffectNameHash { get; set; }
        public ushort Flags { get; set; }
        public uint TargetBoneNameHash { get; set; }
        public uint BoneNameHash { get; set; }
        public List<FXCreateData> FXCreateData { get; set; } = new List<FXCreateData>();

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(PackageHash);
            writer.WriteUInt(EffectNameHash);
            writer.WriteUShort(Flags);
            writer.WriteUInt(TargetBoneNameHash);
            writer.WriteUInt(BoneNameHash);
            int count = FXCreateData.Count;
            if (count > 0xFF)
            {
                throw new IOException("FXCreateData list too big > 255!");
            }
            writer.WriteByte((byte)count);
            foreach (var fx in FXCreateData)
            {
                fx.Serialize(writer);
            }
        }
    }
    public class FXCreateData
    {
        public uint TargetNetId { get; set; }
        public uint NetAssignedNetId { get; set; }
        public uint CasterNetId { get; set; }
        public uint BindNetId { get; set; }
        public uint KeywordNetId { get; set; }
        public short PositionX { get; set; }
        public float PositionY { get; set; }
        public short PositionZ { get; set; }
        public short TargetPositionX { get; set; }
        public float TargetPositionY { get; set; }
        public short TargetPositionZ { get; set; }
        public short OwnerPositionX { get; set; }
        public float OwnerPositionY { get; set; }
        public short OwnerPositionZ { get; set; }
        public Vector3 OrientationVector { get; set; }
        public float TimeSpent { get; set; }
        public float ScriptScale { get; set; }


        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(TargetNetId);
            writer.WriteUInt(NetAssignedNetId);
            writer.WriteUInt(CasterNetId);
            writer.WriteUInt(BindNetId);
            writer.WriteUInt(KeywordNetId);
            writer.WriteShort(PositionX);
            writer.WriteFloat(PositionY);
            writer.WriteShort(PositionZ);
            writer.WriteShort(TargetPositionX);
            writer.WriteFloat(TargetPositionY);
            writer.WriteShort(TargetPositionZ);
            writer.WriteShort(PositionX);
            writer.WriteFloat(OwnerPositionY);
            writer.WriteShort(OwnerPositionZ);
            OrientationVector.Serialize(writer);
            writer.WriteFloat(TimeSpent);
            writer.WriteFloat(ScriptScale);
        }
    }
}
