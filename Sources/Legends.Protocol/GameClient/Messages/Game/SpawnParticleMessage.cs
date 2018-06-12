using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SpawnParticleMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SpawnParticle;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            /* writer.WriteByte((byte)1); // number of particles
            writer.WriteUInt((uint)particle.Owner.getChampionHash());
            buffer.Write((uint)HashFunctions.HashString(particle.Name));
            buffer.Write((int)0x00000020); // flags ?

            buffer.Write((short)0); // Unk
            buffer.Write((uint)HashFunctions.HashString(particle.BoneName));

            buffer.Write((byte)1); // number of targets ?
            buffer.Write((uint)particle.Owner.NetId);
            buffer.Write((uint)particle.NetId); // Particle net id ?
            buffer.Write((uint)particle.Owner.NetId);

            if (particle.Target.IsSimpleTarget)
                buffer.Write((int)0);
            else
                buffer.Write((particle.Target as GameObject).NetId);

            buffer.Write((int)0); // unk

            for (var i = 0; i < 3; ++i)
            {
                var map = Game.Map;
                var ownerHeight = map.NavGrid.GetHeightAtLocation(particle.Owner.X, particle.Owner.Y);
                var particleHeight = map.NavGrid.GetHeightAtLocation(particle.X, particle.Y);
                var higherValue = Math.Max(ownerHeight, particleHeight);
                buffer.Write((short)((particle.Target.X - Game.Map.NavGrid.MapWidth / 2) / 2));
                buffer.Write((float)higherValue);
                buffer.Write((short)((particle.Target.Y - Game.Map.NavGrid.MapHeight / 2) / 2));
            }

            buffer.Write((uint)0); // unk
            buffer.Write((uint)0); // unk
            buffer.Write((uint)0); // unk
            buffer.Write((uint)0); // unk
            buffer.Write((float)particle.Size); // Particle size */
        }
    }
}
