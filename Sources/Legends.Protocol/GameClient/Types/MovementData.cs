using Legends.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Legends.Core;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Legends.Protocol.GameClient.Types
{
    public enum MovementDataType : byte
    {
        None = 0,
        WithSpeed = 1,
        Normal = 2,
        Stop = 3,
    }

    public abstract class MovementData
    {
        public abstract void Serialize(LittleEndianWriter writer);
        public abstract MovementDataType Type
        {
            get;
        }
    }

    public static class MovementDataExtension
    {

        public static MovementData ReadMovementData(this LittleEndianReader reader, MovementDataType type)
        {
            switch (type)
            {
                case MovementDataType.Stop:
                    return new MovementDataStop(reader);
                case MovementDataType.Normal:
                    return new MovementDataNormal(reader);
                case MovementDataType.WithSpeed:
                    return new MovementDataWithSpeed(reader);
                default:
                    return new MovementDataNone(reader);
            }
        }

        public static void WriteMovementData(this LittleEndianWriter writer, MovementData data)
        {
            data.Serialize(writer);
        }
    }


    public class MovementDataNone : MovementData
    {
        public override MovementDataType Type => MovementDataType.None;

        public override void Serialize(LittleEndianWriter writer)
        {
        }
        public MovementDataNone() { }
        public MovementDataNone(LittleEndianReader reader)
        {
        }
    }

    public class MovementDataStop : MovementData
    {
        public override MovementDataType Type => MovementDataType.Stop;

        public Vector2 Position
        {
            get; set;
        }
        public Vector2 Forward
        {
            get; set;
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            Position.Serialize(writer);
            Forward.Serialize(writer);
        }
        public MovementDataStop() { }
        public MovementDataStop(LittleEndianReader reader)
        {
            Position = Extensions.DeserializeVector2(reader);
            Forward = Extensions.DeserializeVector2(reader);
        }
    }

    public class MovementDataNormal : MovementData
    {
        public override MovementDataType Type => MovementDataType.Normal;
        public uint TeleportNetID { get; set; }
        public bool HasTeleportID { get; set; }
        public byte TeleportID { get; set; }
        public GridPosition[] Waypoints { get; set; }

        public override void Serialize(LittleEndianWriter writer)
        {
            int waypointsSize = Waypoints.Length;
            if (waypointsSize > 0x7F)
            {
                throw new Exception("Too many paths > 0x7F!");
            }
            byte bitfield = 0;
            if (Waypoints != null)
            {
                bitfield |= (byte)(waypointsSize << 1);
            }
            if (HasTeleportID)
            {
                bitfield |= 1;
            }
            writer.WriteByte(bitfield);
            if (Waypoints != null)
            {
                writer.WriteUInt(TeleportNetID);
                if (HasTeleportID)
                {
                    writer.WriteByte(TeleportID);
                }
                writer.WriteCompressedWaypoints(Waypoints);
            }
        }

        public MovementDataNormal()
        {

        }

        public MovementDataNormal(LittleEndianReader reader)
        {
            byte bitfield = reader.ReadByte();
            byte size = (byte)(bitfield >> 1);
            HasTeleportID = (bitfield & 1) != 0;
            if (size >= 2)
            {
                TeleportNetID = reader.ReadUInt();
                if (HasTeleportID)
                {
                    TeleportID = reader.ReadByte();
                }
                Waypoints = reader.ReadCompressedWaypoints(size / 2u);
            }
        }
    }

    public class MovementDataWithSpeed : MovementDataNormal
    {
        public override MovementDataType Type => MovementDataType.WithSpeed;
        public SpeedParams SpeedParams { get; set; } = new SpeedParams();

        public override void Serialize(LittleEndianWriter writer)
        {
            int waypointsSize = Waypoints.Length;
            if (waypointsSize > 0x7F)
            {
                throw new Exception("Too many paths > 0x7F!");
            }
            byte bitfield = 0;
            if (Waypoints != null)
            {
                bitfield |= (byte)(waypointsSize << 1);
            }
            if (HasTeleportID)
            {
                bitfield |= 1;
            }
            writer.WriteByte(4);//bitfield);
            if (Waypoints != null)
            {
                writer.WriteUInt(TeleportNetID);
                if (HasTeleportID)
                {
                    writer.WriteByte(TeleportID);
                }
                writer.WriteWaypointSpeedParams(SpeedParams);
                writer.WriteCompressedWaypoints(Waypoints);
            }
        }
        public MovementDataWithSpeed() { }
        public MovementDataWithSpeed(LittleEndianReader reader)
        {
            byte bitfield = reader.ReadByte();
            byte size = (byte)(bitfield >> 1);
            HasTeleportID = (bitfield & 1) != 0;
            if (size >= 2)
            {
                TeleportNetID = reader.ReadUInt();
                if (HasTeleportID)
                {
                    TeleportID = reader.ReadByte();
                }
                SpeedParams = reader.ReadWaypointSpeedParams();
                Waypoints = reader.ReadCompressedWaypoints(size / 2u);
            }
        }
    }


    public static class MovementExtension
    {
        public static MovementDataType ReadMovementDataType(this LittleEndianReader reader)
        {
            return (MovementDataType)reader.ReadByte();
        }

        public static void WriteMovementDataType(this LittleEndianWriter writer, MovementDataType type)
        {
            writer.WriteByte((byte)type);
        }

        public static GridPosition[] ReadCompressedWaypoints(this LittleEndianReader reader, uint size)
        {
            var data = new List<GridPosition>();
            BitArray flags;
            if (size >= 2)
            {
                byte[] flagsBuffer = reader.ReadBytes((int)((size - 2) / 4 + 1));
                flags = new BitArray(flagsBuffer);
            }
            else
            {
                flags = new BitArray(new byte[1]);
            }
            short lastX = reader.ReadShort();
            short lastZ = reader.ReadShort();
            data.Add(new GridPosition(lastX, lastZ));

            for (int i = 1, flag = 0; i < size; i++)
            {
                if (flags[flag])
                {
                    lastX += reader.ReadSByte();
                }
                else
                {
                    lastX = reader.ReadShort();
                }
                flag++;
                if (flags[flag])
                {
                    lastZ += reader.ReadSByte();
                }
                else
                {
                    lastZ = reader.ReadShort();
                }
                flag++;
                data.Add(new GridPosition(lastX, lastZ));
            }
            return data.ToArray();
        }

        public static void WriteCompressedWaypoints(this LittleEndianWriter writer, GridPosition[] data)
        {
            int size = data.Length;
            if (size < 1)
            {
                throw new IOException("Need at least 1 waypoint!");
            }
            byte[] flagsBuffer;
            if (size >= 2)
            {
                flagsBuffer = new byte[(size - 2) / 4 + 1u];
            }
            else
            {
                flagsBuffer = new byte[0];
            }
            var flags = new BitArray(flagsBuffer);
            for (int i = 1, flag = 0; i < size; i++)
            {
                int relativeX = data[i].X - data[i - 1].X;
                flags[flag] = (relativeX <= SByte.MaxValue && relativeX >= SByte.MinValue);
                flag++;

                int realtiveZ = data[i].Y - data[i - 1].Y;
                flags[flag] = (realtiveZ <= SByte.MaxValue && realtiveZ >= SByte.MinValue);
                flag++;
            }
            flags.CopyTo(flagsBuffer, 0);
            writer.WriteBytes(flagsBuffer);
            writer.WriteShort(data[0].X);
            writer.WriteShort(data[0].Y);
            for (int i = 1, flag = 0; i < size; i++)
            {
                if (flags[flag])
                {
                    writer.WriteSByte((SByte)(data[i].X - data[i - 1].X));
                }
                else
                {
                    writer.WriteShort(data[i].X);
                }
                flag++;
                if (flags[flag])
                {
                    writer.WriteSByte((SByte)(data[i].Y - data[i - 1].Y));
                }
                else 
                {
                    writer.WriteShort(data[i].Y);
                }
                flag++;
            }
        }
    }
}
