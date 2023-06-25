using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartedWithStorageMessage : ExchangeStartedMessage  
    { 
        public new const ushort Id = 4535;
        public override ushort MessageId => Id;

        public int storageMaxSlot;

        public ExchangeStartedWithStorageMessage()
        {
        }
        public ExchangeStartedWithStorageMessage(int storageMaxSlot,byte exchangeType)
        {
            this.storageMaxSlot = storageMaxSlot;
            this.exchangeType = exchangeType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (storageMaxSlot < 0)
            {
                throw new System.Exception("Forbidden value (" + storageMaxSlot + ") on element storageMaxSlot.");
            }

            writer.WriteVarInt((int)storageMaxSlot);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            storageMaxSlot = (int)reader.ReadVarUhInt();
            if (storageMaxSlot < 0)
            {
                throw new System.Exception("Forbidden value (" + storageMaxSlot + ") on element of ExchangeStartedWithStorageMessage.storageMaxSlot.");
            }

        }


    }
}








