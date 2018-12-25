using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class DisplayFloatingTextMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FloatingTextWithValue;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint TargetNetId;
        public FloatTextEnum floatingTextType;
        public int param;
        public string message;

        public DisplayFloatingTextMessage()
        {

        }
        public DisplayFloatingTextMessage(uint targetNetId,FloatTextEnum floatingTextType, int param,string message)
        {
            this.TargetNetId = targetNetId;
            this.floatingTextType = floatingTextType;
            this.param = param;
            this.message = message;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(TargetNetId);
            writer.WriteInt((int)floatingTextType);
            writer.WriteInt(param); 
            writer.WriteFixedStringLast(message, 128);
            
        }
    }
}
