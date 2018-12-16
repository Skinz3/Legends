using Legends.World.Entities.Statistics.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public sealed class AttackSpeed : Stat
    {
        public float AttackDelay
        {
            get;
            private set;
        }
        public float Ratio
        {
            get
            {
                return TotalSafe / BaseValue;
            }
        }
        public override float Total => BaseValue * PercentBonus * BaseBonus;

        public AttackSpeed(float attackDelay) : base(0.625f, 0.2f, 2.5f)
        {
            AttackDelay = attackDelay;
            BaseValue = 0.625f / (1 + AttackDelay);
            BaseBonus = 1;
            PercentBaseBonus = 1;
            PercentBonus = 1;
        }
    }
}
