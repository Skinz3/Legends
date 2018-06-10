using Legends.Core.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Legends.Core.Protocol;
using System.Threading.Tasks;

namespace Legends.Protocol.GameClient.Other
{
    /// <summary>
    /// Used for MovementRequestMessage.cs WayPoints are point of the client pathfinding.
    /// </summary>
    public class WaypointsReader
    {
        public List<Vector2> Waypoints
        {
            get;
            private set;
        }
        public WaypointsReader(byte[] buffer, int count, Vector2 mapSize)
        {
            if (count % 2 > 0)
                count++;

            LittleEndianReader reader = new LittleEndianReader(buffer);

            BitArray mask = null;
            if (count > 2)
            {
                mask = new BitArray(reader.ReadBytes((count - 3) / 8 + 1));
            }

            var lastCoord = new Vector2(reader.ReadShort(), reader.ReadShort());
            var vMoves = new List<Vector2> { TranslateCoordinates(lastCoord, mapSize) };

            if (count < 3)
            {
                Waypoints = vMoves;
                return;
            }

            for (var i = 0; i < count - 2; i += 2)
            {
                if (mask[i])
                {
                    lastCoord.X += reader.ReadSByte();
                }
                else
                {
                    lastCoord.X = reader.ReadShort();
                }

                if (mask[i + 1])
                {
                    lastCoord.Y += reader.ReadSByte();
                }
                else
                {
                    lastCoord.Y = reader.ReadShort();
                }

                vMoves.Add(TranslateCoordinates(lastCoord, mapSize));
            }
            Waypoints = vMoves;
        }
        private Vector2 TranslateCoordinates(Vector2 vector, Vector2 mapSize)
        {
            // For ???? reason coordinates are translated to 0,0 as a map center, so we gotta get back the original
            // mapSize contains the real center point coordinates, meaning width/2, height/2
            return new Vector2(2 * vector.X + mapSize.X, 2 * vector.Y + mapSize.Y);
        }
    }
}
