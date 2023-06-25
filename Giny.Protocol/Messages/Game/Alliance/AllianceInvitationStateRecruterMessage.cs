using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceInvitationStateRecruterMessage : NetworkMessage  
    { 
        public  const ushort Id = 5885;
        public override ushort MessageId => Id;

        public string recrutedName;
        public byte invitationState;

        public AllianceInvitationStateRecruterMessage()
        {
        }
        public AllianceInvitationStateRecruterMessage(string recrutedName,byte invitationState)
        {
            this.recrutedName = recrutedName;
            this.invitationState = invitationState;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)recrutedName);
            writer.WriteByte((byte)invitationState);
        }
        public override void Deserialize(IDataReader reader)
        {
            recrutedName = (string)reader.ReadUTF();
            invitationState = (byte)reader.ReadByte();
            if (invitationState < 0)
            {
                throw new System.Exception("Forbidden value (" + invitationState + ") on element of AllianceInvitationStateRecruterMessage.invitationState.");
            }

        }


    }
}








