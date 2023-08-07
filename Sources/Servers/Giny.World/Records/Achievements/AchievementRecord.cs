using Giny.Core;
using Giny.Core.DesignPattern;
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
    [Table("achievements")]
    [D2OClass("Achievement")]
    public class AchievementRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, AchievementRecord> Achievements = new ConcurrentDictionary<long, AchievementRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
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

        [D2OField("categoryId")]
        public int CategoryId
        {
            get;
            set;
        }

        [I18NField]
        [D2OField("descriptionId")]
        public string DescriptionId
        {
            get;
            set;
        }

        [D2OField("points")]
        public int Points
        {
            get;
            set;
        }

        [D2OField("level")]
        public short Level
        {
            get;
            set;
        }

        [D2OField("accountLinked")]
        public bool AccountLinked
        {
            get;
            set;
        }

        [D2OField("objectiveIds")]
        public List<int> ObjectiveIds
        {
            get;
            set;
        }

        [D2OField("rewardIds")]
        public List<int> RewardIds
        {
            get;
            set;
        }

        [Ignore]
        public List<AchievementRewardRecord> Rewards
        {
            get;
            set;
        }

        [Ignore]
        public List<AchievementObjectiveRecord> Objectives
        {
            get;
            set;
        }


        public void ReloadMembers()
        {
            Rewards = new List<AchievementRewardRecord>();
            Objectives = new List<AchievementObjectiveRecord>();

            foreach (var objectiveId in ObjectiveIds)
            {
                AchievementObjectiveRecord objective = AchievementObjectiveRecord.GetAchievementObjective(objectiveId);

                if (objective == null)
                {
                    Logger.Write($"Achievement objective not found ({objectiveId})", Channels.Warning);
                    continue;
                }

                Objectives.Add(objective);
            }


            foreach (var rewardId in RewardIds)
            {
                AchievementRewardRecord reward = AchievementRewardRecord.GetAchievementReward(rewardId);

                if (reward == null)
                {
                    Logger.Write($"Achievement reward not found ({rewardId})", Channels.Warning);
                    continue;
                }

                Rewards.Add(reward);
            }

        }

        [StartupInvoke("Achievements members", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var achievement in Achievements.Values)
            {
                achievement.ReloadMembers();
            }
        }

        public static AchievementRecord GetAchievement(int id)
        {
            if (Achievements.ContainsKey(id))
            {
                return Achievements[id];
            }
            else
            {
                return null;
            }
        }
    }
}
