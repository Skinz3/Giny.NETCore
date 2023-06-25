using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseSellFromInsideRequestMessage : HouseSellRequestMessage  
    { 
        public new const ushort Id = 2789;
        public override ushort MessageId => Id;


        public HouseSellFromInsideRequestMessage()
        {
        }
        public HouseSellFromInsideRequestMessage(int instanceId,long amount,bool forSale)
        {
            this.instanceId = instanceId;
            this.amount = amount;
            this.forSale = forSale;
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








