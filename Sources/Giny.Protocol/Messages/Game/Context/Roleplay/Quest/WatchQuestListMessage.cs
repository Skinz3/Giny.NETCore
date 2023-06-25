using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class WatchQuestListMessage : QuestListMessage  
    { 
        public new const ushort Id = 3233;
        public override ushort MessageId => Id;

        public long playerId;

        public WatchQuestListMessage()
        {
        }
        public WatchQuestListMessage(long playerId,short[] finishedQuestsIds,short[] finishedQuestsCounts,QuestActiveInformations[] activeQuests,short[] reinitDoneQuestsIds)
        {
            this.playerId = playerId;
            this.finishedQuestsIds = finishedQuestsIds;
            this.finishedQuestsCounts = finishedQuestsCounts;
            this.activeQuests = activeQuests;
            this.reinitDoneQuestsIds = reinitDoneQuestsIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of WatchQuestListMessage.playerId.");
            }

        }


    }
}








