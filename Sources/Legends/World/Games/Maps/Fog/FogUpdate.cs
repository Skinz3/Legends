using Legends.Core.Protocol.Enum;
using Legends.World.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games.Maps.Fog
{
    public class FogUpdate
    {
        public uint NetId
        {
            get;
            private set;
        }

        public TeamId TeamId
        {
            get;
            private set;
        }
        public Unit Source
        {
            get;
            private set;
        }
        public FogUpdate(uint netId, TeamId teamId, Unit source)
        {
            this.NetId = NetId;
            this.TeamId = teamId;
            this.Source = source;
        }
    }
}
