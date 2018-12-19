using System;
using System.Collections.Generic;
using System.Linq;
using Legends.Core.Protocol;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Text;

namespace Legends.Protocol.GameClient.Messages.Game
{
    public class UpdateModelMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ChangeCharacterData;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public string modelName;
        public bool useSpells;
        public int skinId;

        public UpdateModelMessage(uint netId, string modelName, bool useSpells, int skinId) : base(netId)
        {
            this.modelName = modelName;
            this.useSpells = useSpells;
            this.skinId = skinId;
        }
        public UpdateModelMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteBool(useSpells); // Use spells from the new model
            writer.WriteByte((byte)0x00); // <-- These three bytes most likely form
            writer.WriteByte((byte)0x00); // <-- an int with the useSpells byte, but
            writer.WriteByte((byte)0x00); // <-- they don't seem to affect anything
            writer.WriteByte((byte)1); // Bit field with bits 1 and 2. Unk
            writer.WriteInt((int)skinId); // SkinID ( -1 means keep using current one?)

            foreach (var b in Encoding.UTF8.GetBytes(modelName))
                writer.WriteByte((byte)b);
            if (modelName.Length < 32)
                writer.Fill(0, 32 - modelName.Length);
        }
    }
}
