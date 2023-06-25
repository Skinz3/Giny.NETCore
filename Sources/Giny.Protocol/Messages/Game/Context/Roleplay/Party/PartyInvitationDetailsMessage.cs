using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationDetailsMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 5287;
        public override ushort MessageId => Id;

        public byte partyType;
        public string partyName;
        public long fromId;
        public string fromName;
        public long leaderId;
        public PartyInvitationMemberInformations[] members;
        public PartyGuestInformations[] guests;

        public PartyInvitationDetailsMessage()
        {
        }
        public PartyInvitationDetailsMessage(byte partyType,string partyName,long fromId,string fromName,long leaderId,PartyInvitationMemberInformations[] members,PartyGuestInformations[] guests,int partyId)
        {
            this.partyType = partyType;
            this.partyName = partyName;
            this.fromId = fromId;
            this.fromName = fromName;
            this.leaderId = leaderId;
            this.members = members;
            this.guests = guests;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)partyType);
            writer.WriteUTF((string)partyName);
            if (fromId < 0 || fromId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fromId + ") on element fromId.");
            }

            writer.WriteVarLong((long)fromId);
            writer.WriteUTF((string)fromName);
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element leaderId.");
            }

            writer.WriteVarLong((long)leaderId);
            writer.WriteShort((short)members.Length);
            for (uint _i6 = 0;_i6 < members.Length;_i6++)
            {
                writer.WriteShort((short)(members[_i6] as PartyInvitationMemberInformations).TypeId);
                (members[_i6] as PartyInvitationMemberInformations).Serialize(writer);
            }

            writer.WriteShort((short)guests.Length);
            for (uint _i7 = 0;_i7 < guests.Length;_i7++)
            {
                (guests[_i7] as PartyGuestInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id6 = 0;
            PartyInvitationMemberInformations _item6 = null;
            PartyGuestInformations _item7 = null;
            base.Deserialize(reader);
            partyType = (byte)reader.ReadByte();
            if (partyType < 0)
            {
                throw new System.Exception("Forbidden value (" + partyType + ") on element of PartyInvitationDetailsMessage.partyType.");
            }

            partyName = (string)reader.ReadUTF();
            fromId = (long)reader.ReadVarUhLong();
            if (fromId < 0 || fromId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fromId + ") on element of PartyInvitationDetailsMessage.fromId.");
            }

            fromName = (string)reader.ReadUTF();
            leaderId = (long)reader.ReadVarUhLong();
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element of PartyInvitationDetailsMessage.leaderId.");
            }

            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i6 = 0;_i6 < _membersLen;_i6++)
            {
                _id6 = (uint)reader.ReadUShort();
                _item6 = ProtocolTypeManager.GetInstance<PartyInvitationMemberInformations>((short)_id6);
                _item6.Deserialize(reader);
                members[_i6] = _item6;
            }

            uint _guestsLen = (uint)reader.ReadUShort();
            for (uint _i7 = 0;_i7 < _guestsLen;_i7++)
            {
                _item7 = new PartyGuestInformations();
                _item7.Deserialize(reader);
                guests[_i7] = _item7;
            }

        }


    }
}








