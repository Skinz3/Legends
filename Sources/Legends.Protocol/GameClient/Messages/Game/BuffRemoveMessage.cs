using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BuffRemoveMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_RemoveBuff;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte buffSlot;
        public uint buffNameHash;
        public float runTimeRemove;

        public BuffRemoveMessage()
        {

        }
        public BuffRemoveMessage(uint netId, byte buffSlot, uint buffNameHash, float runTimeRemove) : base(netId)
        {
            this.buffSlot = buffSlot;
            this.buffNameHash = buffNameHash;
            this.runTimeRemove = runTimeRemove;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(buffSlot);
            writer.WriteUInt(buffNameHash);
            writer.WriteFloat(runTimeRemove);
        }
    }
}
