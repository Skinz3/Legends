using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    public sealed class Health : Stat
    {
        public float Current
        {
            get;
            set;
        }

        private float _baseBonus;
        private float _percentBaseBonus;
        private float _flatBonus;
        private float _percentBonus;

        public override float BaseBonus
        {
            get => _baseBonus;
            set
            {
                if (value == _baseBonus)
                {
                    return;
                }

                var percentHp = Current / Total;
                _baseBonus = value;
                Current = Total * percentHp;
            }
        }

        public override float PercentBaseBonus
        {
            get => _percentBaseBonus;
            set
            {
                if (value == _percentBaseBonus)
                {
                    return;
                }

                var percentHp = Current / Total;
                _percentBaseBonus = value;
                Current = Total * percentHp;
            }
        }

        public override float FlatBonus
        {
            get => _flatBonus;
            set
            {
                if (value == _flatBonus)
                {
                    return;
                }

                var percentHp = Current / Total;
                _flatBonus = value;
                Current = Total * percentHp;
            }
        }

        public override float PercentBonus
        {
            get => _percentBonus;
            set
            {
                if (value == _percentBonus)
                {
                    return;
                }

                var percentHp = Current / Total;
                _percentBonus = value;
                Current = Total * percentHp;
            }
        }

        public Health(float baseValue) : base(baseValue, 0)
        {
            Current = baseValue;
        }
    }
}
