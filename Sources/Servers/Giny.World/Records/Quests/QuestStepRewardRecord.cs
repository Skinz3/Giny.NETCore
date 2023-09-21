using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Quests
{
    [Table("quest_step_rewards")]
    public class QuestStepRewardRecord : IRecord
    {
        [Container]
        private static Dictionary<long, QuestStepRewardRecord> StepRewards = new Dictionary<long, QuestStepRewardRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [D2OField("stepId")]
        public long StepId
        {
            get;
            set;
        }

        [D2OField("levelMin")]
        public short LevelMin
        {
            get;
            set;
        }

        [D2OField("levelMax")]
        public short LevelMax
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
        public List<int> ItemRewards
        {
            get;
            set;
        }


        [D2OField("emotesReward")]
        public List<int> EmoteReward
        {
            get;
            set;
        }


        [D2OField("jobsReward")]
        public List<int> JobsReward
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
    }
}
