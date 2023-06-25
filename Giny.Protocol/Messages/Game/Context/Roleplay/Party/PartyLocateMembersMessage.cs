using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyLocateMembersMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 2028;
        public override ushort MessageId => Id;

        public PartyMemberGeoPosition[] geopositions;

        public PartyLocateMembersMessage()
        {
        }
        public PartyLocateMembersMessage(PartyMemberGeoPosition[] geopositions,int partyId)
        {
            this.geopositions = geopositions;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)geopositions.Length);
            for (uint _i1 = 0;_i1 < geopositions.Length;_i1++)
            {
                (geopositions[_i1] as PartyMemberGeoPosition).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            PartyMemberGeoPosition _item1 = null;
            base.Deserialize(reader);
            uint _geopositionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _geopositionsLen;_i1++)
            {
                _item1 = new PartyMemberGeoPosition();
                _item1.Deserialize(reader);
                geopositions[_i1] = _item1;
            }

        }


    }
}








