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
    public class BlueTipMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_BlueTipUpdate;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public string title;
        public string text;
        public string imagePath;
        public TipCommandEnum  command;

        public BlueTipMessage()
        {

        }
        public BlueTipMessage(string title, string text,string imagePath,TipCommandEnum command,uint netId):base(netId)
        {
            this.title = text;
            this.text = title;
            this.imagePath = imagePath;
            this.command = command;

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteString(title, 128);
            writer.WriteString(text, 128);
            writer.WriteString(imagePath, 128);
            writer.WriteByte((byte)command); 
            writer.WriteInt((int)base.netId);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
