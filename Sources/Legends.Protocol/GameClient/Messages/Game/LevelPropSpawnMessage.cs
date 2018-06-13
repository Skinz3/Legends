
using Legends.Core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;

namespace Legends.Protocol.GameClient.Messages.Game
{
    class LevelPropSpawnMessage : BaseMessage
    {
        public override PacketCmd Cmd => throw new NotImplementedException();

        public override Channel Channel => throw new NotImplementedException();

        public int levelPropNetId;
        public int skinId;
        public Vector3 position;

        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            /*    writer.Write((int)lp.NetId);
                buffer.Write((byte)0x40); // unk
                buffer.Write((byte)lp.SkinId);
                buffer.Write((byte)0);
                buffer.Write((byte)0);
                buffer.Write((byte)0); // Unk
                buffer.Write((float)lp.X);
                buffer.Write((float)lp.Z);
                buffer.Write((float)lp.Y);
                buffer.Write((float)0.0f); // Rotation Y

                buffer.Write((float)lp.DirX);
                buffer.Write((float)lp.DirZ);
                buffer.Write((float)lp.DirY);
                buffer.Write((float)lp.Unk1);
                buffer.Write((float)lp.Unk2);

                buffer.Write((float)1.0f);
                buffer.Write((float)1.0f);
                buffer.Write((float)1.0f); // Scaling
                buffer.Write((int)lp.Team); // Probably a short
                buffer.Write((int)2); // nPropType [size 1 . 4] (4.18) -- if is a prop, become unselectable and use direction params

                foreach (var b in Encoding.Default.GetBytes(lp.Name))
                    buffer.Write((byte)b);
                buffer.fill(0, 64 - lp.Name.Length);
                foreach (var b in Encoding.Default.GetBytes(lp.Model))
                    buffer.Write(b);
                buffer.fill(0, 64 - lp.Model.Length); */
        }
    }
}
