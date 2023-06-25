using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolPartyRegisterRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 110;
        public override ushort MessageId => Id;

        public bool register;

        public IdolPartyRegisterRequestMessage()
        {
        }
        public IdolPartyRegisterRequestMessage(bool register)
        {
            this.register = register;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)register);
        }
        public override void Deserialize(IDataReader reader)
        {
            register = (bool)reader.ReadBoolean();
        }


    }
}








