using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Records.Items;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Achievements
{
    [Table("achievementrewards")]
    [D2OClass("AchievementReward")]
    public class AchievementRewardRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, AchievementRewardRecord> AchievementRewards = new ConcurrentDictionary<long, AchievementRewardRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [D2OField("criteria")]
        public string Criteria
        {
            get;
            set;
        }
        [D2OField("kamasRatio")]
        public double KamasRatio
        {
            get;
            set;
        }
        [D2OField("experienceRatio")]
        public double ExperienceRatio
        {
            get;
            set;
        }
        [D2OField("kamasScaleWithPlayerLevel")]
        public bool KamasScaleWithPlayerLevel
        {
            get;
            set;
        }

        [D2OField("itemsReward")]
        public List<int> ItemsReward
        {
            get;
            set;
        }
        [D2OField("itemsQuantityReward")]
        public List<int> ItemsQuantityReward
        {
            get;
            set;
        }
        [D2OField("emotesReward")]
        public List<int> EmotesReward
        {
            get;
            set;
        }
        [D2OField("spellsReward")]
        public List<int> SpellsReward
        {
            get;
            set;
        }
        [D2OField("titlesReward")]
        public List<int> TitlesReward
        {
            get;
            set;
        }
        [D2OField("ornamentsReward")]
        public List<int> OrnamentsReward
        {
            get;
            set;
        }

        public static AchievementRewardRecord GetAchievementReward(long id)
        {
            if (AchievementRewards.ContainsKey(id))
            {
                return AchievementRewards[id];
            }
            else
            {
                return null;
            }
        }

    }
}
