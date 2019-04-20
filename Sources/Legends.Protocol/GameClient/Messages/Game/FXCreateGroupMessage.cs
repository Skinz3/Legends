using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Types;
using System.IO;
using Legends.Core;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class FXCreateGroupMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_FX_CreateGroup;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        private FXCreateGroupData[] fXCreateGroupDatas;

        public FXCreateGroupMessage()
        {


        }
        public FXCreateGroupMessage(uint netId, FXCreateGroupData[] datas) : base(netId)
        {
            this.fXCreateGroupDatas = datas;
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
        public override void Serialize(LittleEndianWriter writer)
        {
            int count = fXCreateGroupDatas.Length;
            if (count > 0xFF)
            {
                throw new IOException("FXCreateGroup list too big > 255!");
            }
            writer.WriteByte((byte)count);
            foreach (var fxgroup in fXCreateGroupDatas)
            {
                fxgroup.Serialize(writer);
            }
        }
    }
}
