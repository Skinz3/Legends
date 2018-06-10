using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Définit la vie du netId concerné
    /// </summary>
    public class SetHealthMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SetHealth;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public short unk1;

        public float totalHealth;

        public float currentHealth;

        public SetHealthMessage(uint netId, short unk1, float totalHealth, float currentHealth) : base(netId)
        {
            this.unk1 = unk1;
            this.totalHealth = totalHealth;
            this.currentHealth = currentHealth;
        }
        public SetHealthMessage()
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteShort(unk1);
            writer.WriteFloat(totalHealth);
            writer.WriteFloat(currentHealth);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
