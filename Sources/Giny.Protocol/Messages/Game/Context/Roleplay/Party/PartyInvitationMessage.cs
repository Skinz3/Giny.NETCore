using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 7030;
        public override ushort MessageId => Id;

        public byte partyType;
        public string partyName;
        public byte maxParticipants;
        public long fromId;
        public string fromName;
        public long toId;

        public PartyInvitationMessage()
        {
        }
        public PartyInvitationMessage(byte partyType,string partyName,byte maxParticipants,long fromId,string fromName,long toId,int partyId)
        {
            this.partyType = partyType;
            this.partyName = partyName;
            this.maxParticipants = maxParticipants;
            this.fromId = fromId;
            this.fromName = fromName;
            this.toId = toId;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)partyType);
            writer.WriteUTF((string)partyName);
            if (maxParticipants < 0)
            {
                throw new System.Exception("Forbidden value (" + maxParticipants + ") on element maxParticipants.");
            }

            writer.WriteByte((byte)maxParticipants);
            if (fromId < 0 || fromId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fromId + ") on element fromId.");
            }

            writer.WriteVarLong((long)fromId);
            writer.WriteUTF((string)fromName);
            if (toId < 0 || toId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + toId + ") on element toId.");
            }

            writer.WriteVarLong((long)toId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            partyType = (byte)reader.ReadByte();
            if (partyType < 0)
            {
                throw new System.Exception("Forbidden value (" + partyType + ") on element of PartyInvitationMessage.partyType.");
            }

            partyName = (string)reader.ReadUTF();
            maxParticipants = (byte)reader.ReadByte();
            if (maxParticipants < 0)
            {
                throw new System.Exception("Forbidden value (" + maxParticipants + ") on element of PartyInvitationMessage.maxParticipants.");
            }

            fromId = (long)reader.ReadVarUhLong();
            if (fromId < 0 || fromId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fromId + ") on element of PartyInvitationMessage.fromId.");
            }

            fromName = (string)reader.ReadUTF();
            toId = (long)reader.ReadVarUhLong();
            if (toId < 0 || toId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + toId + ") on element of PartyInvitationMessage.toId.");
            }

        }


    }
}








