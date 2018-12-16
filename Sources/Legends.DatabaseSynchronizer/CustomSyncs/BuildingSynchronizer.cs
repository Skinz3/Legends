using Legends.Core.IO.CFG;
using Legends.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using Legends.ORM;
using System.Text;
using System.Threading.Tasks;
using Legends.Core;
using System.Numerics;
using Legends.Core.Utils;

namespace Legends.DatabaseSynchronizer.CustomSyncs
{
    public class BuildingSynchronizer
    {
        static Logger logger = new Logger();

        public static void Synchronize(RafManager manager)
        {
            List<BuildingRecord> records = new List<BuildingRecord>();

            foreach (var file in manager.GetFiles("ObjectCFG.cfg"))
            {
                CFGFile cfg = new CFGFile(file.GetContent(true));

                foreach (var obj in cfg.Objects)
                {
                    BuildingRecord record = new BuildingRecord();

                    record.MapId = Helper.GetMapId(file.Path);
                    record.Name = obj.Key;
                    record.Health = GetFloatFromCFG(obj.Value, "mMaxHP", 0);
                    record.CollisionRadius = GetFloatFromCFG(obj.Value, "Collision Radius", 0);
                    record.BaseStaticHpRegen = GetFloatFromCFG(obj.Value, "mBaseStaticHPRegen", 0);
                    record.CollisionHeight = GetFloatFromCFG(obj.Value, "Collision Height", 0);
                    record.Mana = GetFloatFromCFG(obj.Value, "mMaxMP", 0);
                    record.SelectionHeight = GetFloatFromCFG(obj.Value, "SelectionHeight", 0);
                    record.SelectionRadius = GetFloatFromCFG(obj.Value, "SelectionRadius", 0);
                    record.SkinId = GetIntFromCFG(obj.Value, "skinID", 0);
                    record.SkinName1 = GetStringFromCFG(obj.Value, "SkinName1", string.Empty);
                    record.SkinName2 = GetStringFromCFG(obj.Value, "SkinName2", string.Empty);
                    record.Rot = GetStringFromCFG(obj.Value, "Rot", string.Empty);
                    record.Move = GetStringFromCFG(obj.Value, "Move", string.Empty);
                    record.PerceptionBubbleRadius = GetFloatFromCFG(obj.Value, "PerceptionBubbleRadius", 0);
                    record.PathfindingCollisionRadius = GetFloatFromCFG(obj.Value, "PathfindingCollisionRadius", 0);
                    records.Add(record);

                }
            }

            DatabaseManager.Instance.CreateTable(typeof(BuildingRecord));
            records.AddInstantElements(typeof(BuildingRecord));

            logger.Write("Buildings Synchronized");

        }
        private static string GetStringFromCFG(Dictionary<string, string> properties, string key, string @default)
        {
            return properties.GetValueOrDefault(key, @default);
        }
        private static int GetIntFromCFG(Dictionary<string, string> properties, string key, float @default)
        {
            return int.Parse(properties.GetValueOrDefault(key, @default.ToString()));
        }
        private static float GetFloatFromCFG(Dictionary<string, string> properties, string key, float @default)
        {
            return float.Parse(properties.GetValueOrDefault(key, @default.ToString()).Replace('.', ','));
        }
    }
}
