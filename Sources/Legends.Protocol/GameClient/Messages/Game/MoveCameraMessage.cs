using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class MoveCameraMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_MoveCamera;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector3 position;
        public float seconds;

        public MoveCameraMessage()
        {

        }
        public MoveCameraMessage(uint netId, Vector3 position, float seconds) : base(netId)
        {
            this.position = position;
            this.seconds = seconds;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            // Unk, if somebody figures out let @horato know
            writer.WriteByte(0x97);
            writer.WriteByte(0xD4);
            writer.WriteByte(0x00);
            writer.WriteByte(0x58);
            writer.WriteByte(0xD7);
            writer.WriteByte(0x17);
            writer.WriteByte(0x00);
            writer.WriteByte(0xCD);
            writer.WriteByte(0xED);
            writer.WriteByte(0x13);
            writer.WriteByte(0x01);
            writer.WriteByte(0xA0);
            writer.WriteByte(0x96);

            writer.WriteFloat(position.X);
            writer.WriteFloat(position.Y); // I think this coordinate is ignored
            writer.WriteFloat(position.Z);
            writer.WriteFloat(seconds);
        }
    }
}
