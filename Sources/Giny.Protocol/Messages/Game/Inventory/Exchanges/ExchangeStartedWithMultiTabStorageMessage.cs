using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeStartedWithMultiTabStorageMessage : ExchangeStartedMessage
    {
        public new const ushort Id = 4807;
        public override ushort MessageId => Id;

        public int storageMaxSlot;
        public int tabNumber;

        public ExchangeStartedWithMultiTabStorageMessage()
        {
        }
        public ExchangeStartedWithMultiTabStorageMessage(int storageMaxSlot, int tabNumber, byte exchangeType)
        {
            this.storageMaxSlot = storageMaxSlot;
            this.tabNumber = tabNumber;
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
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element tabNumber.");
            }

            writer.WriteVarInt((int)tabNumber);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            storageMaxSlot = (int)reader.ReadVarUhInt();
            if (storageMaxSlot < 0)
            {
                throw new System.Exception("Forbidden value (" + storageMaxSlot + ") on element of ExchangeStartedWithMultiTabStorageMessage.storageMaxSlot.");
            }

            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of ExchangeStartedWithMultiTabStorageMessage.tabNumber.");
            }

        }

    }
}


