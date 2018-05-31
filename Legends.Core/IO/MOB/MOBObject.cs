using System.Numerics;
using System.Text;

namespace Legends.Core.IO.MOB
{
    /// <summary>
    /// Represents an Object inside of a <see cref="MOBFile"/>
    /// </summary>
    public class MOBObject
    {
        /// <summary>
        /// Name of this <see cref="MOBObject"/>
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type of this <see cref="MOBObject"/>
        /// </summary>
        public MOBObjectType Type { get; set; }
        /// <summary>
        /// Position of this <see cref="MOBObject"/>
        /// </summary>
        public Vector3 Position { get; set; }
        /// <summary>
        /// Rotation of this <see cref="MOBObject"/>
        /// </summary>
        public Vector3 Rotation { get; set; }
        /// <summary>
        /// Scale of this <see cref="MOBObject"/>
        /// </summary>
        public Vector3 Scale { get; set; }
        /// <summary>
        /// Used to store additional Vector data of this <see cref="MOBObject"/>
        /// </summary>
        public Vector3 ReservedVector1 { get; set; }
        /// <summary>
        /// Used to store additional Vector data of this <see cref="MOBObject"/>
        /// </summary>
        public Vector3 ReservedVector2 { get; set; }

        /// <summary>
        /// Initializes a new <see cref="MOBObject"/>
        /// </summary>
        /// <param name="name">Name of this <see cref="MOBObject"/></param>
        /// <param name="type">Type of this <see cref="MOBObject"/></param>
        /// <param name="position">Position of this <see cref="MOBObject"/></param>
        /// <param name="rotation">Scale of this <see cref="MOBObject"/></param>
        /// <param name="scale">Scale of this <see cref="MOBObject"/></param>
        /// <param name="reservedVector1">Used to store additional Vector data of this <see cref="MOBObject"/></param>
        /// <param name="reservedVector2">Used to store additional Vector data of this <see cref="MOBObject"/></param>
        public MOBObject(string name, MOBObjectType type, Vector3 position, Vector3 rotation, Vector3 scale, Vector3 reservedVector1, Vector3 reservedVector2)
        {
            this.Name = name;
            this.Type = type;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.ReservedVector1 = reservedVector1;
            this.ReservedVector2 = reservedVector2;
        }

        /// <summary>
        /// Initializes a new <see cref="MOBObject"/> from a <see cref="BinaryReader"/>
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> to read from</param>
        public MOBObject(LittleEndianReader reader)
        {
            this.Name = Encoding.ASCII.GetString(reader.ReadBytes(60)).Replace("\0", "");
            reader.ReadShort();
            this.Type = (MOBObjectType)reader.ReadUShort();
            this.Position = Extensions.DeserializeVector3(reader); 
            this.Rotation = Extensions.DeserializeVector3(reader);
            this.Scale = Extensions.DeserializeVector3(reader);
            this.ReservedVector1 = Extensions.DeserializeVector3(reader);
            this.ReservedVector2 = Extensions.DeserializeVector3(reader);
            reader.ReadUInt();
        }

        /// <summary>
        /// Writes this <see cref="MOBObject"/> into a <see cref="BinaryWriter"/>
        /// </summary>
        /// <param name="writer">The <see cref="BinaryWriter"/> to write to</param>
        public void Write(LittleEndianWriter writer)
        {
            writer.WriteBytes(Encoding.ASCII.GetBytes(this.Name.PadRight(60, '\u0000')));
            writer.WriteUShort((ushort)0);
            writer.WriteUShort((ushort)this.Type);
            this.Position.Serialize(writer);
            this.Rotation.Serialize(writer);
            this.Scale.Serialize(writer);
            this.ReservedVector1.Serialize(writer);
            this.ReservedVector2.Serialize(writer);
            writer.WriteByte(0);
        }
    }

    /// <summary>
    /// <see cref="MOBObject"/> types
    /// </summary>
    public enum MOBObjectType : ushort
    {
        /// <summary>
        /// Represents a <see cref="MOBObject"/> where minions spawn
        /// </summary>
        BarrackSpawn,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> where players spawn
        /// </summary>
        NexusSpawn,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that indicates the size of the map
        /// </summary>
        LevelSize,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is an Inhibitor
        /// </summary>
        Barrack,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is a Nexus
        /// </summary>
        Nexus,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is a Turret
        /// </summary>
        Turret,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is a Shop
        /// </summary>
        Shop,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is a Lake
        /// </summary>
        Lake,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that is a Navigation Waypoint
        /// </summary>
        Nav,
        /// <summary>
        /// Represents a <see cref="MOBObject"/> that provides certain information for the game
        /// </summary>
        Info,
        /// <summary>
        /// Represnts a <see cref="MOBObject"/> that is a Level Prop
        /// </summary>
        LevelProp
    };
}