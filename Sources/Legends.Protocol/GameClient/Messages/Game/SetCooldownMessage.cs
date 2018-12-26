using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SetCooldownMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SetCooldown;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;


        public byte slotId;
        public float currentCd;
        public float totalCd;

        public SetCooldownMessage(uint netId,byte slotId,float currentCd,float totalCd) : base(netId)
        {
            this.slotId = slotId;
            this.currentCd = currentCd;
            this.totalCd = totalCd;
        }

        public SetCooldownMessage()
        {
        }


        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(slotId);
            writer.WriteByte(0xF8); // 4.18
            writer.WriteFloat(currentCd);
            writer.WriteFloat(totalCd);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
