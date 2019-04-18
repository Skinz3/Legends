using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Particles
{
    public class FXManager
    {
        private Dictionary<uint, FX> FXS
        {
            get;
            set;
        }
        private AIUnit Owner
        {
            get;
            set;
        }
        public FXManager(AIUnit owner)
        {
            this.FXS = new Dictionary<uint, FX>();
            this.Owner = owner;
        }

        public void CreateFX(FX fx, bool add)
        {
            if (add)
                this.FXS.Add(fx.NetId, fx);
            this.Owner.NotifyFXCreated(fx);
        }
        public void DestroyFX(uint netId)
        {
            this.FXS.Remove(netId);
            this.Owner.NotifyFXDestroyed(netId);
        }
        public void DestroyFX(string name)
        {
            var netId = FXS.FirstOrDefault(x => x.Value.Name == name).Key;
            DestroyFX(netId);
        }
    }
}
