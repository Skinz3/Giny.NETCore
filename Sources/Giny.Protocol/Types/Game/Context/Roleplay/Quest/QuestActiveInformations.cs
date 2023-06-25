using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class QuestActiveInformations  
    { 
        public const ushort Id = 3739;
        public virtual ushort TypeId => Id;

        public short questId;

        public QuestActiveInformations()
        {
        }
        public QuestActiveInformations(short questId)
        {
            this.questId = questId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element questId.");
            }

            writer.WriteVarShort((short)questId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            questId = (short)reader.ReadVarUhShort();
            if (questId < 0)
            {
                throw new System.Exception("Forbidden value (" + questId + ") on element of QuestActiveInformations.questId.");
            }

        }


    }
}








