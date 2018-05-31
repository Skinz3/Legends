using Legends.ORM.Attributes;
using Legends.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Records
{
    [Table("/Database/Experiences/")]
    public class ExperienceRecord : ITable
    {
        [JsonCache]
        private static List<ExperienceRecord> Experiences = new List<ExperienceRecord>();

        [JsonFileName]
        public int Level
        {
            get;
            set;
        }

        public float CumulativeExp
        {
            get;
            set;
        }
        public ExperienceRecord()
        {

        }
        public ExperienceRecord(int level, float cumulativeExp)
        {
            this.Level = level;
            this.CumulativeExp = cumulativeExp;
        }
        private static ExperienceRecord HighestExperience()
        {
            return Experiences.Last();
        }
        public static int GetLevel(float exp)
        {
            int result;

            ExperienceRecord highest = HighestExperience();
            if (exp >= highest.CumulativeExp)
            {
                result = highest.Level;
            }
            else
            {
                result = (ushort)(Experiences.FirstOrDefault(x => x.CumulativeExp > exp).Level - 1);
            }
            return result;
        }
    }
}
