using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol;
using ENet;
using Legends.World.Entities.AI;
using Legends.Core.Protocol.Game;
using System.Numerics;
using Legends.Core.DesignPattern;
using Legends.Core.Protocol.Messages.Game;

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
            foreach (var player in Units.Values.OfType<AIHero>())
            {
                player.Client.Send(message, channel, flags);
            }
        }
        public void AddUnit(Unit unit)
        {
            unit.TeamNo = Size + 1;
            Units.Add(unit.TeamNo, unit);
        }

        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit.TeamNo);
        }
        public Team GetOposedTeam()
        {
            return Id == TeamId.BLUE ? Game.PurpleTeam : Game.BlueTeam;
        }
        public bool HasVision(Unit player)
        {
            return VisibleUnits.Contains(player);
        }
        public Unit[] GetVisibleUnits()
        {
            return VisibleUnits.ToArray();
        }
        [InDeveloppement(InDeveloppementState.TEMPORARY)]
        private void OnTeamEnterVision(Unit unit)
        {
            if (unit.IsMoving)
            {
                AIUnit attackableUnit = (AIUnit)unit;
                Send(new EnterVisionMessage(false, unit.NetId, unit.Position, attackableUnit.WaypointsCollection.WaypointsIndex, attackableUnit.WaypointsCollection.GetWaypoints(), Game.Map.Record.MiddleOfMap));
            }
            else
            {
                Send(new EnterVisionMessage(false, unit.NetId, unit.Position, 1, new Vector2[] { unit.Position, unit.Position }, Game.Map.Record.MiddleOfMap));
            }
        }
        private void OnTeamLeaveVision(Unit unit)
        {
            Send(new LeaveVisionMessage(unit.NetId));
        }
        public void Update(float deltaTime)
        {
            foreach (var opponent in GetOposedTeam().Units.Values)
            {
                bool visible = false;

                foreach (var unit in Units.Values)
                {
                    if (unit.PerceptionBubbleRadius > 0)
                    {
                        if (unit.InFieldOfView(opponent))
                        {
                            visible = true;
                            if (!VisibleUnits.Contains(opponent))
                            {
                                OnTeamEnterVision(opponent);
                                unit.OnUnitEnterVision(opponent);
                                VisibleUnits.Add(opponent);
                            }
                        }

                    }

                }

                if (visible == false)
                {
                    if (VisibleUnits.Contains(opponent))
                    {
                        VisibleUnits.Remove(opponent);

                        OnTeamLeaveVision(opponent);

                        foreach (var unit in Units.Values)
                        {
                          
                            unit.OnUnitLeaveVision(opponent);
                        }
                    }
                }
            }
        }

    }
}
