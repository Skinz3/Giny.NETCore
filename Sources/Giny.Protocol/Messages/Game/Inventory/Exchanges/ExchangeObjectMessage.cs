using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectMessage : NetworkMessage  
    { 
        public  const ushort Id = 8683;
        public override ushort MessageId => Id;

        public bool remote;

        public ExchangeObjectMessage()
        {
        }
        public ExchangeObjectMessage(bool remote)
        {
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)remote);
        }
        public override void Deserialize(IDataReader reader)
        {
            remote = (bool)reader.ReadBoolean();
        }


    }
}








