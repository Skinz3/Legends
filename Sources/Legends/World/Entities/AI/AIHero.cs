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
using Legends.World.Entities.Statistics.Replication;

namespace Legends.World.Entities.AI
{
    public class AIHero : AIUnit
    {
        public const float DEFAULT_START_GOLD = 475;
        public const float DEFAULT_COOLDOWN_REDUCTION = 0f;

       

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
        public bool ReadyToStart
        {
            get;
            set;
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
        public Score Score
        {
            get;
            private set;
        }
        public bool Disconnected
        {
            get;
            private set;
        }
        public override string Name => Data.Name;

        public override float PerceptionBubbleRadius => ((HeroStats)Stats).PerceptionBubbleRadius.Total;

        public override bool IsAttackAutomatic => false;

        private DeathTimer DeathTimer
        {
            get;
            set;
        }
        public AIHero(LoLClient client, PlayerData data)
        {
            Client = client;
            Data = data;
            Disconnected = false;
        }

        public override void Initialize()
        {
            Record = AIUnitRecord.GetAIUnitRecord(Data.ChampionName);
            Champion = ChampionProvider.Instance.GetChampion(this, (ChampionEnum)Enum.Parse(typeof(ChampionEnum), Data.ChampionName));
            Stats = new HeroStats(Record, Data.SkinId);
            Model = Data.ChampionName;
            DeathTimer = new DeathTimer(this);
            SkinId = Data.SkinId;
            Score = new Score();
            base.Initialize();
        }
        [InDeveloppement(InDeveloppementState.TODO, "Skill points")]
        public void AddExperience(float value)
        {
            int oldLevel = AIStats.Level;
            AIStats.AddExperience(value);

            if (AIStats.Level > oldLevel)
            {
                int diff = AIStats.Level - oldLevel;

                AIStats.Health.BaseBonus += (float)Record.HpPerLevel;
                AIStats.Mana.BaseBonus += (float)Record.MpPerLevel;
                AIStats.HpRegeneration.BaseBonus += (float)Record.HpRegenPerLevel;
                AIStats.ManaRegeneration.BaseBonus += (float)Record.MPRegenPerLevel;
                AIStats.AttackDamage.BaseBonus += (float)Record.DamagePerLevel;
                AIStats.Armor.BaseBonus += (float)Record.ArmorPerLevel;
                AIStats.AbilityPower.BaseBonus += (float)Record.AbilityPowerIncPerLevel;
                AIStats.AttackSpeed.BaseBonus += (float)Record.AttackSpeedPerLevel;
                AIStats.CriticalHit.BaseBonus += (float)Record.CritPerLevel;
                AIStats.MagicResistance.BaseBonus += (float)Record.MagicResistPerLevel;
            }

            Game.Send(new LevelUpMessage(NetId, (byte)AIStats.Level, 0)); // tdoo
            UpdateStats();
        }
        public override void OnDead(AttackableUnit source) // we override base
        {
            AIStats.Health.Current = 0;
            AIStats.Mana.Current = 0;
            UpdateStats();
            Alive = false;
            Score.DeathCount++;
            DeathTimer.OnDead();
            Game.Send(new ChampionDieMessage(500, NetId, source.NetId, DeathTimer.TimeLeftSeconds));
            Game.UnitAnnounce(UnitAnnounceEnum.Death, NetId, source.NetId, new int[0]);
            Client.Send(new ChampionDeathTimerMessage(NetId, DeathTimer.TimeLeftSeconds));
        }
        public void OnRevive()
        {
            AIStats.Health.Current = AIStats.Health.Total;
            AIStats.Mana.Current = AIStats.Mana.Total;
            Alive = true;
            Position = SpawnPosition;
            Game.Send(new ChampionRespawnMessage(NetId, Position));
            UpdateStats();
        }
        public void DebugMessage(string content)
        {
            Client.Send(new DebugMessage(NetId, content));
        }
        public override void Update(long deltaTime)
        {
            base.Update(deltaTime);
            DeathTimer.Update(deltaTime);
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
            Game.UnitAnnounce(UnitAnnounceEnum.SummonerLeft, NetId, 0, new int[0]);
        }

       
    }
}
