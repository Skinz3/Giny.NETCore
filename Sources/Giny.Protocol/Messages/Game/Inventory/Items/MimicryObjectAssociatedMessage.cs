using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MimicryObjectAssociatedMessage : SymbioticObjectAssociatedMessage  
    { 
        public new const ushort Id = 2699;
        public override ushort MessageId => Id;


        public MimicryObjectAssociatedMessage()
        {
        }
        public MimicryObjectAssociatedMessage(int hostUID)
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








