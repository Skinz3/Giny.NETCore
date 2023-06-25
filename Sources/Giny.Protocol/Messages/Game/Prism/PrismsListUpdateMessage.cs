using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismsListUpdateMessage : PrismsListMessage  
    { 
        public new const ushort Id = 7086;
        public override ushort MessageId => Id;


        public PrismsListUpdateMessage()
        {
        }
        public PrismsListUpdateMessage(PrismSubareaEmptyInfo[] prisms)
        {
            this.prisms = prisms;
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








