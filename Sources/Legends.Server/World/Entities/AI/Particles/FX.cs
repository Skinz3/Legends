using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Types;

namespace Legends.World.Entities.AI.Particles
{
    public struct FX : IProtocolable<FXCreateData>
    {
        public uint NetId
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Bones
        {
            get;
            set;
        }
        public float Size
        {
            get;
            set;
        }
        public AIUnit Source
        {
            get;
            set;
        }
        public AIUnit Target
        {
            get;
            set;
        }
        public FX(uint netId, string name, string bones, float size, AIUnit source, AIUnit target)
        {
            this.NetId = netId;
            this.Name = name;
            this.Bones = bones;
            this.Size = size;
            this.Source = source;
            this.Target = target;
        }


        public FXCreateData GetProtocolObject()
        {
            return new FXCreateData()
            {
                BindNetId = Target.NetId,
                CasterNetId = Source.NetId,
                KeywordNetId = Source.NetId,
                NetAssignedNetId = NetId,
                OrientationVector = new Vector3(),
                OwnerPositionX = Source.Cell.X,
                OwnerPositionY = Source.Position.Y,
                OwnerPositionZ = 0,
                PositionX = Target.Cell.X,
                PositionY = Target.Position.Y,
                PositionZ = 0,
                ScriptScale = Size,// taille
                TargetNetId = NetId,
                TargetPositionX = (short)Target.Position.X,
                TargetPositionY = Target.Position.Y,
                TargetPositionZ = 0,
                TimeSpent = 0.0f, // ou on en est?
            };
        }
    }
}
