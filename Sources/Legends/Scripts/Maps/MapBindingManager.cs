using Legends.Core.DesignPattern;
using Legends.Core.Utils;
using Legends.World.Entities;
using Legends.World.Games;
using Legends.World.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Scripts.Maps
{
    public class Binding : IInitializable
    {
        public bool AllDead
        {
            get;
            private set;
        }
        public AttackableUnit[] UnitsProtected
        {
            get;
            private set;
        }
        public AttackableUnit[] UnitsThatProtect
        {
            get;
            private set;
        }

        public Binding(AttackableUnit[] unitsThatProtect, AttackableUnit[] unitsProtected,bool alldead)
        {
            this.UnitsProtected = unitsProtected;
            this.UnitsThatProtect = unitsThatProtect;
            this.AllDead = alldead;
        }
        private bool IsUnitsProtectedTargetable()
        {
            if (AllDead)
            {
                return UnitsThatProtect.All(x => x.Alive == false);
            }
            else
            {
                return UnitsThatProtect.FirstOrDefault(x => x.Alive == false) != null;
            }
        }

        public void Initialize()
        {
            foreach (var unitThatIProtect in UnitsProtected)
            {
                unitThatIProtect.Stats.IsTargetable = IsUnitsProtectedTargetable();
                unitThatIProtect.UpdateStats();

                foreach (var unitThatProtect in UnitsThatProtect)
                {
                    unitThatProtect.OnDeadEvent += OnUnitThatProtectDie;
                    unitThatProtect.OnReviveEvent += OnUnitThatProtectRevive;
                }
            }
        }

        private void OnUnitThatProtectRevive(AttackableUnit arg1, Unit arg2)
        {
            foreach (var unit in UnitsProtected)
            {
                unit.Stats.IsTargetable = IsUnitsProtectedTargetable();
                unit.UpdateStats();
            }
        }

        private void OnUnitThatProtectDie(AttackableUnit dead, Unit source)
        {
            foreach (var unit in UnitsProtected)
            {
                unit.Stats.IsTargetable = IsUnitsProtectedTargetable();
                unit.UpdateStats();
            }
        }

    }
    public class MapBindingManager : IInitializable
    {
        Logger logger = new Logger();

        public List<Binding> Bindings
        {
            get;
            private set;
        }
        private Game Game
        {
            get;
            set;
        }
        public MapBindingManager(Game game, List<BindingDescription> bindings)
        {
            this.Game = game;
            this.Bindings = new List<Binding>();

            foreach (var binding in bindings)
            {
                var keys = Array.ConvertAll(binding.UnitsThatProtect, x => Game.GetUnit<AttackableUnit>(x));
                var values = Array.ConvertAll(binding.UnitsProtected, x => Game.GetUnit<AttackableUnit>(x));

                if (keys.All(x => x != null) && values.All(x => x != null))
                {
                    Bindings.Add(new Binding(keys, values, binding.AllDead));
                }
                else
                {
                    logger.Write("Unable to add binding to game, some unit names are not correct", MessageState.ERROR);
                }
            }
        }
        public void Initialize()
        {
            foreach (var binding in Bindings)
            {
                binding.Initialize();
            }
        }


    }
}
