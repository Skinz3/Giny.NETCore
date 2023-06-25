using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseItemRemoveOkMessage : NetworkMessage  
    { 
        public  const ushort Id = 3056;
        public override ushort MessageId => Id;

        public int sellerId;

        public ExchangeBidHouseItemRemoveOkMessage()
        {
        }
        public ExchangeBidHouseItemRemoveOkMessage(int sellerId)
        {
            this.sellerId = sellerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)sellerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            sellerId = (int)reader.ReadInt();
        }


    }
}








