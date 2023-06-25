using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicPingMessage : NetworkMessage  
    { 
        public  const ushort Id = 398;
        public override ushort MessageId => Id;

        public bool quiet;

        public BasicPingMessage()
        {
        }
        public BasicPingMessage(bool quiet)
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








