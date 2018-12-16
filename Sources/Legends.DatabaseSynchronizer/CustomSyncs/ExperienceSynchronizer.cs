using Legends.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Legends.Records;
using Legends.ORM;

namespace Legends.DatabaseSynchronizer.CustomSyncs
{
    public class ExperienceSynchronizer
    {
        private static Logger logger = new Logger();

        public static void Synchronize(RafManager manager)
        {
            float[] cumulativeExps = new float[]
         {
                0f,
                280f,
                660f,
                1140f,
                1720f,
                2400f,
                3180f,
                4060f,
                5040f,
                6120f,
                7300f,
                8580f,
                9960f,
                11440f,
                13020f,
                14700f,
                16480f,
                18360f,
         };
            List<ExperienceRecord> records = new List<ExperienceRecord>();

            int level = 1;
            for (int i = 0; i < cumulativeExps.Length; i++)
            {
                records.Add(new ExperienceRecord(level, cumulativeExps[i]));
                level++;
            }
            DatabaseManager.Instance.CreateTable(typeof(ExperienceRecord));
            records.AddInstantElements(typeof(ExperienceRecord)); 

            logger.Write("Experiences synchronized");
        }
    }
}
