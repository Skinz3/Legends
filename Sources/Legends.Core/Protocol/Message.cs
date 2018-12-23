using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol
{
    public abstract class Message
    {
        public abstract PacketCmd Cmd
        {
            get;
        }
        public abstract Channel Channel
        {
            get;
        }

        public abstract void Serialize(LittleEndianWriter writer);

        public abstract void Deserialize(LittleEndianReader reader);

        public override string ToString()
        {
            return GetType().Name;
        }

        public virtual void Unpack(LittleEndianReader reader)
        {
            this.Deserialize(reader);
        }
        public virtual void Pack(LittleEndianWriter writer)
        {
            WriteHeader(writer);
            Serialize(writer);
        }
        public void WriteHeader(LittleEndianWriter writer)
        {
            int cmd = (int)Cmd;

            if (cmd > 0xFFFF)
                cmd = cmd >> 16;

            writer.WriteByte((byte)cmd);
        }
        
    }
    public class MessageHandlerAttribute : Attribute
    {
        public PacketCmd packetCmd;
        public Channel channel;

        public MessageHandlerAttribute(PacketCmd packetCmd,Channel channel = Channel.CHL_C2S)
        {
            this.packetCmd = packetCmd;
            this.channel = channel;
        }
    }
}
