using Legends.Configurations;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Game;
using Legends.Core.Protocol.LoadingScreen;
using Legends.Core.Protocol.Messages.Game;
using Legends.Core.Protocol.Other;
using Legends.Network;
using Legends.Records;
using Legends.World.Champions;
using Legends.World.Games;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core.Protocol;
using Legends.Core.Utils;
using ENet;
using Legends.World.Entities.Statistics;
using Legends.Core.DesignPattern;

namespace Legends.World.Entities.AI
{
    public class AIHero : AIUnit
    {
        public const float DEFAULT_START_GOLD = 475;
        public const float DEFAULT_COOLDOWN_REDUCTION = 0f;
        public const float DEFAULT_PERCEPTION_BUBBLE_RADIUS = 1350f;

        public LoLClient Client
        {
            get;
            private set;
        }

        public int PlayerNo
        {
            get;
            set;
        }
        public PlayerData Data
        {
            get;
            private set;
        }

        public bool ReadyToSpawn
        {
            get;
            set;
        }

        public AIUnitRecord ChampionRecord
        {
            get;
            private set;
        }
        public Champion Champion
        {
            get;
            private set;
        }
        public PlayerStats PlayerStats
        {
            get
            {
                return Stats as PlayerStats;
            }
        }
        public bool Disconnected
        {
            get;
            private set;
        }
        public override string Name => Data.Name;

        public override float PerceptionBubbleRadius => ((PlayerStats)Stats).PerceptionBubbleRadius.Total;

        public AIHero(LoLClient client, PlayerData data)
        {
            Client = client;
            Data = data;
            Disconnected = false;
        }

        public override void Initialize()
        {
            ChampionRecord = AIUnitRecord.GetChampion(Data.ChampionName);
            Champion = ChampionManager.Instance.GetChampion(this, (ChampionEnum)Enum.Parse(typeof(ChampionEnum), Data.ChampionName));
            Stats = new PlayerStats(ChampionRecord, Data.SkinId);
            Model = Data.ChampionName;
            SkinId = Data.SkinId;
            base.Initialize();
        }
        public void DebugMessage(string content)
        {
            Client.Send(new DebugMessage(NetId, content));
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            WaypointsCollection.InterpolateMovement(deltaTime);
        }

        /// <summary>
        /// Envoit un message a tous les joueurs possédant la vision sur ce joueur.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="channel"></param>
        public void SendVision(Message message, Channel channel = Channel.CHL_S2C, PacketFlags flags = PacketFlags.Reliable)
        {
            Team.Send(message, channel, flags);

            Team oposedTeam = GetOposedTeam();

            if (oposedTeam.HasVision(this))
            {
                oposedTeam.Send(message);
            }
        }

        [InDeveloppement(InDeveloppementState.TEMPORARY, "For serialization reasons...")]
        public override void OnUnitEnterVision(Unit unit)
        {
            if (unit.IsMoving)
            {
                AIUnit attackableUnit = (AIUnit)unit;
                Client.Send(new EnterVisionMessage(false, unit.NetId, unit.Position, attackableUnit.WaypointsCollection.WaypointsIndex, attackableUnit.WaypointsCollection.GetWaypoints(), Game.Map.Record.MiddleOfMap));
            }
            else
            {
                Client.Send(new EnterVisionMessage(false, unit.NetId, unit.Position, 1, new Vector2[] { unit.Position, unit.Position }, Game.Map.Record.MiddleOfMap));
            }
        }
        public override void OnUnitLeaveVision(Unit unit)
        {
            Client.Send(new LeaveVisionMessage(unit.NetId));
        }
        public void UpdateInfos()
        {
            Game.Send(new PlayerInfoMessage(NetId, Data.Summoner1Spell, Data.Summoner2Spell));
        }

        public override void UpdateStats(bool partial = true)
        {
            PlayerStats.UpdateReplication(partial);
            Game.Send(new UpdateStatsMessage(0, NetId, PlayerStats.ReplicationManager.Values, partial));

            if (partial)
            {
                foreach (var x in PlayerStats.ReplicationManager.Values)
                {
                    if (x != null)
                    {
                        x.Changed = false;
                    }
                }
            }
        }
        [InDeveloppement(InDeveloppementState.STARTED)]
        public void OnDisconnect()
        {
            Disconnected = true;
            Game.RemoveUnit(this); // sure? 
            Game.UnitAnnounce(UnitAnnounceEnum.SummonerLeft, NetId);
        }
    }
}
