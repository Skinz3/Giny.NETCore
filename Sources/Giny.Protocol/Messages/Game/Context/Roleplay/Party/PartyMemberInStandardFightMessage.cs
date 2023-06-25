using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyMemberInStandardFightMessage : AbstractPartyMemberInFightMessage  
    { 
        public new const ushort Id = 4603;
        public override ushort MessageId => Id;

        public MapCoordinatesExtended fightMap;

        public PartyMemberInStandardFightMessage()
        {
        }
        public PartyMemberInStandardFightMessage(MapCoordinatesExtended fightMap,int partyId,byte reason,long memberId,int memberAccountId,string memberName,short fightId,short timeBeforeFightStart)
        {
            this.fightMap = fightMap;
            this.partyId = partyId;
            this.reason = reason;
            this.memberId = memberId;
            this.memberAccountId = memberAccountId;
            this.memberName = memberName;
            this.fightId = fightId;
            this.timeBeforeFightStart = timeBeforeFightStart;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            fightMap.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            fightMap = new MapCoordinatesExtended();
            fightMap.Deserialize(reader);
        }


    }
}








