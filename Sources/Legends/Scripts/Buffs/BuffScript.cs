using Legends.Core;
using Legends.Core.CSharp;
using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.Utils;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Protocol.GameClient.Types;
using Legends.Records;
using Legends.World;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Entities.AI.Particles;
using Legends.World.Games.Delayed;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
using Legends.World.Spells.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Spells
{
    public abstract class BuffScript : Script, IUpdatable
    {
        protected AIUnit Target
        {
            get;
            private set;
        }
        protected AIUnit Source
        {
            get;
            set;
        }
        public abstract BuffTypeEnum BuffType
        {
            get;
        }
        public abstract string BuffName
        {
            get;
        }
        public abstract float MaxDuration
        {
            get;
        }
        public float Duration
        {
            get;
            private set;
        }
        public bool Ended
        {
            get
            {
                return Duration <= 0;
            }
        }
        public byte Slot
        {
            get;
            set;
        }

        public BuffScript(AIUnit source, AIUnit target)
        {
            this.Duration = MaxDuration;
            this.Source = source;
            this.Target = target;
        }
        public abstract void OnAdded();

        public abstract void OnRemoved();

        public void Update(float deltaTime)
        {
            Duration -= deltaTime;
        }

        protected void DestroyFX(uint netId)
        {
            Target.FXManager.DestroyFX(netId);
        }
        protected void DestroyFX(string name)
        {
            Target.FXManager.DestroyFX(name);
        }
        protected void DestroyFX(AIUnit unit, string name)
        {
            unit.FXManager.DestroyFX(name);
        }
        public void CreateFX(string effectName, string bonesName, float size, AIUnit target, bool add)
        {
            uint netId = Target.Game.NetIdProvider.Pop();
            FX fx = new FX(netId, effectName, bonesName, size, Target, target);
            target.FXManager.CreateFX(fx, add);
        }
        public void CreateFX(string effectName, string bonesName, float size, Vector2 targetPosition)
        {
            uint netId = Target.Game.NetIdProvider.Pop();
            FX fx = new FX(netId, effectName, bonesName, size, Target, targetPosition);
            Target.FXManager.CreateFX(fx, false);
        }

        public void CreateFXs(FX[] fxs, AIUnit target, bool add)
        {
            target.FXManager.CreateFXs(fxs, add);
        }

    }
}
