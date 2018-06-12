using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using Legends.Protocol.GameClient.Enum;
using Legends.World.Spells;
using Legends.World.Entities.Statistics.Replication;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Entities.AI.Deaths;

namespace Legends.World.Entities.Buildings
{
    public class Inhibitor : AnimatedBuilding
    {
        /// <summary>
        /// Values from .cfg file is wrong (3000, 5500) 
        /// </summary>
        public const float INIBITOR_LIFE = 4000.0f;
        /// <summary>
        /// 5 Minutes
        /// </summary>
        public const float TIME_TO_RESPAWN_SECONDS = 5f * 60;
        /// <summary>
        /// 4 Minutes
        /// </summary>
        public const float TIME_TO_RESPAWNING_SOON_SECONDS = 4f * 60;

        public override bool AddFogUpdate => true;

        private InhibitorDeath Death
        {
            get;
            set;

        }
        public Inhibitor(uint netId, BuildingRecord buildingRecord, MapObjectRecord mapObjectRecord) : base(netId, buildingRecord, mapObjectRecord)
        {
            Death = new InhibitorDeath(this, TIME_TO_RESPAWNING_SOON_SECONDS);
        }
        public void Revive()
        {
            Death.Revive();
        }
        public override void Initialize()
        {
            base.Initialize();
            this.Stats.Health.SetBaseValue(INIBITOR_LIFE);
        }
        public override void UpdateHeath()
        {
            base.UpdateHeath();
        }
        public override void Update(long deltaTime)
        {
            Death.Update(deltaTime);
            base.Update(deltaTime);
        }
        public override void OnDead(AttackableUnit source)
        {
            Stats.IsTargetable = false; 
            UpdateStats();

            Game.UnitAnnounce(UnitAnnounceEnum.InhibitorDestroyed, NetId, source.NetId, new uint[0]);

            Death.OnDead();
            Game.Send(new InhibitorStateUpdateMessage(NetId, InhibitorStateEnum.Dead));
            base.OnDead(source);
        }
        public void OnRespawingSoon()
        {
            Game.UnitAnnounce(UnitAnnounceEnum.InhibitorAboutToSpawn, NetId, 0, new uint[0]);
        }
        public override void OnRevive(AttackableUnit source)
        {
            base.OnRevive(source);
            Game.UnitAnnounce(UnitAnnounceEnum.InhibitorSpawned, NetId, 0, new uint[0]);
            Stats.IsTargetable = true;
            this.Stats.Health.Current = this.Stats.Health.TotalSafe;
            UpdateStats();
            Game.Send(new InhibitorStateUpdateMessage(NetId, InhibitorStateEnum.Alive));
        }
    }
}
