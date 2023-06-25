using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class RefreshFollowedQuestsOrderRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6506;
        public override ushort MessageId => Id;

        public short[] quests;

        public RefreshFollowedQuestsOrderRequestMessage()
        {
        }
        public RefreshFollowedQuestsOrderRequestMessage(short[] quests)
        {
            this.quests = quests;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)quests.Length);
            for (uint _i1 = 0;_i1 < quests.Length;_i1++)
            {
                if (quests[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + quests[_i1] + ") on element 1 (starting at 1) of quests.");
                }

                writer.WriteVarShort((short)quests[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _questsLen = (uint)reader.ReadUShort();
            quests = new short[_questsLen];
            for (uint _i1 = 0;_i1 < _questsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of quests.");
                }

                quests[_i1] = (short)_val1;
            }

        }


    }
}








