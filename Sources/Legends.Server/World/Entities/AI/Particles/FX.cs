using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;
using Legends.Core.DesignPattern;
using Legends.Protocol.GameClient.Types;

namespace Legends.World.Entities.AI.Particles
{
    public struct FX : IProtocolable<FXCreateData>
    {
        public uint NetId
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
        public string Bones
        {
            get;
            private set;
        }
        public float Size
        {
            get;
            private set;
        }
        public AIUnit Source
        {
            get;
            private set;
        }
        public AIUnit Target
        {
            get;
            private set;
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
                KeywordNetId = Target.NetId,
                NetAssignedNetId = NetId,
                OrientationVector = new Vector3(),
                OwnerPositionX = 0,
                OwnerPositionY = 0,
                OwnerPositionZ = 0,
                PositionX = 0,
                PositionY = 0,
                PositionZ = 0,
                ScriptScale = Size,// taille
                TargetNetId = Target.NetId,
                TargetPositionX = 0,
                TargetPositionY = 0,
                TargetPositionZ = 0,
                TimeSpent = 0.0f, // ou on en est?
            };
        }

        [InDevelopment(InDevelopmentState.THINK_ABOUT_IT, "Multiple FXCreate Data? change architecture")]
        public FXCreateGroupData GetFXCreateGroupData()
        {
            return new FXCreateGroupData()
            {

                BoneNameHash = Bones.HashString(),
                PackageHash = (uint)Source.GetHash(),
                EffectNameHash = Name.HashString(),
                Flags = 32,
                TargetBoneNameHash = "".HashString(),
                FXCreateData = new List<FXCreateData>()
                {
                       GetProtocolObject(),
                }

            };
        }
    }

}