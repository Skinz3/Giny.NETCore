using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceInvitationAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 4127;
        public override ushort MessageId => Id;

        public bool accept;

        public AllianceInvitationAnswerMessage()
        {
        }
        public AllianceInvitationAnswerMessage(bool accept)
        {
            this.accept = accept;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)accept);
        }
        public override void Deserialize(IDataReader reader)
        {
            accept = (bool)reader.ReadBoolean();
        }


    }
}








