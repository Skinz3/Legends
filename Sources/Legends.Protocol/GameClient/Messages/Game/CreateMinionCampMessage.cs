using Legends.Core;
using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CreateMinionCampMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CreateMinionCamp;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector3 Position;
        public string MinimapIcon;
        public byte CampIndex;
        public AudioVOComponentEvent AudioVOComponentRevealEvent;
        public TeamId TeamSide;
        public int TimerType;
        public float Expire;

        public CreateMinionCampMessage()
        {

        }
        public CreateMinionCampMessage(uint netId, Vector3 position, string minimapIcon, byte campIndex,
            AudioVOComponentEvent audioVOComponentEvent, TeamId teamId, int timerType, float expire)
        {
            this.Position = position;
            this.MinimapIcon = minimapIcon;
            this.CampIndex = campIndex;
            this.AudioVOComponentRevealEvent = audioVOComponentEvent;
            this.TeamSide = teamId;
            this.TimerType = timerType;
            this.Expire = expire;
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            Position.Serialize(writer);
            writer.WriteString(MinimapIcon, 64);
            writer.WriteByte(CampIndex);
            writer.WriteByte((byte)AudioVOComponentRevealEvent);
            writer.WriteByte((byte)TeamSide);
            writer.WriteInt(TimerType);
            writer.WriteFloat(Expire);
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
