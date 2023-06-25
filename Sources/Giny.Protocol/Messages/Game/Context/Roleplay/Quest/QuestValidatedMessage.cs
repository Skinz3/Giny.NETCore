using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class QuestValidatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6536;
        public override ushort MessageId => Id;

        public short questId;

        public QuestValidatedMessage()
        {
        }
        public QuestValidatedMessage(short questId)
        {
            this.questId = questId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element questId.");
            }

            writer.WriteVarShort((short)questId);
        }
        public override void Deserialize(IDataReader reader)
        {
            questId = (short)reader.ReadVarUhShort();
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element of QuestValidatedMessage.questId.");
            }

        }


    }
}








