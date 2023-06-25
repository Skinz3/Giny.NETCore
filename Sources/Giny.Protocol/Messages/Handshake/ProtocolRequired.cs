using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ProtocolRequired : NetworkMessage  
    { 
        public  const ushort Id = 1730;
        public override ushort MessageId => Id;

        public string version;

        public ProtocolRequired()
        {
        }
        public ProtocolRequired(string version)
        {
            this.version = version;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)version);
        }
        public override void Deserialize(IDataReader reader)
        {
            version = (string)reader.ReadUTF();
        }


    }
}








