using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Quests
{
    [ProtoContract]
    public class CharacterQuestRecord
    {
        [ProtoMember(1)]
        public long QuestId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public long StepId
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public List<CharacterQuestObjectiveRecord> Objectives
        {
            get;
            set;
        }

        private QuestStepRecord m_stepRecord;

        public QuestStepRecord StepRecord
        {
            get
            {
                if (m_stepRecord == null || m_stepRecord.Id != StepId)
                {
                    m_stepRecord = QuestStepRecord.GetQuestStep(StepId);
                }

                return m_stepRecord;
            }
        }

        private QuestRecord? m_record;
        public QuestRecord Record
        {
            get
            {
                if (m_record == null)
                {
                    m_record = QuestRecord.GetQuest(QuestId);
                }
                return m_record;
            }
        }



        public List<CharacterQuestObjectiveRecord> GetObjectives(params QuestObjectiveTypeEnum[] types)
        {
            return Objectives.Where(x => types.Contains(x.Record.Type)).ToList();
        }
        public QuestActiveInformations GetQuestActiveInformations()
        {
            return new QuestActiveDetailedInformations()
            {
                questId = (short)QuestId,
                stepId = (short)StepId,
                objectives = Objectives.Where(x => Available(x)).Select(x => x.GetQuestObjectiveInformations()).ToArray()
            };
        }

        public bool Available(CharacterQuestObjectiveRecord record)
        {
            if (record.Done)
            {
                return true;
            }

            var talkBackObjective = Objectives.FirstOrDefault(x => !x.Done && x.Record.Type == QuestObjectiveTypeEnum.NpcTalkBack);


            if (talkBackObjective != null)
            {
                var indexRef = Objectives.IndexOf(talkBackObjective);


                return Objectives.IndexOf(record) < indexRef || (Objectives.Take(indexRef).All(x => x.Done) && record == talkBackObjective);
            }

            return true;

        }
        public bool Finished()
        {
            return Objectives.All(x => x.Done) && StepId == Record.StepIds.Last();
        }
        public bool NextStep()
        {
            var index = Record.StepIds.IndexOf((int)StepId);
            index++;

            if (index > Record.Steps.Count - 1)
            {
                return false;
            }

            StepId = Record.StepIds[index];

            Objectives = StepRecord.Objectives.Select(x => x.GetCharacterQuestObjectiveRecord()).ToList();
            return true;
        }

    }
}
