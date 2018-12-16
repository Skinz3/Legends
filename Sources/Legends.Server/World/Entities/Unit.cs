using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Entities.Movements;
using Legends.World.Games;
using Legends.World.Games.Maps;
using Legends.World.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities
{
    public abstract class Unit : IUpdatable, IInitializable
    {
        public const float DEFAULT_MODEL_SIZE = 1f;

        public uint NetId
        {
            get;
            set;
        }
        public Team Team
        {
            get;
            private set;
        }
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// Numero de l'unitée dans son équipe.
        /// </summary>
        public int TeamNo
        {
            get;
            set;
        }
        public abstract bool IsMoving
        {
            get;
        }
        public string Model
        {
            get;
            protected set;
        }
        public int SkinId
        {
            get;
            protected set;
        }
        public bool Alive
        {
            get;
            protected set;
        }
        public abstract bool AddFogUpdate
        {
            get;
        }
        public Vector2 Position
        {
            get;
            set;
        }
        public Vector2 SpawnPosition
        {
            get;
            set;
        }

        public Game Game
        {
            get;
            private set;
        }
        public abstract float PerceptionBubbleRadius
        {
            get;
        }
        public Unit(uint netId)
        {
            this.NetId = netId;
            this.Alive = true;
        }
        public virtual void Initialize()
        {

        }
        public virtual void UpdateModel(string newModel, bool updateSpells, int skinId)
        {
            Model = newModel;
            SkinId = skinId;
            Game.Send(new UpdateModelMessage(NetId, newModel, updateSpells, skinId));
        }
        /// <summary>
        /// Called by Game.AddUnit()
        /// </summary>
        /// <param name="team"></param>
        public void DefineTeam(Team team)
        {
            this.Team = team;
        }
        public void DefineGame(Game game)
        {
            this.Game = game;
        }
        public virtual void Update(long deltaTime)
        {

        }
        public abstract void OnUnitEnterVision(Unit unit);

        public abstract void OnUnitLeaveVision(Unit unit);

        public Team GetOposedTeam()
        {
            return Team.Id == TeamId.BLUE ? Game.PurpleTeam : Game.BlueTeam;
        }
        public bool InFieldOfView(Unit other)
        {
            var dist = this.GetDistanceTo(other);
            return dist <= PerceptionBubbleRadius;
        }
        public float GetDistanceTo(Unit other)
        {
            return Geo.GetDistance(Position, other.Position);
        }
        /// <summary>
        /// In Radians
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public float GetAngleBetween(Unit other)
        {
            return Geo.GetAngle(Position, other.Position);
        }
        public bool HasVision(Unit other)
        {
            return Team.HasVision(other);
        }
        public override string ToString()
        {
            return Name;
        }


    }
}
