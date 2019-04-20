using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class PlayAnimationMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_Animation;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public float scaleTime;
        public float startProgress;
        public float speedRatio;
        public string animationName;

        public PlayAnimationMessage()
        {

        }
        public PlayAnimationMessage(uint netId, float scaleTime, float startProgress, float speedRatio, string animationName) : base(netId)
        {
            this.scaleTime = scaleTime;
            this.startProgress = startProgress;
            this.speedRatio = speedRatio;
            this.animationName = animationName;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(100); // animation flags, unknown
            writer.WriteFloat(scaleTime);
            writer.WriteFloat(startProgress);
            writer.WriteFloat(speedRatio);
            writer.WriteFixedStringLast(animationName, 64);
        }
    }
}
