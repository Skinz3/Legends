using Legends.Core.IO;
using Legends.Core.Protocol;
using System;
using Legends.Core;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public byte slot;
        public byte spellSlotType;
        public Vector2 position;
        public Vector2 endPosition;
        public uint targetNetId;

        public CastSpellRequestMessage()
        {

        }
        public CastSpellRequestMessage(uint netId) : base(netId)
        {

        }



        public override void Deserialize(LittleEndianReader reader)
        {
            spellSlotType = reader.ReadByte();
            slot = reader.ReadByte();
            position = Extensions.DeserializeVector2(reader);
            endPosition = Extensions.DeserializeVector2(reader);
            targetNetId = reader.ReadUInt();

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
