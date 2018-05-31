using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;

namespace Legends.Core.Protocol.Messages.Game
{
    public class UpdateModelMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_UpdateModel;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public string modelName;
        public bool useSpells;
        public int skinId;

        public UpdateModelMessage(int netId, string modelName, bool useSpells, int skinId) : base(netId)
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
            writer.WriteBoolean(useSpells); // Use spells from the new model
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
