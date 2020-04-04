using Legends.Core;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
using Legends.Scripts.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.AI.Buffs
{
    public class BuffManager : IUpdatable
    {
        private AIUnit Owner
        {
            get;
            set;
        }
        public Dictionary<byte, BuffScript> Buffs
        {
            get;
            set;
        }
        public BuffManager(AIUnit owner)
        {
            this.Owner = owner;
            this.Buffs = new Dictionary<byte, BuffScript>();
        }
        public BuffScript AddBuff<T>(AIUnit source) where T : BuffScript
        {
            var buff = BuffScriptManager.Instance.GetBuffScript<T>(Owner, source);
            buff.OnAdded();
            buff.Slot = PopNextSlotId();
            Buffs.Add(buff.Slot, buff);
            Owner.Game.Send(new BuffAddMessage(Owner.NetId, buff.Slot, buff.BuffType, 1, false, buff.BuffName.HashString(), (uint)Owner.GetHash(), 0f, (buff.MaxDuration / 1000f), source.NetId));
            return buff;
        }
        private byte PopNextSlotId()
        {
            return (byte)(Buffs.Keys.Count > 0 ? Buffs.Keys.Last() + 1 : 0);
        }
        public void RemoveBuff(BuffScript buff)
        {
            buff.OnRemoved();
            Buffs.Remove(buff.Slot);
            Owner.Game.Send(new BuffRemoveMessage(Owner.NetId, buff.Slot, buff.BuffName.HashString(), 0f));
        }
        public void Update(float deltaTime)
        {
            foreach (var buff in Buffs.Values.ToArray())
            {
                buff.Update(deltaTime);

                if (buff.Duration <= 0)
                {
                    RemoveBuff(buff);
                }
            }
        }
    }
}
