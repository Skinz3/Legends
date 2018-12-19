using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;


namespace Legends.Protocol.GameClient.Types
{
    public class SpeedParams
    {
        public float PathSpeedOverride { get; set; }
        public float ParabolicGravity { get; set; }
        public Vector2 ParabolicStartPoint { get; set; }
        public bool Facing { get; set; }
        public uint FollowNetID { get; set; }
        public float FollowDistance { get; set; }
        public float FollowBackDistance { get; set; }
        public float FollowTravelTime { get; set; }
    }

    public static class SpeedParamsExtension
    {
        public static SpeedParams ReadWaypointSpeedParams(this LittleEndianReader reader)
        {
            var data = new SpeedParams();
            data.PathSpeedOverride = reader.ReadFloat();
            data.ParabolicGravity = reader.ReadFloat();
            data.ParabolicStartPoint = Extensions.DeserializeVector2(reader);
            data.Facing = reader.ReadBool();
            data.FollowNetID = reader.ReadUInt();
            data.FollowDistance = reader.ReadFloat();
            data.FollowBackDistance = reader.ReadFloat();
            data.FollowTravelTime = reader.ReadFloat();
            return data;
        }

        public static void WriteWaypointSpeedParams(this LittleEndianWriter writer, SpeedParams data)
        {
            if (data == null)
            {
                data = new SpeedParams();
            }
            writer.WriteFloat(data.PathSpeedOverride);
            writer.WriteFloat(data.ParabolicGravity);
            data.ParabolicStartPoint.Serialize(writer);
            writer.WriteBool(data.Facing);
            writer.WriteUInt(data.FollowNetID);
            writer.WriteFloat(data.FollowDistance);
            writer.WriteFloat(data.FollowBackDistance);
            writer.WriteFloat(data.FollowTravelTime);
        }
    }
}
