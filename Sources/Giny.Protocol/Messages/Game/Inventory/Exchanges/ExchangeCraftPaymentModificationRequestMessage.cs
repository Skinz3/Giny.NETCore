using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeCraftPaymentModificationRequestMessage : NetworkMessage
    {
        public const ushort Id = 4312;
        public override ushort MessageId => Id;

        public long quantity;

        public ExchangeCraftPaymentModificationRequestMessage()
        {
        }
        public ExchangeCraftPaymentModificationRequestMessage(long quantity)
        {
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (quantity < 0 || quantity > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarLong((long)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            quantity = (long)reader.ReadVarUhLong();
            if (quantity < 0 || quantity > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ExchangeCraftPaymentModificationRequestMessage.quantity.");
            }

        }

    }
}


