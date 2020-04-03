using Legends.Protocol.GameClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games
{
    public class BlueTeam : Team
    {
        public override TeamId Id => TeamId.BLUE;

        public BlueTeam(Game game) : base(game)
        {

        }
        public override Team[] GetOposedTeams()
        {
            return new Team[] 
            {
                Game.PurpleTeam,
                Game.NeutralTeam,
            };
        }

        public override void Update(float deltaTime)
        {
            UpdateVision(deltaTime, Game.PurpleTeam.AliveUnits);
            UpdateVision(deltaTime, Game.NeutralTeam.AliveUnits);
        }
    }
}
