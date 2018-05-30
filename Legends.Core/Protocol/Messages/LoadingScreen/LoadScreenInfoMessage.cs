using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.Protocol.LoadingScreen
{
    public class LoadScreenInfoMessage : Message
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_LoadScreenInfo;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public int blueMax;
        public int purpleMax;

        public long[] blueTeam;
        public long[] purpleTeam;

        public LoadScreenInfoMessage(int blueMax, int purpleMax, long[] blueTeam, long[] purpleTeam)
        {
            this.blueMax = blueMax;
            this.purpleMax = purpleMax;
            this.blueTeam = blueTeam;
            this.purpleTeam = purpleTeam;
        }
        public LoadScreenInfoMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {

        }

        public override void Serialize(LittleEndianWriter writer)
        {
           
            writer.WriteInt(blueMax); // blueMax
            writer.WriteInt(purpleMax); // purpleMax
            foreach (var id in blueTeam)
            {
                writer.WriteLong(id);
            }

            for (var i = 0; i < blueMax - (blueTeam.Length); ++i)
                writer.WriteLong(0);

            writer.Fill(0, 144);

            foreach (var id in purpleTeam)
            {
                writer.WriteLong(id);
            }

            for (var i = 0; i < purpleMax - (purpleTeam.Length); ++i)
                writer.WriteLong(0);

            writer.Fill(0, 144);

            writer.WriteInt(blueTeam.Length);
            writer.WriteInt(purpleTeam.Length);
        }
    }
}
