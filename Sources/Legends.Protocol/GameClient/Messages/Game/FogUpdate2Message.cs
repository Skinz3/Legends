using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System.Numerics;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Brouillard de guerre pour le netId concerné
    /// </summary>
    public class FogUpdate2Message : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FogUpdate2;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public TeamId teamId;
        public uint unitNetId;
        public Vector2 position;
        public uint fogNetId;
        public float visionRadius;

        public FogUpdate2Message(TeamId teamId, uint unitNetId, Vector2 position, uint fogNetId, float visionRadius) : base(0)
        {
            this.teamId = teamId;
            this.unitNetId = unitNetId;
            this.position = position;
            this.fogNetId = fogNetId;
            this.visionRadius = visionRadius;
        }
        public FogUpdate2Message()
        {

        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt((int)teamId);
            writer.WriteByte((byte)0xFE);
            writer.WriteByte((byte)0xFF);
            writer.WriteByte((byte)0xFF);
            writer.WriteByte((byte)0xFF);
            writer.WriteInt((int)0);
            writer.WriteUInt((uint)unitNetId); // Fog Attached, when unit dies it disappears
            writer.WriteUInt((uint)fogNetId); //Fog NetID
            writer.WriteInt((int)0);
            writer.WriteFloat(position.X);
            writer.WriteFloat(position.Y);
            writer.WriteFloat((float)2500);
            writer.WriteFloat((float)88.4f);
            writer.WriteFloat((float)130);
            writer.WriteFloat((float)1.0f);
            writer.WriteInt((int)0);
            writer.WriteByte((byte)199);
            writer.WriteFloat(visionRadius); // vision radius
        }

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
