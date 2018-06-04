using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Game
{
    public class NextAutoattackMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_NextAutoAttack;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int targetNetId;
        public int futureProjectileNetId;
        public bool critical;
        public bool initial;

        public NextAutoattackMessage(int sourceNetId,int targetNetId,int futureProjectileNetId,bool critical,bool initial) : base(sourceNetId)
        {
            this.targetNetId = targetNetId;
            this.futureProjectileNetId = futureProjectileNetId;
            this.critical = critical;
            this.initial = initial;
        }

        public NextAutoattackMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(targetNetId);
            if (initial)
                writer.WriteByte(0x80); // extraTime
            else
                writer.WriteByte(0x7F); // extraTime

            writer.WriteInt(futureProjectileNetId);
            if (critical)
                writer.WriteByte(0x49); // attackSlot
            else
                writer.WriteByte(0x40); // attackSlot

            writer.WriteByte(0x40);
            writer.WriteByte(0x01);
            writer.WriteByte(0x7B);
            writer.WriteByte(0xEF);
            writer.WriteByte(0xEF);
            writer.WriteByte(0x01);
            writer.WriteByte(0x2E);
            writer.WriteByte(0x55);
            writer.WriteByte(0x55);
            writer.WriteByte(0x35);
            writer.WriteByte(0x94);
            writer.WriteByte(0xD3);
        }
    }
}
