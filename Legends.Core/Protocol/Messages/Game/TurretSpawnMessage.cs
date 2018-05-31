using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Game
{
    public class TurretSpawnMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_TurretSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int turretNetId;
        public string turretName;

        public TurretSpawnMessage()
        {

        }
        public TurretSpawnMessage(int netId, int turretNetId, string turretName) : base(netId)
        {
            this.turretNetId = turretNetId;
            this.turretName = turretName;
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {

            writer.WriteInt(turretNetId);
            writer.WriteByte((byte)0x40);
            foreach (var b in Encoding.UTF8.GetBytes(turretName))
                writer.WriteByte((byte)b);
                
            writer.Fill(0, 64 - turretName.Length);
            writer.WriteByte((byte)0x0C);
            writer.WriteByte((byte)0x00);
            writer.WriteByte((byte)0x00);
            writer.WriteByte((byte)0x80);
            writer.WriteByte((byte)0x01);
        }
    }
}
