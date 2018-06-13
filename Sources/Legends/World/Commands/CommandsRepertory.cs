using Legends.Core.DesignPattern;
using Legends.Core.Geometry;
using Legends.Protocol.GameClient.Enum;
using Legends.Protocol.GameClient.Messages.Game;
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
using Legends.World.Entities.Buildings;

namespace Legends.World.Commands
{
    class CommandsRepertory
    {
        [Command("item")]
        public static void AddItemCommand(LoLClient client, int itemId)
        {
            client.Hero.Inventory.AddItem(itemId);
        }
        [Command("inhibitors")]
        public static void RespawnInhibitorsCommand(LoLClient client)
        {
            foreach (var inhib in client.Hero.GetOposedTeam().GetUnits<Inhibitor>(x => !x.Alive))
            {
                inhib.Revive();
            }
        }
        [Command("inventory")]
        public static void InventoryCommand(LoLClient client)
        {
            foreach (var item in client.Hero.Inventory.GetItems())
            {
                client.Hero.DebugMessage("Id:" + item.Id + " Slot:" + item.Slot);
            }
        }
        [Command("addcrit")]
        public static void AddCritCommand(LoLClient client, float value)
        {
            client.Hero.Stats.CriticalHit.FlatBonus += value;
            client.Hero.UpdateStats();
        }
        [Command("addlifesteal")]
        public static void AddLifeStealCommand(LoLClient client, float value)
        {
            client.Hero.Stats.LifeSteal.FlatBonus += value;
            client.Hero.UpdateStats();
        }
        [Command("range")]
        public static void Cercle2Command(LoLClient client)
        {
            var distance = (float)client.Hero.Record.AttackRange + (float)client.Hero.Record.PathfindingCollisionRadius;

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
        [Command("addlife")]
        public static void AddLifeCommand(LoLClient client, float value)
        {
            client.Hero.Stats.Health.BaseBonus += value;
            client.Hero.UpdateStats();
        }
        [Command("adddamage")]
        public static void AddDamageCommand(LoLClient client, float value)
        {
            client.Hero.Stats.AttackDamage.FlatBonus += value;
            client.Hero.UpdateStats();
        }
        [Command("addas")]
        public static void AddAttackSpeed(LoLClient client, float value)
        {
            client.Hero.Stats.AttackSpeed.BaseBonus += value;
            client.Hero.UpdateStats();

        }
        [Command("speed")]
        public static void SpeedCommand(LoLClient client, float speed)
        {
            client.Hero.Stats.MoveSpeed.SetBaseValue(speed);
            client.Hero.UpdateStats(true);
        }

        [Command("size")]
        public static void SizeCommand(LoLClient client, float size)
        {
            client.Hero.Stats.ModelSize.SetBaseValue(size);
            client.Hero.UpdateStats(true);
        }
        [InDevelopment(InDevelopmentState.HAS_BUG, "When player leave vision, the model is swap back.")]
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
            client.Hero.Stats.Health.Current = client.Hero.Stats.Health.TotalSafe;
            client.Hero.UpdateStats();
        }
        [Command("test")]
        public static void TestCommand(LoLClient client)
        {
            client.Send(new DashMessage(client.Hero.NetId, 30f, 10f, client.Hero.Position, true, client.Hero.NetId, client.Hero.Game.Map.Size,
                new Vector2(500, 500), 10f, 10f, 10f));
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
        [Command("addgold")]
        public static void AddGoldCommand(LoLClient client, int gold)
        {
            client.Hero.Stats.AddGold(gold);
            client.Hero.UpdateStats();
        }
    }
}
