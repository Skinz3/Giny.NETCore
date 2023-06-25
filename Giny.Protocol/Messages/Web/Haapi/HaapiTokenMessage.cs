using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiTokenMessage : NetworkMessage  
    { 
        public  const ushort Id = 876;
        public override ushort MessageId => Id;

        public string token;

        public HaapiTokenMessage()
        {
        }
        public HaapiTokenMessage(string token)
        {
            this.token = token;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)token);
        }
        public override void Deserialize(IDataReader reader)
        {
            token = (string)reader.ReadUTF();
        }


    }
}








