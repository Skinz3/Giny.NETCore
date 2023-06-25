using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseInListUpdatedMessage : ExchangeBidHouseInListAddedMessage  
    { 
        public new const ushort Id = 5004;
        public override ushort MessageId => Id;


        public ExchangeBidHouseInListUpdatedMessage()
        {
        }
        public ExchangeBidHouseInListUpdatedMessage(int itemUID,short objectGID,int objectType,ObjectEffect[] effects,long[] prices)
        {
            this.itemUID = itemUID;
            this.objectGID = objectGID;
            this.objectType = objectType;
            this.effects = effects;
            this.prices = prices;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








