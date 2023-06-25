using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class WrapperObjectAssociatedMessage : SymbioticObjectAssociatedMessage  
    { 
        public new const ushort Id = 6952;
        public override ushort MessageId => Id;


        public WrapperObjectAssociatedMessage()
        {
        }
        public WrapperObjectAssociatedMessage(int hostUID)
        {
            this.hostUID = hostUID;
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








