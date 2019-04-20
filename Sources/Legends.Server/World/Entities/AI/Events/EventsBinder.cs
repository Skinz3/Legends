using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Events
{
    public class EventsBinder
    {
        public event Action<Vector2[]> EvtStartMoving;

        public event Action<Spell, Vector2, Vector2> EvtSpellStartCasting;

        public event Action<Damages> EvtDamagesInflicted;



        public void OnDamagesInflicted(Damages damages)
        {
            EvtDamagesInflicted?.Invoke(damages);
        }
        public void OnStartMoving(Vector2[] vector2)
        {
            EvtStartMoving?.Invoke(vector2);
        }
        public void OnSpellStartCasting(Spell spell, Vector2 position, Vector2 endPosition)
        {
            EvtSpellStartCasting?.Invoke(spell, position, endPosition);
        }
    }
}
