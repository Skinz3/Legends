﻿using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.IO.NavGrid;
using Legends.Core.Utils;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World.Entities.AI.Events;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
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
        static Logger logger = new Logger();

        public const float DEFAULT_MODEL_SIZE = 1f;

        public uint NetId
        {
            get;
            set;
        }
        public EventsBinder EventsBinder
        {
            get;
            set;
        }
        public Team Team
        {
            get;
            private set;
        }
        public MapCellRecord Cell
        {
            get
            {
                return Game.Map.Record.GetCell(Position);
            }
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
        public Vector2 CellPosition
        {
            get
            {
                return Game.Map.Record.TranslateFromNavGrid(Cell.X, Cell.Y);
            }
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
        public Vector3 GetPositionVector3()
        {
            return new Vector3(Position.X, Position.Y, Game.Map.Record.GetZ(Position));
        }
        public bool Disposed
        {
            get;
            set;
        }
        public bool ObjectAvailable
        {
            get
            {
                return !Disposed && !PendingDispose;
            }
        }
        public bool PendingDispose
        {
            get;
            set;
        }

        public Unit(uint netId)
        {
            this.NetId = netId;
            this.Alive = true;
            this.Disposed = false;
            this.EventsBinder = new EventsBinder();

        }
        public virtual void Initialize()
        {

        }

        public abstract VisibilityData GetVisibilityData();


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
        public virtual void Update(float deltaTime)
        {

        }

        public abstract void OnUnitEnterVision(Unit unit);

        public abstract void OnUnitLeaveVision(Unit unit);


        public bool IsVisible(Unit other)
        {
            bool fov = this.GetDistanceTo(other) <= PerceptionBubbleRadius;
            // bool inBrush = other.CellHasFlag(NavigationGridCellFlags.HasGrass);
            return fov;//&& !inBrush;

        }
        public bool CellHasFlag(NavigationGridCellFlags flags)
        {
            return Cell.HasFlag(Game.Map.Record, flags);
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
        public bool IsFriendly(Unit other)
        {
            return Team == other.Team;
        }
        public override string ToString()
        {
            return Name;
        }

        public virtual void OnGameStart()
        {

        }
    }
}
