using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInvitationStateRecrutedMessage : NetworkMessage  
    { 
        public  const ushort Id = 3106;
        public override ushort MessageId => Id;

        public byte invitationState;

        public GuildInvitationStateRecrutedMessage()
        {
        }
        public GuildInvitationStateRecrutedMessage(byte invitationState)
        {
            this.invitationState = invitationState;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)invitationState);
        }
        public override void Deserialize(IDataReader reader)
        {
            invitationState = (byte)reader.ReadByte();
            if (invitationState < 0)
            {
                throw new System.Exception("Forbidden value (" + invitationState + ") on element of GuildInvitationStateRecrutedMessage.invitationState.");
            }

        }


    }
}








