using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicPongMessage : NetworkMessage  
    { 
        public  const ushort Id = 5303;
        public override ushort MessageId => Id;

        public bool quiet;

        public BasicPongMessage()
        {
        }
        public BasicPongMessage(bool quiet)
        {
            this.quiet = quiet;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)quiet);
        }
        public override void Deserialize(IDataReader reader)
        {
            quiet = (bool)reader.ReadBoolean();
        }


    }
}








