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
    public class CharacterQuestObjectiveRecord
    {
        [ProtoMember(1)]
        public long ObjectiveId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public bool Done
        {
            get;
            set;
        }

        private QuestObjectiveRecord m_record;

        public QuestObjectiveRecord Record
        {
            get
            {
                if (m_record == null)
                {
                    m_record = QuestObjectiveRecord.GetQuestObjective(ObjectiveId);
                }

                return m_record;
            }
        }
        public QuestObjectiveInformations GetQuestObjectiveInformations()
        {
            return new QuestObjectiveInformations((short)ObjectiveId, !Done, new string[0]);
        }

        public bool InvolveNpc(long npcId)
        {
            return Record.Parameters.Param0 == npcId && (
                     Record.Type == QuestObjectiveTypeEnum.NpcTalkBack ||
                     Record.Type == QuestObjectiveTypeEnum.GoToNpc);
        }
    }
}
