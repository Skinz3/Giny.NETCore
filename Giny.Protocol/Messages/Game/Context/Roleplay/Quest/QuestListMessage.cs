using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class QuestListMessage : NetworkMessage  
    { 
        public  const ushort Id = 4672;
        public override ushort MessageId => Id;

        public short[] finishedQuestsIds;
        public short[] finishedQuestsCounts;
        public QuestActiveInformations[] activeQuests;
        public short[] reinitDoneQuestsIds;

        public QuestListMessage()
        {
        }
        public QuestListMessage(short[] finishedQuestsIds,short[] finishedQuestsCounts,QuestActiveInformations[] activeQuests,short[] reinitDoneQuestsIds)
        {
            this.finishedQuestsIds = finishedQuestsIds;
            this.finishedQuestsCounts = finishedQuestsCounts;
            this.activeQuests = activeQuests;
            this.reinitDoneQuestsIds = reinitDoneQuestsIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)finishedQuestsIds.Length);
            for (uint _i1 = 0;_i1 < finishedQuestsIds.Length;_i1++)
            {
                if (finishedQuestsIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + finishedQuestsIds[_i1] + ") on element 1 (starting at 1) of finishedQuestsIds.");
                }

                writer.WriteVarShort((short)finishedQuestsIds[_i1]);
            }

            writer.WriteShort((short)finishedQuestsCounts.Length);
            for (uint _i2 = 0;_i2 < finishedQuestsCounts.Length;_i2++)
            {
                if (finishedQuestsCounts[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + finishedQuestsCounts[_i2] + ") on element 2 (starting at 1) of finishedQuestsCounts.");
                }

                writer.WriteVarShort((short)finishedQuestsCounts[_i2]);
            }

            writer.WriteShort((short)activeQuests.Length);
            for (uint _i3 = 0;_i3 < activeQuests.Length;_i3++)
            {
                writer.WriteShort((short)(activeQuests[_i3] as QuestActiveInformations).TypeId);
                (activeQuests[_i3] as QuestActiveInformations).Serialize(writer);
            }

            writer.WriteShort((short)reinitDoneQuestsIds.Length);
            for (uint _i4 = 0;_i4 < reinitDoneQuestsIds.Length;_i4++)
            {
                if (reinitDoneQuestsIds[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + reinitDoneQuestsIds[_i4] + ") on element 4 (starting at 1) of reinitDoneQuestsIds.");
                }

                writer.WriteVarShort((short)reinitDoneQuestsIds[_i4]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _id3 = 0;
            QuestActiveInformations _item3 = null;
            uint _val4 = 0;
            uint _finishedQuestsIdsLen = (uint)reader.ReadUShort();
            finishedQuestsIds = new short[_finishedQuestsIdsLen];
            for (uint _i1 = 0;_i1 < _finishedQuestsIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of finishedQuestsIds.");
                }

                finishedQuestsIds[_i1] = (short)_val1;
            }

            uint _finishedQuestsCountsLen = (uint)reader.ReadUShort();
            finishedQuestsCounts = new short[_finishedQuestsCountsLen];
            for (uint _i2 = 0;_i2 < _finishedQuestsCountsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of finishedQuestsCounts.");
                }

                finishedQuestsCounts[_i2] = (short)_val2;
            }

            uint _activeQuestsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _activeQuestsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<QuestActiveInformations>((short)_id3);
                _item3.Deserialize(reader);
                activeQuests[_i3] = _item3;
            }

            uint _reinitDoneQuestsIdsLen = (uint)reader.ReadUShort();
            reinitDoneQuestsIds = new short[_reinitDoneQuestsIdsLen];
            for (uint _i4 = 0;_i4 < _reinitDoneQuestsIdsLen;_i4++)
            {
                _val4 = (uint)reader.ReadVarUhShort();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of reinitDoneQuestsIds.");
                }

                reinitDoneQuestsIds[_i4] = (short)_val4;
            }

        }


    }
}








