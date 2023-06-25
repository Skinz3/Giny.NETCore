using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AdminCommandMessage : NetworkMessage  
    { 
        public  const ushort Id = 3581;
        public override ushort MessageId => Id;

        public string content;

        public AdminCommandMessage()
        {
        }
        public AdminCommandMessage(string content)
        {
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)content);
        }
        public override void Deserialize(IDataReader reader)
        {
            content = (string)reader.ReadUTF();
        }


    }
}








