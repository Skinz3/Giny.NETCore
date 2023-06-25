using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage  
    { 
        public new const ushort Id = 8088;
        public override ushort MessageId => Id;

        public long replacedCharacterId;

        public GuildFightTakePlaceRequestMessage()
        {
        }
        public GuildFightTakePlaceRequestMessage(long replacedCharacterId,double taxCollectorId)
        {
            this.replacedCharacterId = replacedCharacterId;
            this.taxCollectorId = taxCollectorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (replacedCharacterId < 0 || replacedCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + replacedCharacterId + ") on element replacedCharacterId.");
            }

            writer.WriteVarLong((long)replacedCharacterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            replacedCharacterId = (long)reader.ReadVarUhLong();
            if (replacedCharacterId < 0 || replacedCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + replacedCharacterId + ") on element of GuildFightTakePlaceRequestMessage.replacedCharacterId.");
            }

        }


    }
}








