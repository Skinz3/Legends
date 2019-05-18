using Legends.Core.IO;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class ChangeSlotSpellDataMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ChangeSpell;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public byte spellSlot;
        public bool isSummonerSpell;
        public IChangeSpellData changeSpellData;


        public ChangeSlotSpellDataMessage(uint netId, byte spellSlot, bool isSummonerSpell, IChangeSpellData changeSpellData) : base(netId)
        {
            this.spellSlot = spellSlot;
            this.isSummonerSpell = isSummonerSpell;
            this.changeSpellData = changeSpellData;
        }

        public ChangeSlotSpellDataMessage()
        {
        }


        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteByte(spellSlot);


            byte bitfield = 0;
            if (isSummonerSpell)
                bitfield |= 0x01;
            writer.WriteByte(bitfield);

            writer.WriteUInt((uint)changeSpellData.ChangeSlotSpellDataType);

            ChangeSpellDataExtension.WriteChangeSpellData(writer, changeSpellData);
            changeSpellData.Serialize(writer);
        }
    }
}
