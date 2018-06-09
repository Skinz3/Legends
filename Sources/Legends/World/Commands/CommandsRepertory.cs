using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Core.Protocol.Enum;
using Legends.Core.Protocol.Messages.Game;
using Legends.Network;
using Legends.World.Entities.AI;
using Legends.World.Entities.AI.BasicAttack;
using Legends.World.Entities.Movements;
using Legends.World.Entities.Statistics;
using Legends.World.Entities.Statistics.Replication;
using Legends.World.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Legends.World.Commands
{
    class CommandsRepertory
    {
        [Command("addcrit")]
        public static void CercleCommand(LoLClient client)
        {
            client.Hero.AIStats.CriticalHit.FlatBonus += 0.10f;
            client.Hero.UpdateStats();

        }
        [Command("baserange")]
        public static void Cercle2Command(LoLClient client)
        {
            var distance = (float)client.Hero.Record.AttackRange;

            List<Vector2> results = new List<Vector2>();

            float start = 0;

            float end = (float)(2 * Math.PI);

            for (float i = start; i < end; i += 0.5f)
            {
                var v = Geo.GetPointOnCircle(client.Hero.Position, i, distance);
                client.Hero.AttentionPing(v, client.Hero.NetId, PingTypeEnum.Ping_OnMyWay);
            }

        }
        [Command("chasingrange")] // chasing range
        public static void Cercle3Command(LoLClient client)
        {
            var distance = (float)client.Hero.Record.AttackRange + (client.Hero.Record.AttackRange * (float)client.Hero.Record.ChasingAttackRangePercent);

            List<Vector2> results = new List<Vector2>();

            float start = 0;

            float end = (float)(2 * Math.PI);

            for (float i = start; i < end; i += 0.5f)
            {
                var v = Geo.GetPointOnCircle(client.Hero.Position, i, distance);
                client.Hero.AttentionPing(v, client.Hero.NetId, PingTypeEnum.Ping_OnMyWay);
            }

        }
        [Command("position")]
        public static void PositionCommand(LoLClient client)
        {
            client.Hero.DebugMessage(client.Hero.Position.ToString());
            client.Hero.AttentionPing(client.Hero.Position, client.Hero.NetId, PingTypeEnum.Ping_OnMyWay);
        }
        [Command("test")]
        public static void TestCommand(LoLClient client)
        {
            client.Hero.PlayerStats.AttackDamage.FlatBonus += 50f;
            client.Hero.PlayerStats.AttackSpeed.BaseBonus += 0.3f;
            client.Hero.Stats.Health.BaseBonus += 500;
            client.Hero.UpdateStats();
        }
        [Command("speed")]
        public static void SpeedCommand(LoLClient client, float speed)
        {
            client.Hero.PlayerStats.MoveSpeed.SetBaseValue(speed);
            client.Hero.UpdateStats(true);
        }

        [Command("size")]
        public static void SizeCommand(LoLClient client, float size)
        {
            client.Hero.PlayerStats.ModelSize.SetBaseValue(size);
            client.Hero.UpdateStats(true);
        }
        [InDeveloppement(InDeveloppementState.HAS_BUG, "When player leave vision, the model is swap back.")]
        [Command("model")]
        public static void ModelCommand(LoLClient client, string model)
        {
            client.Hero.UpdateModel(model, false, 0);
        }
        [Command("skin")]
        public static void SkinCommand(LoLClient client, int skinId)
        {
            client.Hero.UpdateModel(client.Hero.Model, false, skinId);
        }
        [Command("exp")]
        public static void AddExperienceCommand(LoLClient client, float exp)
        {
            client.Hero.AddExperience(exp);
        }
        [Command("life")]
        public static void LifeCommand(LoLClient client)
        {
            client.Hero.AIStats.Health.Current = client.Hero.AIStats.Health.Total;
            client.Hero.UpdateStats();
        }

        [Command("vision")]
        public static void VisionCommand(LoLClient client)
        {
            string str = "I have vision on : ";
            str += Environment.NewLine;
            foreach (var unit in client.Hero.Team.GetVisibleUnits())
            {
                str += unit.Name + " distance: (" + unit.GetDistanceTo(client.Hero) + ")  ";

            }
            client.Hero.DebugMessage(str);
        }
    }
}
