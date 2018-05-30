using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.IO;
using System.Numerics;

namespace Legends.Core.Protocol.Game
{
    /// <summary>
    /// omfg 
    /// </summary>
    public class MovementAnswerMessage : GameMessage
    {
        public static PacketCmd PACKET_CMD = PacketCmd.PKT_S2C_MoveAns;
        public override PacketCmd Cmd => PACKET_CMD;

        public static Channel CHANNEL = Channel.CHL_LOW_PRIORITY;
        public override Channel Channel => CHANNEL;

        public Vector2[] wayPoints;
        public int actorNetId;
        public Vector2 mapSize;

        public MovementAnswerMessage(int netId, int ticks, Vector2[] wayPoints, int actorNetId, Vector2 mapSize) : base(netId, ticks)
        {
            this.wayPoints = wayPoints;
            this.actorNetId = actorNetId;
            this.mapSize = mapSize;
        }
        public MovementAnswerMessage()
        {

        }
        public override void Deserialize(LittleEndianReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(LittleEndianWriter writer)
        {
            writer.WriteShort(1);
            byte coordCount = (byte)(2 * wayPoints.Length);
            writer.WriteByte(coordCount);
            writer.WriteInt(actorNetId);
            int startPos = writer.Position;

            var maskSize = (coordCount + 5) / 8;

            for (int i = 0; i < maskSize; i++)
            {
                writer.WriteByte(0);
            }
            var lastCoord = new Vector2();
            for (int i = 0; i < wayPoints.Length; i++)
            {
                var curVector = new Vector2((wayPoints[i].X - mapSize.X) / 2, (wayPoints[i].Y - mapSize.Y) / 2);
                var relative = new Vector2(curVector.X - lastCoord.X, curVector.Y - lastCoord.Y);
                var isAbsolute = new Tuple<bool, bool>(
                      i == 0 || relative.X < sbyte.MinValue || relative.X > sbyte.MaxValue,
                      i == 0 || relative.Y < sbyte.MinValue || relative.Y > sbyte.MaxValue);

                var data1 = SetBitmaskValue(writer.Data, startPos, (2 * i - 2), !isAbsolute.Item1);
                writer.Clear();
                writer.WriteBytes(data1);


                if (isAbsolute.Item1)
                    writer.WriteShort((short)curVector.X);
                else
                    writer.WriteByte((byte)relative.X);

                var data2 = SetBitmaskValue(writer.Data, startPos, (2 * i - 1), !isAbsolute.Item2);
                writer.Clear();
                writer.WriteBytes(data2);

                if (isAbsolute.Item2)
                    writer.WriteShort((short)curVector.Y);
                else
                    writer.WriteByte((byte)relative.Y);

                lastCoord = curVector;
            }
        }
        public static byte[] SetBitmaskValue(byte[] mask, int startPos, int pos, bool val)
        {
            if (pos < 0)
                return mask;

            if (val)
                mask[startPos + (pos / 8)] |= (byte)(1 << (pos % 8));
            else
                mask[startPos + (pos / 8)] &= (byte)(~(1 << (pos % 8)));

            return mask;
        }

    }
}
