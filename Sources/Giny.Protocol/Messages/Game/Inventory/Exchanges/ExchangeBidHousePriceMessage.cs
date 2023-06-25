using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHousePriceMessage : NetworkMessage  
    { 
        public  const ushort Id = 361;
        public override ushort MessageId => Id;

        public short genId;

        public ExchangeBidHousePriceMessage()
        {
        }
        public ExchangeBidHousePriceMessage(short genId)
        {
            this.genId = genId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (genId < 0)
            {
                throw new System.Exception("Forbidden value (" + genId + ") on element genId.");
            }

            writer.WriteVarShort((short)genId);
        }
        public override void Deserialize(IDataReader reader)
        {
            genId = (short)reader.ReadVarUhShort();
            if (genId < 0)
            {
                throw new System.Exception("Forbidden value (" + genId + ") on element of ExchangeBidHousePriceMessage.genId.");
            }

        }


    }
}








