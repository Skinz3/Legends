using Legends.Core.Protocol.Enum;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells
{
    public class Damages
    {
        public AttackableUnit Source
        {
            get;
            private set;
        }
        public AttackableUnit Target
        {
            get;
            private set;
        }
        public float Amount
        {
            get;
            private set;
        }
        public DamageType Type
        {
            get;
            private set;
        }
        public DamageResultEnum Result
        {
            get;
            private set;
        }
        public Damages(AttackableUnit source, AttackableUnit target, float amount, DamageType type, DamageResultEnum result)
        {
            this.Source = source;
            this.Target = target;
            this.Amount = amount;
            this.Type = type;
            this.Result = result;
        }
    }
}
