using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CompassUpdatePartyMemberMessage : CompassUpdateMessage  
    { 
        public new const ushort Id = 598;
        public override ushort MessageId => Id;

        public long memberId;
        public bool active;

        public CompassUpdatePartyMemberMessage()
        {
        }
        public CompassUpdatePartyMemberMessage(long memberId,bool active,byte type,MapCoordinates coords)
        {
            this.memberId = memberId;
            this.active = active;
            this.type = type;
            this.coords = coords;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteBoolean((bool)active);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of CompassUpdatePartyMemberMessage.memberId.");
            }

            active = (bool)reader.ReadBoolean();
        }


    }
}








