using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.LoadingScreen
{
    public class KeyCheckMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_KeyCheck;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_HANDSHAKE;
        public override Channel Channel => CHANNEL;

        public byte[] partialKey; //Bytes 1 to 3 from the blowfish key for that client (byte[3]
        public int playerNo;
        public long userId;   //User id
        public int trash;
        public long checkId;   //Encrypted testVar
        public int trash2;

        public KeyCheckMessage(byte[] partialKey, int playerNo, long userId, int trash, long checkId, int trash2)
        {
            this.partialKey = partialKey;
            this.playerNo = playerNo;
            this.userId = userId;
            this.trash = trash;
            this.checkId = checkId;
            this.trash2 = trash2;
        }
        public KeyCheckMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.partialKey = reader.ReadBytes(3);
            this.playerNo = reader.ReadInt();
            this.userId = reader.ReadLong();
            this.trash = reader.ReadInt();
            this.checkId = reader.ReadLong();
            this.trash2 = reader.ReadInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(partialKey[0]);
            writer.WriteByte(partialKey[1]);
            writer.WriteByte(partialKey[2]);
            writer.WriteInt(playerNo);
            writer.WriteLong(userId);
            writer.WriteInt(trash);
            writer.WriteLong(checkId);
            writer.WriteInt(trash2);
        }
    }
}
