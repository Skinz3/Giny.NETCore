using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AuthenticationTicketMessage : NetworkMessage  
    { 
        public  const ushort Id = 6334;
        public override ushort MessageId => Id;

        public string lang;
        public string ticket;

        public AuthenticationTicketMessage()
        {
        }
        public AuthenticationTicketMessage(string lang,string ticket)
        {
            this.lang = lang;
            this.ticket = ticket;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)lang);
            writer.WriteUTF((string)ticket);
        }
        public override void Deserialize(IDataReader reader)
        {
            lang = (string)reader.ReadUTF();
            ticket = (string)reader.ReadUTF();
        }


    }
}








