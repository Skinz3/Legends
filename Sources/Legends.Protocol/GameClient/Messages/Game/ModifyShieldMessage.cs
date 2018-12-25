using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ModifyShieldMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ModifyShield;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public bool physical;
        public bool magical;
        public bool stopShieldFade;
        public float amount;


        public ModifyShieldMessage(uint netId, bool physical, bool magical, bool stopShieldFade,float amount) : base(netId)
        {
            this.physical = physical;
            this.magical = magical;
            this.stopShieldFade = stopShieldFade;
            this.amount = amount;
        }
        public ModifyShieldMessage()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            byte bitfield = 0;
            if (physical)
                bitfield |= 1;
            if (magical)
                bitfield |= 2;
            if (stopShieldFade)
                bitfield |= 4;
            writer.WriteByte(bitfield);
            writer.WriteFloat(amount);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
