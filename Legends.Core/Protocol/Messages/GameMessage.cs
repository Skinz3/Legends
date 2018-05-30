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
        public int ticks;

        public GameMessage(int netId, int ticks) : base(netId)
        {
            this.ticks = ticks;
        }
        public GameMessage()
        {

        }

        public override void Pack(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)Cmd);
            writer.WriteInt(netId);
            writer.WriteInt(ticks);
            Serialize(writer);
        }
        public override void Unpack(LittleEndianReader reader)
        {
            this.netId = reader.ReadInt();
            this.ticks = reader.ReadInt();
            this.Deserialize(reader);
        }
    }
}
