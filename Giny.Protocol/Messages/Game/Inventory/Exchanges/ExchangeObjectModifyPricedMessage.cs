using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectModifyPricedMessage : ExchangeObjectMovePricedMessage  
    { 
        public new const ushort Id = 6429;
        public override ushort MessageId => Id;


        public ExchangeObjectModifyPricedMessage()
        {
        }
        public ExchangeObjectModifyPricedMessage(int objectUID,int quantity,long price)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
            this.price = price;
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








