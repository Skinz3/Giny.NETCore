using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeReplyTaxVendorMessage : NetworkMessage  
    { 
        public  const ushort Id = 4036;
        public override ushort MessageId => Id;

        public long objectValue;
        public long totalTaxValue;

        public ExchangeReplyTaxVendorMessage()
        {
        }
        public ExchangeReplyTaxVendorMessage(long objectValue,long totalTaxValue)
        {
            this.objectValue = objectValue;
            this.totalTaxValue = totalTaxValue;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectValue < 0 || objectValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + objectValue + ") on element objectValue.");
            }

            writer.WriteVarLong((long)objectValue);
            if (totalTaxValue < 0 || totalTaxValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + totalTaxValue + ") on element totalTaxValue.");
            }

            writer.WriteVarLong((long)totalTaxValue);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectValue = (long)reader.ReadVarUhLong();
            if (objectValue < 0 || objectValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + objectValue + ") on element of ExchangeReplyTaxVendorMessage.objectValue.");
            }

            totalTaxValue = (long)reader.ReadVarUhLong();
            if (totalTaxValue < 0 || totalTaxValue > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + totalTaxValue + ") on element of ExchangeReplyTaxVendorMessage.totalTaxValue.");
            }

        }


    }
}








