using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class QuestObjectiveValidatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 5846;
        public override ushort MessageId => Id;

        public short questId;
        public short objectiveId;

        public QuestObjectiveValidatedMessage()
        {
        }
        public QuestObjectiveValidatedMessage(short questId,short objectiveId)
        {
            this.questId = questId;
            this.objectiveId = objectiveId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element questId.");
            }

            writer.WriteVarShort((short)questId);
            if (objectiveId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectiveId + ") on element objectiveId.");
            }

            writer.WriteVarShort((short)objectiveId);
        }
        public override void Deserialize(IDataReader reader)
        {
            questId = (short)reader.ReadVarUhShort();
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element of QuestObjectiveValidatedMessage.questId.");
            }

            objectiveId = (short)reader.ReadVarUhShort();
            if (objectiveId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectiveId + ") on element of QuestObjectiveValidatedMessage.objectiveId.");
            }

        }


    }
}








