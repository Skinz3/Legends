using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol
{
    public abstract class StateMessage2 : BaseMessage
    {
        public StateMessage2(int netId) : base(netId)
        {

        }
        public StateMessage2()
        {

        }
        public override void Unpack(LittleEndianReader reader)
        {
            base.Unpack(reader);
            reader.ReadByte();
        }
        public override void Pack(LittleEndianWriter writer)
        {
            base.Pack(writer);
            writer.WriteByte(0);
        }
    }
}
