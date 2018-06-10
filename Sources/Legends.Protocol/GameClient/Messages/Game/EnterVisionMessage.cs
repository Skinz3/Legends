using Legends.Core.Geometry;
using Legends.Core.IO;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Messages.Game
{
    /// <summary>
    /// Apparition d'un object dans le champ de vision
    /// </summary>
    public class EnterVisionMessage : BaseMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_ObjectSpawn;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_S2C;
        public override Channel Channel => CHANNEL;

        public Vector2 position;
        public Vector2[] waypoints;
        public Vector2 middleOfMap;
        public int waypointsIndex;
        public bool spawn;

        public EnterVisionMessage()
        {

        }
        public EnterVisionMessage(bool spawn, uint netId, Vector2 position, int waypointsIndex, Vector2[] waypoints, Vector2 middleOfMap) : base(netId)
        {
            this.spawn = spawn;
            this.position = position;
            this.waypoints = waypoints;
            this.middleOfMap = middleOfMap;
            this.waypointsIndex = waypointsIndex;

       
        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }
        private void SerializeMoving(LittleEndianWriter writer)
        {

            writer.WriteShort((short)0); // extraInfo
            writer.WriteByte((byte)0); //c.getInventory().getItems().size(); // itemCount?
            //buffer.Write((short)7; // unknown

            /*
            for (int i = 0; i < c.getInventory().getItems().size(); i++) {
               ItemInstance* item = c.getInventory().getItems()[i];

               if (item != 0 && item.getTemplate() != 0) {
                  buffer.Write((short)item.getStacks();
                  buffer.Write((short)0; // unk
                  buffer.Write((int)item.getTemplate().getId();
                  buffer.Write((short)item.getSlot();
               }
               else {
                  buffer.fill(0, 7);
               }
            }
            */

            writer.Fill(0, 10);
            writer.WriteFloat((float)1.0f);
            writer.Fill(0, 13);

            writer.WriteByte((byte)2); // Type of data: Waypoints=2
            writer.WriteInt((int)Environment.TickCount); // unk

            writer.WriteByte((byte)((waypoints.Length - waypointsIndex + 1) * 2)); // coordCount
            writer.WriteUInt((uint)netId);
            writer.WriteByte((byte)0); // movement mask; 1=KeepMoving?
            writer.WriteShort(MovementVector.TargetXToNormalFormat(position.X, middleOfMap));
            writer.WriteShort(MovementVector.TargetYToNormalFormat(position.Y, middleOfMap));

            for (int i = waypointsIndex; i < waypoints.Length; ++i)
            {
                writer.WriteShort(MovementVector.TargetXToNormalFormat(waypoints[i].X, middleOfMap));
                writer.WriteShort(MovementVector.TargetXToNormalFormat(waypoints[i].Y, middleOfMap));
            }
        }
        public void SerializeStatic(LittleEndianWriter writer)
        {

            writer.Fill(0, 15);
            writer.WriteByte((byte)0x80); // unk
            writer.WriteByte((byte)0x3F); // unk
            writer.Fill(0, 13);
            writer.WriteByte((byte)3); // unk
            writer.WriteInt((int)1); // unk
            writer.WriteFloat(position.X);
            writer.WriteFloat(position.Y);
            writer.WriteFloat((float)0x3F441B7D); // z ?
            writer.WriteFloat((float)0x3F248DBB); // Rotation ?

        }

        public override void Serialize(LittleEndianWriter writer)
        {
            if (!spawn)
                SerializeMoving(writer);
            else
                SerializeStatic(writer);
        }
    }

}
