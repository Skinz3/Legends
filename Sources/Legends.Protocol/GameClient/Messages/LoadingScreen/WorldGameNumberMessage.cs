using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;

namespace Legends.Protocol.GameClient.LoadingScreen
{
    public class WorldGameNumberMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_World_SendGameNumber;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public WorldGameNumberMessage()
        {

        }
        public long gameId;
        public string name;

        public WorldGameNumberMessage(long gameId,string name)
        {
            this.gameId = gameId;
            this.name = name;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            this.gameId = reader.ReadLong();
           
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteInt(0);
            writer.WriteLong(gameId);
          

            var data = Encoding.UTF8.GetBytes(name);
            foreach (var d in data)
                writer.WriteByte((byte)d);
            writer.Fill(0, 128 - data.Length);
       
        }
    }
}
