using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;
using System.Numerics;
using Legends.Core;
using Legends.Protocol.GameClient.Enum;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CreateTurretMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CreateTurret;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public uint turretNetId;
        public string turretName;

        public CreateTurretMessage()
        {

        }
        public CreateTurretMessage(uint netId, uint turretNetId, string turretName) : base(netId)
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
            writer.WriteUInt(turretNetId);
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
