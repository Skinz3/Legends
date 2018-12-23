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
        public bool Ignorable()
        {
            return Physical == 0 && Magical == 0 && MagicalAndPhysical == 0;
        }
        public float UseMagicalShield(float damagesDelta)
        {
            float num = Magical -= damagesDelta;

            if (Magical < 0)
                Magical = 0;

            return num < 0 ? -num : 0;
        }
        public float UsePhysicalShield(float damagesDelta)
        {
            float num = Physical -= damagesDelta;

            if (Physical < 0)
                Physical = 0;

            return num < 0 ? -num : 0;
        }
        public float UseMagicalAndPhysicalShield(float damagesDelta)
        {
            float num = MagicalAndPhysical -= damagesDelta;

            if (MagicalAndPhysical < 0)
                MagicalAndPhysical = 0;

            return num < 0 ? -num : 0;
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
