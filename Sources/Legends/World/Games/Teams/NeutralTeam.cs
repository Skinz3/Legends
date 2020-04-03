using ENet;
using Legends.Core.Protocol;
using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games
{
    public class NeutralTeam : Team
    {
        public override TeamId Id => TeamId.NEUTRAL;

        public NeutralTeam(Game game) : base(game)
        {

        }
        public override Team[] GetOposedTeams()
        {
            return new Team[]
            {
                Game.PurpleTeam,
                Game.BlueTeam,
            };
        }
        public override void Update(float deltaTime)
        {
            UpdateVision(deltaTime, Game.BlueTeam.AliveUnits);
            UpdateVision(deltaTime, Game.PurpleTeam.AliveUnits);
        }
    }
}
