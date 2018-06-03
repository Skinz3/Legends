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
        //    writer.WriteByte((byte)Cmd);
            writer.WriteInt(netId);


            if ((short)Cmd >= byte.MaxValue) // Make an extended packet instead
            {
                var oldPosition = writer.Position;
                writer.Position = 0;
                writer.WriteByte((byte)PacketCmd.PKT_S2C_Extended);
                writer.Position = oldPosition;
                writer.WriteShort((short)Cmd);
            }

            Serialize(writer);
        }
        public override void Unpack(LittleEndianReader reader)
        {
            this.netId = reader.ReadInt();
            this.Deserialize(reader);
        }
    }
}
