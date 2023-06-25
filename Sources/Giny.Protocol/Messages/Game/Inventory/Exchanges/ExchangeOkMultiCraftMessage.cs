using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeOkMultiCraftMessage : NetworkMessage  
    { 
        public  const ushort Id = 8687;
        public override ushort MessageId => Id;

        public long initiatorId;
        public long otherId;
        public byte role;

        public ExchangeOkMultiCraftMessage()
        {
        }
        public ExchangeOkMultiCraftMessage(long initiatorId,long otherId,byte role)
        {
            this.initiatorId = initiatorId;
            this.otherId = otherId;
            this.role = role;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (initiatorId < 0 || initiatorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + initiatorId + ") on element initiatorId.");
            }

            writer.WriteVarLong((long)initiatorId);
            if (otherId < 0 || otherId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + otherId + ") on element otherId.");
            }

            writer.WriteVarLong((long)otherId);
            writer.WriteByte((byte)role);
        }
        public override void Deserialize(IDataReader reader)
        {
            initiatorId = (long)reader.ReadVarUhLong();
            if (initiatorId < 0 || initiatorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + initiatorId + ") on element of ExchangeOkMultiCraftMessage.initiatorId.");
            }

            otherId = (long)reader.ReadVarUhLong();
            if (otherId < 0 || otherId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + otherId + ") on element of ExchangeOkMultiCraftMessage.otherId.");
            }

            role = (byte)reader.ReadByte();
        }


    }
}








