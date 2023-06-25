using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.World.Managers.Experiences;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Formulas
{
    public class JobFormulas : Singleton<JobFormulas>
    {
        public const int MaxJobLevelGap = 100;

        public int GetCollectedItemQuantity(int jobLevel, SkillRecord skillRecord)
        {
            AsyncRandom rd = new AsyncRandom();
            var min = jobLevel == ExperienceManager.MaxLevel ? 7 : 1;
            var max = skillRecord.MinLevel == ExperienceManager.MaxLevel ? 8 : (int)(7d + ((jobLevel - skillRecord.MinLevel) / 10));
            return rd.Next(min, max);
        }

        public int GetCraftExperience(short resultLevel, short crafterLevel, int craftXpRatio)
        {
            double result = 0;
            double value = 0;

            if (resultLevel - MaxJobLevelGap > crafterLevel)
            {
                return 0;
            }

            value = 20.0d * resultLevel / (Math.Pow((double)crafterLevel - resultLevel, 1.1) / 10.0 + 1.0);

            if (craftXpRatio > -1)
            {
                result = (double)value * (craftXpRatio / 100d);
            }
            else if (craftXpRatio > -1)
            {
                result = value * (craftXpRatio / 100);
            }
            else
            {
                result = value;
            }
            return (int)(Math.Floor(result) * ConfigFile.Instance.JobRate);
        }
    }
}
