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
        public uint netId;

        public BaseMessage(uint netId)
        {
            this.netId = netId;
        }
        public BaseMessage()
        {

        }

        public override void Pack(LittleEndianWriter writer)
        {
            if ((short)Cmd > byte.MaxValue) // oops, riot needs ids ! 
            {
                writer.WriteByte((byte)PacketCmd.PKT_S2C_Extended);
                writer.WriteUInt(netId);
                writer.WriteShort((short)Cmd);

            }
            else
            {
                writer.WriteByte((byte)Cmd);
                writer.WriteUInt(netId);
            }

            Serialize(writer);
        }
        public override void Unpack(LittleEndianReader reader)
        {
            this.netId = reader.ReadUInt();
            this.Deserialize(reader);
        }
    }
}
