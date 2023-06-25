using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFightLeaveRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7537;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public long characterId;

        public GuildFightLeaveRequestMessage()
        {
        }
        public GuildFightLeaveRequestMessage(double taxCollectorId,long characterId)
        {
            this.taxCollectorId = taxCollectorId;
            this.characterId = characterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            if (characterId < 0 || characterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element characterId.");
            }

            writer.WriteVarLong((long)characterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of GuildFightLeaveRequestMessage.taxCollectorId.");
            }

            characterId = (long)reader.ReadVarUhLong();
            if (characterId < 0 || characterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterId + ") on element of GuildFightLeaveRequestMessage.characterId.");
            }

        }


    }
}








