using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Core.Protocol.Enum;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// Brouillard de guerre pour le netId concerné
    /// </summary>
    public class FogUpdate2Message : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FogUpdate2;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int fogNetId;

        public FogUpdate2Message(int netId, int fogNetId) : base(netId)
        {
            this.fogNetId = fogNetId;
        }
        public FogUpdate2Message()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt((int)TeamId.BLUE);
            writer.WriteByte((byte)0xFE);
            writer.WriteByte((byte)0xFF);
            writer.WriteByte((byte)0xFF);
            writer.WriteByte((byte)0xFF);
            writer.WriteInt((int)0);
            writer.WriteUInt((uint)netId); // Fog Attached, when unit dies it disappears
            writer.WriteUInt((uint)fogNetId); //Fog NetID
            writer.WriteInt((int)0);
            writer.WriteFloat(10);
            writer.WriteFloat(10);
            writer.WriteFloat((float)2500);
            writer.WriteFloat((float)88.4f);
            writer.WriteFloat((float)130);
            writer.WriteFloat((float)1.0f);
            writer.WriteInt((int)0);
            writer.WriteByte((byte)199);
            writer.WriteFloat(450); // vision radius
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
