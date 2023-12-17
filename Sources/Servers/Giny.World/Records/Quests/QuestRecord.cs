using Giny.Core.DesignPattern;
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
    [D2OClass("Quest")]
    [Table("quests")]
    public class QuestRecord : IRecord
    {
        [Container]
        private static Dictionary<long, QuestRecord> Quests = new Dictionary<long, QuestRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }

        [D2OField("nameId")]
        [I18NField]
        public string Name
        {
            get;
            set;
        }

        [D2OField("stepIds")]
        public List<int> StepIds
        {
            get;
            set;
        }

        [Ignore]
        public List<QuestStepRecord> Steps
        {
            get;
            private set;
        }

        [D2OField("categoryId")]
        public short CategoryId
        {
            get;
            set;
        }

        [D2OField("repeatType")]
        public long RepeatType
        {
            get;
            set;
        }

        [D2OField("repeatLimit")]
        public byte RepeatLimit
        {
            get;
            set;
        }

        [D2OField("isDungeonQuest")]
        public bool IsDungeonQuest
        {
            get;
            set;
        }

        [D2OField("levelMin")]
        public int LevelMin
        {
            get;
            set;
        }

        [D2OField("levelMax")]
        public int LevelMax
        {
            get;
            set;
        }

        [D2OField("isPartyQuest")]
        public bool IsPartyQuest
        {
            get;
            set;
        }

        [D2OField("startCriterion")]
        public string StartCriterion
        {
            get;
            set;
        }

        [D2OField("followable")]
        public bool Followable
        {
            get;
            set;
        }

        [StartupInvoke("Quest members", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var quest in Quests.Values)
            {
                quest.ReloadMembers();
            }
        }

        private void ReloadMembers()
        {
            this.Steps = new List<QuestStepRecord>();

            foreach (var stepId in StepIds)
            {
                var step = QuestStepRecord.GetQuestStep(stepId);

                step.ReloadMembers();

                Steps.Add(step);
            }
        }

        public override string ToString()
        {
            return $"({Id}) {Name}";
        }

        public static QuestRecord GetQuest(long questId)
        {
            if (Quests.ContainsKey(questId))
            {
                return Quests[questId];
            }
            else
            {
                return null;
            }
        }
        public static List<QuestRecord> GetQuests()
        {
            return Quests.Values.ToList();
        }
    }
}
