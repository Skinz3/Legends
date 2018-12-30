using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Types;
using System.IO;
using Legends.Core;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class FXCreateGroupMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FX_CreateGroup;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        private FXCreateGroupData[] fXCreateGroupDatas;

        public FXCreateGroupMessage()
        {


        }
        public FXCreateGroupMessage(uint netId, FXCreateGroupData[] datas) : base(netId)
        {
            this.fXCreateGroupDatas = datas;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
        private void Serialize2(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)1); // number of particles
            writer.WriteUInt("Ezreal".HashString());
            writer.WriteUInt("ezreal_bow.troy".HashString());
            writer.WriteInt(0x00000020); // flags ?

            writer.WriteShort((short)0); // Unk
            writer.WriteUInt("L_HAND".HashString());

            writer.WriteByte((byte)1); // number of targets ?

            writer.WriteUInt(netId);
            writer.WriteUInt(39439); // Particle net id ?
            writer.WriteUInt(netId);

            writer.WriteUInt(netId);

            writer.WriteInt(0); // unk

            for (var i = 0; i < 3; ++i)
            {
                var ownerHeight = 0;
                var particleHeight = 0;
                var higherValue = Math.Max(ownerHeight, particleHeight);
                writer.WriteShort((short)1);
                writer.WriteInt(higherValue);
                writer.WriteShort((short)1);
            }

            writer.WriteUInt((uint)0); // unk
            writer.WriteUInt((uint)0); // unk
            writer.WriteUInt((uint)0); // unk
            writer.WriteUInt((uint)0); // unk
            writer.WriteFloat(1.0f); // Particle size
        }
        public override void Serialize(LittleEndianWriter writer)
        {
          //Serialize2(writer);
            //return;
            int count = fXCreateGroupDatas.Length;
            if (count > 0xFF)
            {
                throw new IOException("FXCreateGroup list too big > 255!");
            }
            writer.WriteByte((byte)count);
            foreach (var fxgroup in fXCreateGroupDatas)
            {
                fxgroup.Serialize(writer);
            }
        }
    }
}
