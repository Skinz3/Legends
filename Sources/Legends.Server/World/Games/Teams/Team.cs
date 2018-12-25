using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.World.Entities;
using Legends.Protocol.GameClient.Enum;
using Legends.Core.Protocol;
using ENet;
using Legends.World.Entities.AI;
using System.Numerics;
using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Games.Maps.Fog;
using Legends.Network;
using Legends.World.Entities.Buildings;

namespace Legends.World.Games
{
    public abstract class Team : IUpdatable
    {
        public abstract TeamId Id
        {
            get;
        }
        public Dictionary<int, Unit> Units
        {
            get;
            set;
        }
        public Unit[] AliveUnits
        {
            get
            {
                return Array.FindAll(Units.Values.OfType<Unit>().ToArray(), x => x.Alive);
            }
        }
        public int Size
        {
            get
            {
                return Units.Count;
            }
        }
        /// <summary>
        /// Dictionary (VisibleUnit,VisibleByUnit)
        /// </summary>
        private Dictionary<Unit, Unit> VisibleUnits
        {
            get;
            set;
        }
        protected Game Game
        {
            get;
            private set;
        }
        private List<FogUpdate> FogUpdates
        {
            get;
            set;
        }
        public Team(Game game)
        {
            this.Units = new Dictionary<int, Unit>();
            this.VisibleUnits = new Dictionary<Unit, Unit>();
            this.Game = game;
            this.FogUpdates = new List<FogUpdate>();
        }
        public virtual void Send(Message message, Channel channel = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            foreach (var player in Units.Values.OfType<AIHero>())
            {
                player.Client.Send(message, channel, flags);
            }
        }
        public void AddUnit(Unit unit)
        {
            unit.TeamNo = Units.Count == 0 ? 1 : Units.Keys.OrderByDescending(x => x).First() + 1;
            Units.Add(unit.TeamNo, unit);
        }

        public void Iteration<T>(Action<T> action) where T : Unit
        {
            foreach (var unit in Units.Values.OfType<T>())
            {
                action(unit);
            }
        }

        public T[] GetUnits<T>(Predicate<T> predicate)
        {
            return Array.FindAll<T>(Units.Values.OfType<T>().ToArray(), predicate);
        }
        public T GetUnit<T>(Func<T, bool> predicate) where T : Unit
        {
            return Units.Values.OfType<T>().FirstOrDefault(predicate);
        }
        public T GetUnit<T>(string name) where T : Unit
        {
            return GetUnit<T>(x => x.Name == name);
        }
        public T GetUnit<T>(uint netId) where T : Unit
        {
            return GetUnit<T>(x => x.NetId == netId);
        }
        public void RemoveUnit(Unit unit)
        {
            Units.Remove(unit.TeamNo);
        }

        public abstract Team[] GetOposedTeams();

        public bool HasVision(Unit player)
        {
            return VisibleUnits.ContainsKey(player);
        }
        public Unit[] GetVisibleUnits()
        {
            return VisibleUnits.Keys.ToArray();
        }
        [InDevelopment(InDevelopmentState.TEMPORARY)]
        private void OnTeamEnterVision(Unit unit)
        {
            Send(new OnEnterVisiblityClientMessage(unit.NetId, unit.GetVisibilityData()));
        }
        private void OnTeamLeaveVision(Unit unit)
        {
            Send(new LeaveVisionMessage(unit.NetId));
        }
        public void InitializeFog()
        {
            foreach (var unit in Array.FindAll(Units.Values.ToArray(), x => x.AddFogUpdate))
            {
                AddFogUpdate(new FogUpdate(Game.NetIdProvider.PopNextNetId(), Id, unit));
            }
        }
        private void AddFogUpdate(FogUpdate fogUpdate)
        {
            this.Send(new FogUpdate2Message(Id, fogUpdate.Source.NetId,
                fogUpdate.Source.Position, fogUpdate.NetId, fogUpdate.Source.PerceptionBubbleRadius));

            FogUpdates.Add(fogUpdate);
        }
        protected void UpdateVision(float deltaTime, Unit[] targetedUnits)
        {
            foreach (var opponent in targetedUnits)
            {
                bool visible = false;

                foreach (var unit in AliveUnits)
                {
                    if (unit.PerceptionBubbleRadius > 0)
                    {
                        if (unit.IsVisible(opponent))
                        {
                            visible = true;
                            if (!VisibleUnits.ContainsKey(opponent))
                            {
                                OnTeamEnterVision(opponent);
                                unit.OnUnitEnterVision(opponent);
                                VisibleUnits.Add(opponent, unit);
                            }
                        }

                    }

                }

                if (visible == false)
                {
                    if (VisibleUnits.ContainsKey(opponent))
                    {
                        var visibleBy = VisibleUnits[opponent];

                        VisibleUnits.Remove(opponent);

                        OnTeamLeaveVision(opponent);

                        visibleBy.OnUnitLeaveVision(opponent);
                    }
                }
            }
        }
        public abstract void Update(float deltaTime);

        public void Initialize()
        {
            foreach (var unit in Units.Values)
            {
                unit.Initialize();
            }
        }
    }
}
