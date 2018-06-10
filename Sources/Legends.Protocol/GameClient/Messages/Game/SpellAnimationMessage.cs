using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SpellAnimationMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SpellAnimation;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public string animationName;

        public SpellAnimationMessage(string animationName, uint netId) : base(netId)
        {
            this.animationName = animationName;
        }
        public SpellAnimationMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte((byte)0xC4); // unk  <--
            writer.WriteUInt((uint)0); // unk     <-- One of these bytes is a flag
            writer.WriteUInt((uint)0); // unk     <--
            writer.WriteFloat((float)1.0f); // Animation speed scale factor
            foreach (var b in Encoding.UTF8.GetBytes(animationName))
                writer.WriteByte(b);
            writer.WriteByte((byte)0);
        }
    }
}
