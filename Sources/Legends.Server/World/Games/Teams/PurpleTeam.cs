using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Protocol.GameClient.Enum;

namespace Legends.World.Games
{
    public class PurpleTeam : Team
    {
        public override TeamId Id => TeamId.PURPLE;

        public PurpleTeam(Game game) : base(game)
        {

        }
        public override Team[] GetOposedTeams()
        {
            return new Team[]
            {
                Game.BlueTeam,
                Game.NeutralTeam,
            };
        }

        public override void Update(float deltaTime)
        {
            UpdateVision(deltaTime, Game.BlueTeam.AliveUnits);
            UpdateVision(deltaTime, Game.NeutralTeam.AliveUnits);
        }
    }
}
