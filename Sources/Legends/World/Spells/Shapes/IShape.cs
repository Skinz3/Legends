using Legends.World.Entities;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Spells.Shapes
{
    public interface IShape 
    {
        bool Collide(AttackableUnit target);
    }
}
