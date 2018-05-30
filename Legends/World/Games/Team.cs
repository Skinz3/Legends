using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol;
using ENet;

namespace Legends.World.Games
{
    public class Team
    {
        public TeamId Id
        {
            get;
            private set;
        }
        public Dictionary<int, Unit> Units
        {
            get;
            set;
        }
        public int Size
        {
            get
            {
                return Units.Count;
            }
        }
        private List<Unit> VisibleUnits
        {
            get;
            set;
        }
        private Game Game
        {
            get;
            set;
        }
        public Team(Game game, TeamId id)
        {
            this.Id = id;
            this.Units = new Dictionary<int, Unit>();
            this.VisibleUnits = new List<Unit>();
            this.Game = game;
        }
        public void Send(Message message, Channel channel = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            foreach (var player in Units.Values.OfType<Player>())
            {
                player.Client.Send(message, channel, flags);
            }
        }
        public void AddUnit(Unit unit)
        {
            unit.TeamNo = Size + 1;
            Units.Add(unit.TeamNo, unit);
        }
        public Team GetOposedTeam()
        {
            return Id == TeamId.BLUE ? Game.PurpleTeam : Game.BlueTeam;
        }
        public bool HasVision(Unit player)
        {
            return VisibleUnits.Contains(player);
        }
        public void Update(float deltaTime)
        {
            foreach (var opponent in GetOposedTeam().Units.Values)
            {
                foreach (var unit in Units.Values)
                {
                    if (unit.PerceptionBubbleRadius > 0)
                    {
                        if (unit.InFieldOfView(opponent))
                        {
                            if (!VisibleUnits.Contains(opponent))
                            {
                                VisibleUnits.Add(opponent);
                                unit.OnUnitEnterVision(opponent);
                            }
                        }
                        else
                        {
                            if (VisibleUnits.Contains(opponent))
                            {
                                VisibleUnits.Remove(opponent);
                                unit.OnUnitLeaveVision(opponent);
                            }
                        }
                    }

                }
            }
        }


    }
}
