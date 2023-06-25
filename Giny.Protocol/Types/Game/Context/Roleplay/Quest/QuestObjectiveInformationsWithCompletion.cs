using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations  
    { 
        public new const ushort Id = 5684;
        public override ushort TypeId => Id;

        public short curCompletion;
        public short maxCompletion;

        public QuestObjectiveInformationsWithCompletion()
        {
        }
        public QuestObjectiveInformationsWithCompletion(short curCompletion,short maxCompletion,short objectiveId,bool objectiveStatus,string[] dialogParams)
        {
            this.curCompletion = curCompletion;
            this.maxCompletion = maxCompletion;
            this.objectiveId = objectiveId;
            this.objectiveStatus = objectiveStatus;
            this.dialogParams = dialogParams;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (curCompletion < 0)
            {
                throw new System.Exception("Forbidden value (" + curCompletion + ") on element curCompletion.");
            }

            writer.WriteVarShort((short)curCompletion);
            if (maxCompletion < 0)
            {
                throw new System.Exception("Forbidden value (" + maxCompletion + ") on element maxCompletion.");
            }

            writer.WriteVarShort((short)maxCompletion);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            curCompletion = (short)reader.ReadVarUhShort();
            if (curCompletion < 0)
            {
                throw new System.Exception("Forbidden value (" + curCompletion + ") on element of QuestObjectiveInformationsWithCompletion.curCompletion.");
            }

            maxCompletion = (short)reader.ReadVarUhShort();
            if (maxCompletion < 0)
            {
                throw new System.Exception("Forbidden value (" + maxCompletion + ") on element of QuestObjectiveInformationsWithCompletion.maxCompletion.");
            }

        }


    }
}








