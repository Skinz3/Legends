using Legends.Core;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Types
{
    public class ProtocolBasicAttack
    {
        public uint TargetNetId
        {
            get; set;
        }
        public Vector3 TargetPosition
        {
            get; set;
        }
        public SByte ExtraTime
        {
            get; set;
        }
        public uint MissileNextId
        {
            get; set;
        }
        public AttackSlotEnum AttackSlot
        {
            get;
            set;
        }
        public void Serialize(LittleEndianWriter writer)
        {
            writer.WriteUInt(TargetNetId);
            writer.WriteSByte((sbyte)(ExtraTime + 128));
            writer.WriteUInt(MissileNextId);
            writer.WriteByte((byte)AttackSlot); // attackSlot
            TargetPosition.Serialize(writer);

        }
    }
}
