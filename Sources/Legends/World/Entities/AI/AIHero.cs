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
using Legends.Core.Protocol.Messages;

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
        public HeroStats PlayerStats
        {
            get
            {
                return Stats as HeroStats;
            }
        }
        public bool Disconnected
        {
            get;
            private set;
        }
        public override string Name => Data.Name;

        public override float PerceptionBubbleRadius => ((HeroStats)Stats).PerceptionBubbleRadius.Total;

        public AIHero(LoLClient client, PlayerData data)
        {
            Client = client;
            Data = data;
            Disconnected = false;
        }

        public override void Initialize()
        {
            ChampionRecord = AIUnitRecord.GetAIUnitRecord(Data.ChampionName);
            Champion = ChampionManager.Instance.GetChampion(this, (ChampionEnum)Enum.Parse(typeof(ChampionEnum), Data.ChampionName));
            Stats = new HeroStats(ChampionRecord, Data.SkinId);
            Model = Data.ChampionName;
            SkinId = Data.SkinId;
            base.Initialize();
        }
        public void AddVision(AIUnit source)
        {
            Client.Send(new FogUpdate2Message(Team.Id, source.NetId, source.Position, NetIdProvider.PopNextNetId(), source.PerceptionBubbleRadius));
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

        public override void OnUnitEnterVision(Unit unit)
        {

        }
        public override void OnUnitLeaveVision(Unit unit)
        {

        }
        public void UpdateInfos()
        {
            Game.Send(new PlayerInfoMessage(NetId, Data.Summoner1Spell, Data.Summoner2Spell));
        }
        public void AttentionPing(Vector2 position, int targetNetId, PingTypeEnum pingType)
        {
            Team.Send(new AttentionPingAnswerMessage(position, targetNetId, NetId, pingType));
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
            Game.RemoveUnit(this); // maybe depend of reconnect system
            Game.UnitAnnounce(UnitAnnounceEnum.SummonerLeft, NetId);
        }


    }
}
