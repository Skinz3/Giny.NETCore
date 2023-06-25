using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyUpdateMessage : AbstractPartyEventMessage  
    { 
        public new const ushort Id = 465;
        public override ushort MessageId => Id;

        public PartyMemberInformations memberInformations;

        public PartyUpdateMessage()
        {
        }
        public PartyUpdateMessage(PartyMemberInformations memberInformations,int partyId)
        {
            this.memberInformations = memberInformations;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)memberInformations.TypeId);
            memberInformations.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            uint _id1 = (uint)reader.ReadUShort();
            memberInformations = ProtocolTypeManager.GetInstance<PartyMemberInformations>((short)_id1);
            memberInformations.Deserialize(reader);
        }


    }
}








