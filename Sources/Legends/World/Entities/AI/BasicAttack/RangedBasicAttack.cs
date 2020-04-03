﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;
using Legends.Core.DesignPattern;
using Legends.World.Spells.Projectiles;
using Legends.Records;
using Legends.Core.Utils;
using Legends.World.Games;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class RangedBasicAttack : BasicAttack
    {
        static Logger logger = new Logger();

        private TargetedProjectile Projectile
        {
            get;
            set;
        }

        public RangedBasicAttack(AIUnit unit, AttackableUnit target,
            bool critical, bool first, AttackSlotEnum slot) : base(unit, target, critical, first, slot)
        {


        }
        private void CastProjectile()
        {
            SpellRecord basicAttackRecord = Unit.Record.BasicAttack;

            if (basicAttackRecord == null)
            {
                logger.Write("No basic attack data for unit " + Unit.Name, MessageState.WARNING);
                return;
            }
            if (basicAttackRecord.MissileSpeed == 0)
            {
                logger.Write("We wont spawn a projectile with 0 speed!", MessageState.WARNING);
                return;
            }
            this.Projectile = new TargetedProjectile(Unit.Game.NetIdProvider.Pop(), Unit, Target, Unit.Position, basicAttackRecord.MissileSpeed, OnReach);
            Unit.GetAttackManager<RangedManager>().AddProjectile(Projectile);
        }
        protected override float GetAutocancelDistance()
        {
            return (float)Unit.GetAutoattackRange(Target) + 120f;
        }
        private void OnReach(AttackableUnit target, Projectile projectile)
        {
            if (Target.Alive)
            {
                InflictDamages();
            }

            Unit.GetAttackManager<RangedManager>().RemoveProjectile(Projectile);
            Projectile = null;
        }
        protected override void OnCancel()
        {

        }

        protected override void OnCastTimeReach()
        {
            CastProjectile();
        }
    }

}
