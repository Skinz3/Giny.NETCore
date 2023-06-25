using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class WrapperObjectErrorMessage : SymbioticObjectErrorMessage  
    { 
        public new const ushort Id = 1427;
        public override ushort MessageId => Id;


        public WrapperObjectErrorMessage()
        {
        }
        public WrapperObjectErrorMessage(byte reason,byte errorCode)
        {
            this.reason = reason;
            this.errorCode = errorCode;
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








