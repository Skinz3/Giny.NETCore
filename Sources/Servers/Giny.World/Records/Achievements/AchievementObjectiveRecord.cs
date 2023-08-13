using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Criterias;
using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Network;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Achievements
{
    [D2OClass("AchievementObjective")]
    [Table("achievement_objectives")]
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

        [D2OField("achievementId")]
        public int AchievementId
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

        [Ignore]
        public CriteriaExpression CriteriaExpression
        {
            get;
            set;
        }


        [StartupInvoke(StartupInvokePriority.FifthPass)]
        public static void Initialize()
        {
            foreach (var objective in AchievementObjectives.Values)
            {
                objective.CriteriaExpression = new CriteriaExpression(objective.Criterion);
            }
        }


        public static List<AchievementObjectiveRecord> GetAchievementObjectives()
        {
            return AchievementObjectives.Values.ToList();
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

        public short GetValue(WorldClient client)
        {
            var criterion = GetMainCriterion();

            if (criterion != null)
            {
                return criterion.GetCurrentValue(client);
            }
            else
            {
                return 0;
            }
        }
        public short GetMaxValue()
        {
            var criterion = GetMainCriterion();

            if (criterion != null)
            {
                return criterion.MaxValue;
            }
            else
            {
                return 1;
            }
        }

        private Criterion GetMainCriterion()
        {
            return CriteriaExpression.FindCriterionHandlers().Where(x => !(x is UnknownCriterion)).FirstOrDefault();
        }
    }
}
