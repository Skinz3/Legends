using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.BasicAttack
{
    public class MeleeManager : AttackManager
    {
        public MeleeManager(AIUnit unit) : base(unit)
        {


        }

        public override void BeginAttackTarget(AttackableUnit target)
        {
            if (IsAttacking && target != CurrentAutoattack.Target) // Si la cible que l'on attaque est differente de target
            {
                StopAttackTarget(); // alors on cancel l'anim quoi qu'il arrive

                if (CurrentAutoattack.Hit) // Si l'attaque précédante a touchée
                {
                    CurrentAutoattack.OnBasicAttackEnded = new Func<BasicAttack, bool>((BasicAttack attack) => // a la fin du delai l'attaque, on changera de cible (pas de cancel d'anim sur l'anim de l'auto).
                    {
                        DestroyAutoattack();
                        Unit.TryBasicAttack(target);
                        return true;
                    });
                }
                else // Sinon on peut directment passer a la nouvelle auto
                {
                    DestroyAutoattack();
                    Unit.TryBasicAttack(target);
                }
            }
            if (IsAttacking == false) // osef, facile
            {
                bool critical = target.Stats.IsCriticalImmune ? false : Unit.Stats.CriticalStrike();
                CurrentAutoattack = new MeleeBasicAttack(Unit, target, critical);
                CurrentAutoattack.Notify();
                Unit.OnTargetSet(target);
            }
            else if (IsAttacking == true && CurrentAutoattack.Hit && target == CurrentAutoattack.Target)
            { // Si l'on attaquait la cible, que l'ancienne auto a été cancel mais qu'elle a touchée, et que la cible n'a pas changée
                CurrentAutoattack.OnBasicAttackEnded = new Func<BasicAttack, bool>((BasicAttack attack) =>
                 {
                     // Unit.AttackManager.StopAttackTarget();
                     Unit.AttackManager.DestroyAutoattack();
                     Unit.TryBasicAttack(target);

                     return true;

                 });

            }
        }

        public override void NextAutoattack()
        {
            bool critical = CurrentAutoattack.Target.Stats.IsCriticalImmune ? false : Unit.Stats.CriticalStrike();
            Unit.AttackManager.CurrentAutoattack = new MeleeBasicAttack(Unit, CurrentAutoattack.Target, critical, false, DetermineNextSlot(critical));
            CurrentAutoattack.Notify();
        }
    }
}
