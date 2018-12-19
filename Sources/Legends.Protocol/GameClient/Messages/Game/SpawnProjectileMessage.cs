using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class SpawnProjectileMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_SpawnProjectile;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector3 projectilePosition;
        public Vector3 targetPosition;
        public float projectileSpeed;
        public bool targeted;
        public int projectileHash;
        public int ownerNetId;
        public bool ownerIsChampion;
        public int ownerChampionHash;
        public uint projectileNetId;
        public uint targetNetId;

        public SpawnProjectileMessage()
        {

        }
        public SpawnProjectileMessage(uint netId, Vector3 projectilePosition, Vector3 targetPosition, float projectileSpeed,
            bool targeted, int projHash, int ownerNetId, bool ownerIsChampion, int ownerChampionHash, uint projectileNetId, 
            uint targetNetId) : base(netId)
        {
            this.netId = netId;
            this.projectilePosition = projectilePosition;
            this.targetPosition = targetPosition;
            this.projectileSpeed = projectileSpeed;
            this.targeted = targeted;
            this.projectileHash = projHash;
            this.ownerNetId = ownerNetId;
            this.ownerIsChampion = ownerIsChampion;
            this.ownerChampionHash = ownerChampionHash;
            this.projectileNetId = projectileNetId;
            this.targetNetId = targetNetId;

        }


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {

            writer.WriteFloat((float)projectilePosition.X);
            writer.WriteFloat((float)projectilePosition.Z + 100.0f);
            writer.WriteFloat((float)projectilePosition.Y);
            writer.WriteFloat((float)projectilePosition.X);
            writer.WriteFloat((float)projectilePosition.Z);
            writer.WriteFloat((float)projectilePosition.Y);
            writer.WriteFloat((float)-0.992436f); // Rotation X
            writer.WriteInt((int)0); // Rotation Z
            writer.WriteFloat((float)-0.122766f); // Rotation Y
            writer.WriteFloat((float)-1984.871338f); // Unk
            writer.WriteFloat((float)-166.666656f); // Unk
            writer.WriteFloat((float)-245.531418f); // Unk
            writer.WriteFloat((float)projectilePosition.X);
            writer.WriteFloat((float)projectilePosition.Z + 100.0f);
            writer.WriteFloat((float)projectilePosition.Y);
            writer.WriteFloat((float)targetPosition.X);
            writer.WriteFloat((float)targetPosition.Z);
            writer.WriteFloat((float)targetPosition.Y);
            writer.WriteFloat((float)projectilePosition.X);
            writer.WriteFloat((float)projectilePosition.Z);
            writer.WriteFloat((float)projectilePosition.Y);
            writer.WriteInt((int)0); // Unk ((float)castDelay ?)
            writer.WriteFloat((float)projectileSpeed); // Projectile speed
            writer.WriteInt((int)0); // Unk
            writer.WriteInt((int)0); // Unk
            writer.WriteInt((int)0x7f7fffff); // Unk
            writer.WriteByte((byte)0); // Unk
            if (targeted)
            {
                writer.WriteShort((short)0x6B); // writer size from here
            }
            else
            {
                writer.WriteShort((short)0x66); // writer size from here
            }
            writer.WriteInt((int)projectileHash); // projectile ID (hashed name)
            writer.WriteInt((int)0); // Second net ID
            writer.WriteByte((byte)0); // spellLevel
            writer.WriteFloat((float)1.0f); // attackSpeedMod
            writer.WriteInt((int)ownerNetId);
            writer.WriteInt((int)ownerNetId);


            if (ownerIsChampion)
            {
                writer.WriteInt(ownerChampionHash);
            }
            else
            {
                writer.WriteInt((int)0);
            }

            writer.WriteUInt(projectileNetId);
            writer.WriteFloat(targetPosition.X);
            writer.WriteFloat(targetPosition.Z);
            writer.WriteFloat(targetPosition.X);
            writer.WriteFloat(targetPosition.X);
            writer.WriteFloat(targetPosition.Z + 100f);
            writer.WriteFloat(targetPosition.Y);

            if (targeted)
            {
                writer.WriteByte((byte)0x01); // numTargets
                writer.WriteUInt(targetNetId);
                writer.WriteByte((byte)0); // hitResult
            }
            else
            {
                writer.WriteByte((byte)0); // numTargets
            }

            writer.WriteFloat(1.0f); // designerCastTime -- Doesn't seem to matter
            writer.WriteInt(0); // extraTimeForCast -- Doesn't seem to matter
            writer.WriteFloat(1.0f); // designerTotalTime -- Doesn't seem to matter
            writer.WriteFloat(0.0f); // cooldown -- Doesn't seem to matter
            writer.WriteFloat(0.0f); // startCastTime -- Doesn't seem to matter
            writer.WriteByte((byte)0x00); // flags?
            writer.WriteByte((byte)0x30); // slot?
            writer.WriteFloat(0.0f); // manaCost?
            writer.WriteFloat(projectilePosition.X);
            writer.WriteFloat(projectilePosition.Z);
            writer.WriteFloat(projectilePosition.Y);
            writer.WriteInt(0); // Unk
            writer.WriteInt(0); // Unk
        }
    }
}
