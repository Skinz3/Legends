using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class ShieldValues
    {
        public float Magical
        {
            get;
            set;
        }
        public float Physical
        {
            get;
            set;
        }
        public float MagicalAndPhysical
        {
            get; set;
        }

        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteFloat(Magical);
            writer.WriteFloat(Physical);
            writer.WriteFloat(MagicalAndPhysical);
        }
        public void Deserialize(LittleEndianReader reader)
        {
            Magical = reader.ReadFloat();
            Physical = reader.ReadFloat();
            MagicalAndPhysical = reader.ReadFloat();
        }
    }
}
