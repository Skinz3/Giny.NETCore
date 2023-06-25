using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AcquaintanceSearchMessage : NetworkMessage  
    { 
        public  const ushort Id = 2520;
        public override ushort MessageId => Id;

        public AccountTagInformation tag;

        public AcquaintanceSearchMessage()
        {
        }
        public AcquaintanceSearchMessage(AccountTagInformation tag)
        {
            this.tag = tag;
        }
        public override void Serialize(IDataWriter writer)
        {
            tag.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            tag = new AccountTagInformation();
            tag.Deserialize(reader);
        }


    }
}








