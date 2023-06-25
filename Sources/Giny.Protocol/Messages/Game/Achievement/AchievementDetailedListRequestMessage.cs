using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementDetailedListRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6918;
        public override ushort MessageId => Id;

        public short categoryId;

        public AchievementDetailedListRequestMessage()
        {
        }
        public AchievementDetailedListRequestMessage(short categoryId)
        {
            this.categoryId = categoryId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (categoryId < 0)
            {
                throw new System.Exception("Forbidden value (" + categoryId + ") on element categoryId.");
            }

            writer.WriteVarShort((short)categoryId);
        }
        public override void Deserialize(IDataReader reader)
        {
            categoryId = (short)reader.ReadVarUhShort();
            if (categoryId < 0)
            {
                throw new System.Exception("Forbidden value (" + categoryId + ") on element of AchievementDetailedListRequestMessage.categoryId.");
            }

        }


    }
}








