using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeOnHumanVendorRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3748;
        public override ushort MessageId => Id;

        public long humanVendorId;
        public short humanVendorCell;

        public ExchangeOnHumanVendorRequestMessage()
        {
        }
        public ExchangeOnHumanVendorRequestMessage(long humanVendorId,short humanVendorCell)
        {
            this.humanVendorId = humanVendorId;
            this.humanVendorCell = humanVendorCell;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (humanVendorId < 0 || humanVendorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + humanVendorId + ") on element humanVendorId.");
            }

            writer.WriteVarLong((long)humanVendorId);
            if (humanVendorCell < 0 || humanVendorCell > 559)
            {
                throw new System.Exception("Forbidden value (" + humanVendorCell + ") on element humanVendorCell.");
            }

            writer.WriteVarShort((short)humanVendorCell);
        }
        public override void Deserialize(IDataReader reader)
        {
            humanVendorId = (long)reader.ReadVarUhLong();
            if (humanVendorId < 0 || humanVendorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + humanVendorId + ") on element of ExchangeOnHumanVendorRequestMessage.humanVendorId.");
            }

            humanVendorCell = (short)reader.ReadVarUhShort();
            if (humanVendorCell < 0 || humanVendorCell > 559)
            {
                throw new System.Exception("Forbidden value (" + humanVendorCell + ") on element of ExchangeOnHumanVendorRequestMessage.humanVendorCell.");
            }

        }


    }
}








