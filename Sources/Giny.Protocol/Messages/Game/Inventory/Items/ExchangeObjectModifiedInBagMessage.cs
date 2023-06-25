using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectModifiedInBagMessage : ExchangeObjectMessage  
    { 
        public new const ushort Id = 7251;
        public override ushort MessageId => Id;

        public ObjectItem @object;

        public ExchangeObjectModifiedInBagMessage()
        {
        }
        public ExchangeObjectModifiedInBagMessage(ObjectItem @object,bool remote)
        {
            this.@object = @object;
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            @object.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            @object = new ObjectItem();
            @object.Deserialize(reader);
        }


    }
}








