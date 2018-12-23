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
using Legends.Protocol.GameClient.Types;

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
            foreach (var inhib in client.Hero.Team.GetOposedTeams()[0].GetUnits<Inhibitor>(x => !x.Alive))
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
        [Command("addshield")]
        public static void AddShieldCommand(LoLClient client, float value)
        {
            client.Hero.Shields.MagicalAndPhysical += value;
            client.Hero.OnShieldModified(true, true, value);
        }
        [Command("addmshield")]
        public static void AddMagicalShieldCommand(LoLClient client, float value)
        {
            client.Hero.Shields.Magical += value;
            client.Hero.OnShieldModified(true, false, value);
        }
        [Command("circle")]
        public static void Cercle2Command(LoLClient client, float size)
        {
            List<Vector2> results = new List<Vector2>();

            float start = 0;

            float end = (float)(2 * Math.PI);

            for (float i = start; i < end; i += 0.5f)
            {
                var v = Geo.GetPointOnCircle(client.Hero.Position, i, size);
                client.Hero.AttentionPing(v, client.Hero.NetId, PingTypeEnum.Ping_OnMyWay);
            }

        }
        [Command("position")]
        public static void PositionCommand(LoLClient client)
        {
            client.Hero.BlueTip("Position",client.Hero.Position.ToString(),string.Empty,TipCommandEnum.ACTIVATE_TIP_DIALOGUE);
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
        [Command("model")]
        public static void ModelCommand(LoLClient client, string model)
        {
            client.Hero.AddStackData(model, 0, true, false, true);
        }
        [Command("skin")]
        public static void SkinCommand(LoLClient client, uint skinId)
        {
            client.Hero.AddStackData(client.Hero.Model, skinId, false, true, true);
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
            client.Send(new DisplayFloatingTextMessage(client.Hero.NetId, FloatTextEnum.Gold, 0, "400"));
            return;
            uint netId = client.Hero.Game.NetIdProvider.PopNextNetId();

            var visibilityData = new VisibilityDataAIMinion()
            {
                MovementSyncID = Environment.TickCount,
                MovementData = new MovementDataStop()
                {
                    Position = client.Hero.Position,
                    Forward = client.Hero.Position,
                },
                BuffCount = new List<KeyValuePair<byte, int>>(),
                Items = new ItemData[0],
                LookAtNetId = 0,
                LookAtPosition = new Vector3(),
                LookAtType = LookAtType.Direction,
                CharacterDataStack = new CharacterStackData[0],
                ShieldValues = null,
                UnknownIsHero = false,

            };
            client.Hero.Game.Send(new CreateNeutralMessage(netId, NetNodeEnum.Map, client.Hero.GetPositionVector3(), client.Hero.GetPositionVector3(),
                new Vector3(), "camp", "SRU_Baron", "wtf", "SRU_Baron_spawn", TeamId.NEUTRAL, 0, 0, MinionRoamState.Inactive, 1, 1, 2, 1, 10, 30, 0, ""));

            client.Hero.Game.Send(new OnEnterVisiblityClientMessage(netId, visibilityData));


            client.Hero.Game.Send(new OnEnterLocalVisiblityClient(netId, 1000, 1000));

            client.Hero.Game.Send(new PlayAnimationMessage(netId, 100, 0, 1, "SRU_Baron_spawn"));


            return;
            //    client.Hero.Game.Send(new UpdateModelMessage(t.NetId, "SRUAP_Turret_Chaos1", true, 0));



            client.Hero.BlueTip("Legends", "This is for developpement purpose only!", "", TipCommandEnum.ACTIVATE_TIP_DIALOGUE);
            return;
            var turret = client.Hero.Game.GetUnit<AITurret>("Turret_T2_C_01");

            List<short> items = new List<short>()
            {
              1501, 1502, 1503, 1505
            };

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
            client.Hero.AddGold(gold, true);
            client.Hero.UpdateStats();
        }
    }
}
