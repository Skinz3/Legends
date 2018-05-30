using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol
{
    public abstract class BaseMessage : Message
    {
        public int netId;

        public BaseMessage(int netId)
        {
            this.netId = netId;
        }
        public BaseMessage()
        {

        }

        public override void Pack(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)Cmd);
            writer.WriteInt(netId);
            Serialize(writer);
        }
        public override void Unpack(LittleEndianReader reader)
        {
            this.netId = reader.ReadInt();
            this.Deserialize(reader);
        }
    }
}
