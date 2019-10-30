using Legends.Core;
using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Records;
using Legends.Scripts.Spells;
using Legends.World.Entities;
using Legends.World.Entities.AI;
using Legends.World.Games.Delayed;
using Legends.World.Spells;
using Legends.World.Spells.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.bin.Debug.Scripts.Spells.Global
{
    public class Recall : SpellScript
    {
        public const string SPELL_NAME = "Recall";

        private DelayedAction RecallAction
        {
            get;
            set;
        }
        public bool IsRecalling
        {
            get
            {
                return RecallAction != null;
            }
        }
        public Recall(AIUnit unit, SpellRecord record) : base(unit, record)
        {

        }

        private void BindEvents()
        {
            Owner.EventsBinder.EvtStartMoving += OnMove;
            Owner.EventsBinder.EvtDamagesInflicted += OnDamageInflicted;
            Owner.EventsBinder.EvtSpellStartCasting += OnSpellCast;
        }
        private void UnbindEvents()
        {
            Owner.EventsBinder.EvtStartMoving -= OnMove;
            Owner.EventsBinder.EvtDamagesInflicted -= OnDamageInflicted;
            Owner.EventsBinder.EvtSpellStartCasting -= OnSpellCast;
        }


        [InDevelopment(InDevelopmentState.TODO, "Add recall buff a nice way (not only send packet x) )")]
        public override void OnFinishCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
            Owner.Game.Send(new BuffAddMessage(Owner.NetId, 0, BuffTypeEnum.Counter
                 , 1, false, "Recall".HashString(), (uint)Owner.GetHash(), 0f, 8f, Owner.NetId)); // properly!

            CreateFX("TeleportHome.troy", "", 1f, Owner, true);
            BindEvents();
            RecallAction = CreateAction(() =>
            {
                DestroyRecall();
                Owner.Teleport(Owner.SpawnPosition, true);
                CreateFX("teleportarrive.troy", "", 1f, Owner, false);

            }, 8f);
        }



        private void DestroyRecall()
        {
            DestroyFX("TeleportHome.troy");
            UnbindEvents();
            RecallAction = null;
        }

        public override bool CanCast()
        {
            return IsRecalling == false && Owner.IsMoving == false;
        }
        public override void OnStartCasting(Vector2 position, Vector2 endPosition, AttackableUnit target)
        {
           
        }



        private void OnMove(Vector2[] obj)
        {
            RecallAction.Cancel();
            DestroyRecall();
        }
        private void OnDamageInflicted(World.Spells.Damages obj)
        {
            RecallAction.Cancel();
            DestroyRecall();
        }
        private void OnSpellCast(Spell arg1, Vector2 arg2, Vector2 arg3)
        {
            RecallAction.Cancel();
            DestroyRecall();
        }
    }
}
