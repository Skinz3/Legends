using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol
{
    public abstract class GameMessage : BaseMessage
    {
        public int syncId;

        public GameMessage(uint netId, int syncId) : base(netId)
        {
            this.syncId = syncId;
        }
        public GameMessage()
        {

        }

        public override void Pack(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)Cmd);
            writer.WriteUInt(netId);
            writer.WriteInt(syncId);
            Serialize(writer);
        }
        public override void Unpack(LittleEndianReader reader)
        {
            this.netId = reader.ReadUInt();
            this.syncId = reader.ReadInt();
            this.Deserialize(reader);
        }
    }
}
