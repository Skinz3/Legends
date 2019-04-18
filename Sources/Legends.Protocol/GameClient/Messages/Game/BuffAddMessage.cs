using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class BuffAddMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_AddBuff;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte buffSlot;
        public BuffTypeEnum buffType;
        public byte count;
        public bool isHidden;
        public uint buffNameHash;
        public uint packageHash;
        public float runningTime;
        public float duration;
        public uint casterNetId;

        public BuffAddMessage(uint netId,byte buffSlot,BuffTypeEnum buffType,byte count,bool isHidden,uint buffNameHash,
            uint packageHash,float runningTime,float duration,uint casterNetId) : base(netId)
        {
            this.buffSlot = buffSlot;
            this.buffType = buffType;
            this.count = count;
            this.isHidden = isHidden;
            this.buffNameHash = buffNameHash;
            this.packageHash = packageHash;
            this.runningTime = runningTime;
            this.duration = duration;
            this.casterNetId = casterNetId;
        }

        public BuffAddMessage()
        {
        }


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(buffSlot);
            writer.WriteByte((byte)buffType);
            writer.WriteByte(count);
            writer.WriteBool(isHidden);
            writer.WriteUInt(buffNameHash);
            writer.WriteUInt(packageHash);
            writer.WriteFloat(runningTime);
            writer.WriteFloat(duration);
            writer.WriteUInt(casterNetId);
        }
    }
}
