using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MimicryObjectErrorMessage : SymbioticObjectErrorMessage  
    { 
        public new const ushort Id = 7868;
        public override ushort MessageId => Id;

        public bool preview;

        public MimicryObjectErrorMessage()
        {
        }
        public MimicryObjectErrorMessage(bool preview,byte reason,byte errorCode)
        {
            this.preview = preview;
            this.reason = reason;
            this.errorCode = errorCode;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)preview);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            preview = (bool)reader.ReadBoolean();
        }


    }
}








