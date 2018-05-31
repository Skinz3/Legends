using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Types;
using System.IO;
using System.Windows.Forms;

namespace Legends.Core.Protocol.Messages.Game
{
    public class UpdateStatsMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_CharStats;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public ReplicateStat[,] values;
        public bool partial;
        public int actorNetId;

        public UpdateStatsMessage()
        {

        }
        public UpdateStatsMessage(int netId, int actorNetId, ReplicateStat[,] values, bool partial) : base(netId)
        {
            this.values = values;
            this.partial = partial;
            this.actorNetId = actorNetId;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            int syncId = Environment.TickCount;
            writer.WriteInt(syncId); // syncID
            writer.WriteByte((byte)1); // updating 1 unit


            uint masterMask = 0;
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 32; j++)
                {
                    var rep = values[i, j];
                    if (rep == null || (!rep.Changed && partial))
                    {
                        continue;
                    }

                    masterMask |= 1u << i;
                    break;
                }
            }

            writer.WriteByte((byte)masterMask);
            writer.WriteUInt((uint)actorNetId);

            for (var i = 0; i < 6; i++)
            {
                uint fieldMask = 0;
                var stream = new MemoryStream();
                var w = new LittleEndianWriter(stream);
                for (var j = 0; j < 32; j++)
                {
                    var rep = values[i, j];
                    if (rep == null || (!rep.Changed && partial))
                    {
                        continue;
                    }

                    fieldMask |= 1u << j;
                    if (rep.IsFloat)
                    {
                        var source = BitConverter.GetBytes(rep.Value);

                        if (source[0] >= 0xFE)
                        {
                            w.WriteByte((byte)0xFE);
                        }

                        w.WriteBytes(source);
                    }
                    else
                    {
                        uint num = rep.Value;
                        while (num >= 0x80)
                        {
                            w.WriteByte((byte)(num | 0x80));
                            num >>= 7;
                        }

                        w.WriteByte((byte)num);
                    }
                }

                var data = stream.ToArray();
                if (data.Length > 0)
                {
                    writer.WriteUInt(fieldMask);
                    writer.WriteByte((byte)data.Length);
                    writer.WriteBytes(data);
                }
            }

       
        }

    }


}
