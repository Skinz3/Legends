using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Legends.Protocol.GameClient.Types
{
    public class CastInformations
    {
        public uint SpellHash { get; set; } //2 - 6
        public uint SpellNetID { get; set; } //6 - 10
        public byte SpellLevel { get; set; } //10 - 11
        public float AttackSpeedModifier { get; set; } //11 - 15
        public uint CasterNetID { get; set; } //15 - 19
        public uint SpellChainOwnerNetID { get; set; } //19 - 23
        public uint PackageHash { get; set; } //23-27
        public uint MissileNetID { get; set; } //27-31
        public Vector3 TargetPosition { get; set; } //31-35, 35-39, 39-43
        public Vector3 TargetPositionEnd { get; set; } //43-47, 47-51, 51-55
        //55 - 56 byte TargetCount
        //56 + Tupple<NetID, HitResult>  ???? is this really hit result

        public List<Tuple<uint, HitResultEnum>> Targets { get; set; } = new List<Tuple<uint, HitResultEnum>>();

        public float DesignerCastTime { get; set; } //0-4
        public float ExtraCastTime { get; set; } //4-8
        public float DesignerTotalTime { get; set; } //8-12
        public float Cooldown { get; set; } //12-16
        public float StartCastTime { get; set; } //16-20

        //bitfield byte //20-21
        public bool IsAutoAttack { get; set; }
        public bool IsSecondAutoAttack { get; set; }
        public bool IsForceCastingOrChannel { get; set; }
        public bool IsOverrideCastPosition { get; set; }
        public bool IsClickCasted { get; set; }

        public byte SpellSlot { get; set; } //21-22
        public float ManaCost { get; set; } //22-26
        public Vector3 SpellCastLaunchPosition { get; set; }//26-30,30-34,34-38
        public int AmmoUsed { get; set; }//38-42
        public float AmmoRechargeTime { get; set; }//42-46


        public void Serialize(LittleEndianWriter writer2)
        {
            byte[] buffer;

            using (var stream = new MemoryStream())
            {
                using (var writer = new LittleEndianWriter(stream))
                {
                    writer.WriteUInt(SpellHash);
                    writer.WriteUInt(SpellNetID);
                    writer.WriteByte(SpellLevel);
                    writer.WriteFloat(AttackSpeedModifier);
                    writer.WriteUInt(CasterNetID);
                    writer.WriteUInt(SpellChainOwnerNetID);
                    writer.WriteUInt(PackageHash);
                    writer.WriteUInt(MissileNetID);
                    TargetPosition.Serialize(writer);
                    TargetPositionEnd.Serialize(writer);

                    int targetCount = Targets.Count;
                    if (targetCount > 32)
                    {
                        throw new IOException("CastInfo targets > 32!!!");
                    }

                    writer.WriteByte((byte)targetCount);
                    foreach (var target in Targets)
                    {
                        writer.WriteUInt(target.Item1);
                        writer.WriteByte((byte)target.Item2);
                    }

                    writer.WriteFloat(DesignerCastTime);
                    writer.WriteFloat(ExtraCastTime);
                    writer.WriteFloat(DesignerTotalTime);
                    writer.WriteFloat(Cooldown);
                    writer.WriteFloat(StartCastTime);

                    byte bitfield = 0;
                    if (IsAutoAttack)
                    {
                        bitfield |= 1;
                    }
                    if (IsSecondAutoAttack)
                    {
                        bitfield |= 2;
                    }
                    if (IsForceCastingOrChannel)
                    {
                        bitfield |= 4;
                    }
                    if (IsOverrideCastPosition)
                    {
                        bitfield |= 8;
                    }
                    if (IsClickCasted)
                    {
                        bitfield |= 16;
                    }
                    writer.WriteByte(bitfield);

                    writer.WriteByte(SpellSlot);
                    writer.WriteFloat(ManaCost);
                    SpellCastLaunchPosition.Serialize(writer);
                    writer.WriteInt(AmmoUsed);
                    writer.WriteFloat(AmmoRechargeTime);
                }
                buffer = new byte[stream.Length];
                var data = stream.GetBuffer();
                Buffer.BlockCopy(data, 0, buffer, 0, buffer.Length);
            }
            writer2.WriteUShort((ushort)(buffer.Length + 2));
            writer2.WriteBytes(buffer);
        }
    }
}