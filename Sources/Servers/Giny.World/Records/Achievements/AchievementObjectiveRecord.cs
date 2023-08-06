using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Achievements
{
    [D2OClass("AchievementObjective")]
    [Table("achievementobjectives")]
    public class AchievementObjectiveRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, AchievementObjectiveRecord> AchievementObjectives = new ConcurrentDictionary<long, AchievementObjectiveRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [D2OField("order")]
        public int ObjectiveOrder
        {
            get;
            set;
        }

        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("criterion")]
        public string Criterion
        {
            get;
            set;
        }

        public static AchievementObjectiveRecord? GetAchievementObjective(long id)
        {
            if (AchievementObjectives.ContainsKey(id))
            {
                return AchievementObjectives[id];
            }
            else
            {
                return null;
            }

        }

    }
}
