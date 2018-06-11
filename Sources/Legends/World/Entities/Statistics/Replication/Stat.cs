using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public class Stat : StatMod
    {
        /// <summary>
        /// Base value of the stat. (obvious)
        /// </summary>
        public float BaseValue
        {
            get;
            protected set;
        }

        public float MinLimit
        {
            get;
            private set;
        }
        public float MaxLimit
        {
            get;
            private set;
        }

        public Stat(float value, float minLimit = float.NegativeInfinity, float maxLimit = float.PositiveInfinity)
        {
            BaseValue = value;
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }

        public Stat() : this(0)
        {

        }
        public float TotalSafe
        {
            get
            {
                float total = Total;
                if (total < MinLimit)
                {
                    return MinLimit;
                }
                return Math.Min(total, MaxLimit);
            }
        }
        public virtual float Total
        {
            get
            {
                var flat = BaseBonus + FlatBonus;
                var percent = PercentBaseBonus + PercentBonus;
                return BaseValue + flat + (flat * percent);
                
            }
        }

        public static explicit operator float(Stat s)
        {
            return s.TotalSafe;
        }
        public virtual void SetBaseValue(float baseValue)
        {
            this.BaseValue = baseValue;
        }
        public void AddBaseValue(float value)
        {
            this.BaseValue += value;
        }
        public void ApplyModifier(StatMod modifier)
        {
            BaseBonus += modifier.BaseBonus;
            PercentBaseBonus += modifier.PercentBaseBonus;
            FlatBonus += modifier.FlatBonus;
            PercentBonus += modifier.PercentBonus;
        }

        public void RemoveModifier(StatMod modifier)
        {
            BaseBonus -= modifier.BaseBonus;
            PercentBaseBonus -= modifier.PercentBaseBonus;
            FlatBonus -= modifier.FlatBonus;
            PercentBonus -= modifier.PercentBonus;
        }
        public override string ToString()
        {
            return string.Format("({0})", TotalSafe);
        }
    }
}
