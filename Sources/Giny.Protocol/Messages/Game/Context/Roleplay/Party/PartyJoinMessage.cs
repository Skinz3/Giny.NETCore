using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyJoinMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 2640;
        public override ushort MessageId => Id;

        public byte partyType;
        public long partyLeaderId;
        public byte maxParticipants;
        public PartyMemberInformations[] members;
        public PartyGuestInformations[] guests;
        public bool restricted;
        public string partyName;

        public PartyJoinMessage()
        {
        }
        public PartyJoinMessage(byte partyType,long partyLeaderId,byte maxParticipants,PartyMemberInformations[] members,PartyGuestInformations[] guests,bool restricted,string partyName,int partyId)
        {
            this.partyType = partyType;
            this.partyLeaderId = partyLeaderId;
            this.maxParticipants = maxParticipants;
            this.members = members;
            this.guests = guests;
            this.restricted = restricted;
            this.partyName = partyName;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)partyType);
            if (partyLeaderId < 0 || partyLeaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + partyLeaderId + ") on element partyLeaderId.");
            }

            writer.WriteVarLong((long)partyLeaderId);
            if (maxParticipants < 0)
            {
                throw new System.Exception("Forbidden value (" + maxParticipants + ") on element maxParticipants.");
            }

            writer.WriteByte((byte)maxParticipants);
            writer.WriteShort((short)members.Length);
            for (uint _i4 = 0;_i4 < members.Length;_i4++)
            {
                writer.WriteShort((short)(members[_i4] as PartyMemberInformations).TypeId);
                (members[_i4] as PartyMemberInformations).Serialize(writer);
            }

            writer.WriteShort((short)guests.Length);
            for (uint _i5 = 0;_i5 < guests.Length;_i5++)
            {
                (guests[_i5] as PartyGuestInformations).Serialize(writer);
            }

            writer.WriteBoolean((bool)restricted);
            writer.WriteUTF((string)partyName);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id4 = 0;
            PartyMemberInformations _item4 = null;
            PartyGuestInformations _item5 = null;
            base.Deserialize(reader);
            partyType = (byte)reader.ReadByte();
            if (partyType < 0)
            {
                throw new System.Exception("Forbidden value (" + partyType + ") on element of PartyJoinMessage.partyType.");
            }

            partyLeaderId = (long)reader.ReadVarUhLong();
            if (partyLeaderId < 0 || partyLeaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + partyLeaderId + ") on element of PartyJoinMessage.partyLeaderId.");
            }

            maxParticipants = (byte)reader.ReadByte();
            if (maxParticipants < 0)
            {
                throw new System.Exception("Forbidden value (" + maxParticipants + ") on element of PartyJoinMessage.maxParticipants.");
            }

            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _membersLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<PartyMemberInformations>((short)_id4);
                _item4.Deserialize(reader);
                members[_i4] = _item4;
            }

            uint _guestsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _guestsLen;_i5++)
            {
                _item5 = new PartyGuestInformations();
                _item5.Deserialize(reader);
                guests[_i5] = _item5;
            }

            restricted = (bool)reader.ReadBoolean();
            partyName = (string)reader.ReadUTF();
        }


    }
}








