using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class QuestStepValidatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 3032;
        public override ushort MessageId => Id;

        public short questId;
        public short stepId;

        public QuestStepValidatedMessage()
        {
        }
        public QuestStepValidatedMessage(short questId,short stepId)
        {
            this.questId = questId;
            this.stepId = stepId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element questId.");
            }

            writer.WriteVarShort((short)questId);
            if (stepId < 0)
            {
                throw new System.Exception("Forbidden value (" + stepId + ") on element stepId.");
            }

            writer.WriteVarShort((short)stepId);
        }
        public override void Deserialize(IDataReader reader)
        {
            questId = (short)reader.ReadVarUhShort();
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element of QuestStepValidatedMessage.questId.");
            }

            stepId = (short)reader.ReadVarUhShort();
            if (stepId < 0)
            {
                throw new System.Exception("Forbidden value (" + stepId + ") on element of QuestStepValidatedMessage.stepId.");
            }

        }


    }
}








