using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class CastSpellRequestMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_C2S_CastSpell;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_C2S;
        public override Channel Channel => CHANNEL;

        public byte spellSlotType; // 4.18 [deprecated? . 2 first(highest) bits: 10 - ability or item, 01 - summoner spell]
        public byte spellSlot; // 0-3 [0-1 if spellSlotType has summoner spell bits set]
        public float x; // Initial point
        public float y; // (e.g. Viktor E laser starting point)
        public float x2; // Final point
        public float y2; // (e.g. Viktor E laser final point)
        public uint targetNetId; // If 0, use coordinates, else use target net id

        public CastSpellRequestMessage()
        {

        }
        public CastSpellRequestMessage(uint netId) : base(netId)
        {

        }



        public override void Deserialize(LittleEndianReader reader)
        {
            spellSlotType = reader.ReadByte();
            spellSlot = reader.ReadByte();
            x = reader.ReadFloat();
            y = reader.ReadFloat();
            x2 = reader.ReadFloat();
            y2 = reader.ReadFloat();
            targetNetId = reader.ReadUInt();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
