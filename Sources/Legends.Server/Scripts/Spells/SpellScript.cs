using Legends.Core.CSharp;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.World;
using Legends.World.Entities.AI;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Spells
{
    public abstract class SpellScript : Script, IUpdatable
    {
        protected AIUnit Unit
        {
            get;
            private set;
        }
        protected SpellRecord SpellRecord
        {
            get;
            private set;
        }
        protected Spell Spell
        {
            get;
            private set;
        }
        public SpellScript(AIUnit owner, SpellRecord spellRecord)
        {
            this.Unit = owner;
            this.SpellRecord = spellRecord;
        }
        public void Bind(Spell spell)
        {
            this.Spell = spell;
        }

        public virtual void Update(float deltaTime)
        {

        }
        public abstract void OnStartCasting(Vector2 position, Vector2 endPosition);

        public abstract void OnFinishCasting(Vector2 position, Vector2 endPosition);


        public void AddProjectile(string name, Vector2 toPosition, Vector2 endPosition)
        {
            var record = SpellRecord.GetSpell(name);

            var position = new Vector3(toPosition.X, toPosition.Y, 0);
            var casterPosition = Unit.GetPositionVector3();
            var direction = new Vector3(2, 2, 0);
            var velocity = new Vector3(1, 1, 1) * 100;
            var startPoint = new Vector3(Unit.Position.X, Unit.Position.Y, 0);
            var endPoint = new Vector3(endPosition.X, endPosition.Y, 0);


            var castInfo = Spell.GetCastInformations(position, endPoint, name);

            Unit.Game.Send(new SpawnProjectileMessage(Unit.NetId, position, casterPosition, direction,
               velocity, startPoint, endPoint, casterPosition, 0, record.MissileSpeed, 1f, 1f, 1f, false, castInfo));

        }
    }
}
