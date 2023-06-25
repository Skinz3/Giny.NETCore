using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FollowedQuestsMessage : NetworkMessage  
    { 
        public  const ushort Id = 1779;
        public override ushort MessageId => Id;

        public QuestActiveDetailedInformations[] quests;

        public FollowedQuestsMessage()
        {
        }
        public FollowedQuestsMessage(QuestActiveDetailedInformations[] quests)
        {
            this.quests = quests;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)quests.Length);
            for (uint _i1 = 0;_i1 < quests.Length;_i1++)
            {
                (quests[_i1] as QuestActiveDetailedInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            QuestActiveDetailedInformations _item1 = null;
            uint _questsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _questsLen;_i1++)
            {
                _item1 = new QuestActiveDetailedInformations();
                _item1.Deserialize(reader);
                quests[_i1] = _item1;
            }

        }


    }
}








