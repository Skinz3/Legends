using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ChampionDieMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ChampionDie;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int goldFromKill;
        public uint killerNetId;
        public float respawnTimerMs;

        public ChampionDieMessage(int goldFromKill, uint deadNetId, uint killerNetId, float respawnTimerMs) : base(deadNetId)
        {
            this.goldFromKill = goldFromKill;
            this.killerNetId = killerNetId;
            this.respawnTimerMs = respawnTimerMs;
        }
        public ChampionDieMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(goldFromKill); // Gold from kill?
            writer.WriteByte((byte)0);

            writer.WriteUInt(killerNetId);

            writer.WriteByte((byte)0);
            writer.WriteByte((byte)7);
            writer.WriteFloat(respawnTimerMs); // Respawn timer, float
        }
    }
}
