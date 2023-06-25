using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicWhoAmIRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3187;
        public override ushort MessageId => Id;

        public bool verbose;

        public BasicWhoAmIRequestMessage()
        {
        }
        public BasicWhoAmIRequestMessage(bool verbose)
        {
            this.verbose = verbose;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)verbose);
        }
        public override void Deserialize(IDataReader reader)
        {
            verbose = (bool)reader.ReadBoolean();
        }


    }
}








