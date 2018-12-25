using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;
using Legends.Protocol.GameClient.Types;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ChangeCharacterDataMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ChangeCharacterData;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public CharacterStackData stackData;

        public ChangeCharacterDataMessage(uint netId, CharacterStackData stackData) : base(netId)
        {
            this.stackData = stackData;
        }
        public ChangeCharacterDataMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(stackData.Id);
            byte bitfield = 0;
            if (stackData.OverrideSpells)
            {
                bitfield |= 1;
            }
            if (stackData.ModelOnly)
            {
                bitfield |= 2;
            }
            if (stackData.ReplaceCharacterPackage)
            {
                bitfield |= 4;
            }
            writer.WriteByte(bitfield);

            writer.WriteUInt(stackData.SkinId);
            writer.WriteFixedStringLast(stackData.SkinName, 64);
        }
    }
}
