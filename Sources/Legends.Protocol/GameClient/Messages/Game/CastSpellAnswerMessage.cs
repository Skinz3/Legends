using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CastSpellAnswerMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CastSpellAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int syncId;
        public byte unk1;
        public short unk2;
        public int spellHash;
        public uint spellNetId;
        public byte spellLevel;
        public float attackSpeedMod;
        public uint ownerNetId;
        public uint ownerNetId2;
        public int championHash;
        public uint futureProjectileId;
        public float x;
        public float z;
        public float y;
        public float xDragEnd;
        public float heigthAtLocation;
        public float yDragEnd;
        public byte numberOfTargets;
        public float designerCastTime;
        public float extraTimeForCast;
        public float designerTotalTime;
        public float cooldown;
        public float startCastTime;
        public byte flags;
        public byte slot;
        public float manaCost;
        public float ownerX;
        public float ownerZ;
        public float ownerY;
        public long unk3;

        public CastSpellAnswerMessage()
        {

        }
     
        public CastSpellAnswerMessage(uint netId, int syncId, byte unk1, short unk2, int spellHash,
            uint spellNetId, byte spellLevel, float attackSpeedMod, uint ownerNetId,
            uint ownerNetId2, int championHash, uint futureProjectileId, float x,
            float z, float y, float xDragEnd, float heigthAtLocation, float yDragEnd,
            byte numberOfTargets, float designerCastTime,
            float extraTimeForCast, float designerTotalTime, float cooldown, float startCastTime,
            byte flags, byte slot, float manaCost, float ownerX, float ownerZ, float ownerY, long unk3) : base(netId)
        {
            this.syncId = syncId;
            this.unk1 = unk1;
            this.unk2 = unk2;
            this.spellHash = spellHash;
            this.spellNetId = spellNetId;
            this.spellLevel = spellLevel;
            this.attackSpeedMod = attackSpeedMod;
            this.ownerNetId = ownerNetId;
            this.ownerNetId2 = ownerNetId2;
            this.championHash = championHash;
            this.futureProjectileId = futureProjectileId;
            this.x = x;
            this.y = y;
            this.z = z;
            this.xDragEnd = xDragEnd;
            this.heigthAtLocation = heigthAtLocation;
            this.yDragEnd = yDragEnd;
            this.numberOfTargets = numberOfTargets;
            this.designerCastTime = designerCastTime;
            this.extraTimeForCast = extraTimeForCast;
            this.designerTotalTime = designerTotalTime;
            this.cooldown = cooldown;
            this.startCastTime = startCastTime;
            this.flags = flags;
            this.slot = slot;
            this.manaCost = manaCost;
            this.ownerX = ownerX;
            this.ownerY = ownerY;
            this.ownerZ = ownerZ;
            this.unk3 = unk3;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(syncId);
            writer.WriteByte(unk1);
            writer.WriteShort(unk2);
            writer.WriteInt(spellHash);
            writer.WriteUInt(spellNetId);
            writer.WriteByte(spellLevel);
            writer.WriteFloat(attackSpeedMod);
            writer.WriteUInt(ownerNetId);
            writer.WriteUInt(ownerNetId2);
            writer.WriteInt(championHash);
            writer.WriteUInt(futureProjectileId);
            writer.WriteFloat(x);
            writer.WriteFloat(z);
            writer.WriteFloat(y);
            writer.WriteFloat(xDragEnd);
            writer.WriteFloat(heigthAtLocation);
            writer.WriteFloat(yDragEnd);
            writer.WriteByte(numberOfTargets);

            if (numberOfTargets > 0)
            {
                // (if > 0, what follows is a list of { uint32 targetNetId, uint8 hitResult})
            }
            writer.WriteFloat(designerCastTime);
            writer.WriteFloat(extraTimeForCast);
            writer.WriteFloat(designerTotalTime);
            writer.WriteFloat(cooldown);
            writer.WriteFloat(startCastTime);
            writer.WriteByte(flags);
            writer.WriteByte(slot);
            writer.WriteFloat(manaCost);
            writer.WriteFloat(ownerX);
            writer.WriteFloat(ownerZ);
            writer.WriteFloat(ownerY);
            writer.WriteLong(unk3);
        }
    }
}
