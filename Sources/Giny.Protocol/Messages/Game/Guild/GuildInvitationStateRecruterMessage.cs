using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInvitationStateRecruterMessage : NetworkMessage  
    { 
        public  const ushort Id = 2787;
        public override ushort MessageId => Id;

        public string recrutedName;
        public byte invitationState;

        public GuildInvitationStateRecruterMessage()
        {
        }
        public GuildInvitationStateRecruterMessage(string recrutedName,byte invitationState)
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
                throw new System.Exception("Forbidden value (" + invitationState + ") on element of GuildInvitationStateRecruterMessage.invitationState.");
            }

        }


    }
}








