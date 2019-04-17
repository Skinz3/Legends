using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SetAnimationsStatesMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SetAnimation;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public Dictionary<string, string> animationOverrides;

        public SetAnimationsStatesMessage(uint netId, Dictionary<string, string> animationOverrides) : base(netId)
        {
            this.animationOverrides = animationOverrides;
        }

        public SetAnimationsStatesMessage()
        {

        }


        public override void Serialize(LittleEndianWriter writer)
        {
            if (animationOverrides.Count > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            writer.WriteByte((byte)animationOverrides.Count);

            foreach (var t in animationOverrides)
            {
                writer.WriteSizedString(t.Key);
                writer.WriteSizedString(t.Value);

            }
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
