using Legends.World.Entities.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Deaths
{
    public class InhibitorDeath : Death<Inhibitor>
    {
        private float RespawingSoonTimeSeconds
        {
            get;
            set;
        }
        private float DeltaRespawningSoonTime
        {
            get;
            set;
        }
        private bool AnnounceNotified
        {
            get;
            set;
        }
        public InhibitorDeath(Inhibitor unit, float respawingSoonTimeSeconds) : base(unit)
        {
            this.RespawingSoonTimeSeconds = respawingSoonTimeSeconds;
            this.DeltaRespawningSoonTime = respawingSoonTimeSeconds * 1000f;
        }
        public override void Update(float deltaTime)
        {
            if (!Unit.Alive && !AnnounceNotified)
            {
                DeltaRespawningSoonTime += deltaTime;

                if (DeltaRespawningSoonTime >= RespawingSoonTimeSeconds * 1000f)
                {
                    AnnounceNotified = true;
                    Unit.OnRespawingSoon();
                    DeltaRespawningSoonTime = 0;
                }
            }
            base.Update(deltaTime);
        }
        public override void Revive()
        {
            AnnounceNotified = false;
            base.Revive();
        }
        protected override float GetTimeLeftSeconds()
        {
            return Inhibitor.TIME_TO_RESPAWN_SECONDS;
        }
    }
}
