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
    [D2OClass("QuestStep")]
    [Table("quest_steps")]
    public class QuestStepRecord : IRecord
    {
        [Container]
        private static Dictionary<long, QuestStepRecord> Steps = new Dictionary<long, QuestStepRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("questId")]
        public long QuestId
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

        [I18NField]
        [D2OField("descriptionId")]
        public string Description
        {
            get;
            set;
        }
        [D2OField("dialogId")]
        public int DialogId
        {
            get;
            set;
        }

        [D2OField("optimalLevel")]
        public int OptimalLevel
        {
            get;
            set;
        }

        [D2OField("duration")]
        public int Duration
        {
            get;
            set;
        }

        [D2OField("objectiveIds")]
        public List<long> ObjectiveIds
        {
            get;
            set;
        }

        [Ignore]
        public List<QuestObjectiveRecord> Objectives
        {
            get;
            private set;
        }

        [D2OField("rewardsIds")]
        public List<long> RewardIds
        {
            get;
            set;
        }

        [Ignore]
        public List<QuestStepRewardRecord> Rewards
        {
            get;
            private set;
        }
        public static QuestStepRecord GetQuestStep(long id)
        {
            return Steps[id];
        }

        public void ReloadMembers()
        {
            Objectives = new List<QuestObjectiveRecord>();
            Rewards = new List<QuestStepRewardRecord>();

            foreach (var objectiveId in ObjectiveIds)
            {
                QuestObjectiveRecord record = QuestObjectiveRecord.GetQuestObjective(objectiveId);
                Objectives.Add(record);
            }

            foreach (var rewardId in RewardIds)
            {
                QuestStepRewardRecord record = QuestStepRewardRecord.GetQuestStepReward(rewardId);
                Rewards.Add(record);
            }

        }

        public override string ToString()
        {
            return $"({Id}) {Name}";
        }
    }
}
