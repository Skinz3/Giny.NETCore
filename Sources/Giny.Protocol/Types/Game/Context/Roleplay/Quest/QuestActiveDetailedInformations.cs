using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class QuestActiveDetailedInformations : QuestActiveInformations  
    { 
        public new const ushort Id = 3883;
        public override ushort TypeId => Id;

        public short stepId;
        public QuestObjectiveInformations[] objectives;

        public QuestActiveDetailedInformations()
        {
        }
        public QuestActiveDetailedInformations(short stepId,QuestObjectiveInformations[] objectives,short questId)
        {
            this.stepId = stepId;
            this.objectives = objectives;
            this.questId = questId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (stepId < 0)
            {
                throw new System.Exception("Forbidden value (" + stepId + ") on element stepId.");
            }

            writer.WriteVarShort((short)stepId);
            writer.WriteShort((short)objectives.Length);
            for (uint _i2 = 0;_i2 < objectives.Length;_i2++)
            {
                writer.WriteShort((short)(objectives[_i2] as QuestObjectiveInformations).TypeId);
                (objectives[_i2] as QuestObjectiveInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            QuestObjectiveInformations _item2 = null;
            base.Deserialize(reader);
            stepId = (short)reader.ReadVarUhShort();
            if (stepId < 0)
            {
                throw new System.Exception("Forbidden value (" + stepId + ") on element of QuestActiveDetailedInformations.stepId.");
            }

            uint _objectivesLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _objectivesLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<QuestObjectiveInformations>((short)_id2);
                _item2.Deserialize(reader);
                objectives[_i2] = _item2;
            }

        }


    }
}








