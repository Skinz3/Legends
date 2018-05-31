using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// ?
    /// </summary>
    public class ViewRequestMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_ViewReq;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public float x;
        public float y;
        public float zoom;
        public float y2;
        public int width;
        public int height;
        public int unk2;
        public byte requestNo;

        public ViewRequestMessage(int netId) : base(netId)
        {
        }

        public ViewRequestMessage()
        {

        }

        public override void Deserialize(LittleEndianReader reader)
        {
            x = reader.ReadFloat();
            y = reader.ReadFloat();
            zoom = reader.ReadFloat();
            y2 = reader.ReadFloat();
            width = reader.ReadInt();
            height = reader.ReadInt();
            unk2 = reader.ReadInt();
            requestNo = reader.ReadByte();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
