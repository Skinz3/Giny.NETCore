using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeTypesItemsExchangerDescriptionForUserMessage : NetworkMessage
    {
        public const ushort Id = 177;
        public override ushort MessageId => Id;

        public int objectGID;
        public int objectType;
        public BidExchangerObjectInfo[] itemTypeDescriptions;

        public ExchangeTypesItemsExchangerDescriptionForUserMessage()
        {
        }
        public ExchangeTypesItemsExchangerDescriptionForUserMessage(int objectGID, int objectType, BidExchangerObjectInfo[] itemTypeDescriptions)
        {
            this.objectGID = objectGID;
            this.objectType = objectType;
            this.itemTypeDescriptions = itemTypeDescriptions;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarInt((int)objectGID);
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element objectType.");
            }

            writer.WriteInt((int)objectType);
            writer.WriteShort((short)itemTypeDescriptions.Length);
            for (uint _i3 = 0; _i3 < itemTypeDescriptions.Length; _i3++)
            {
                (itemTypeDescriptions[_i3] as BidExchangerObjectInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            BidExchangerObjectInfo _item3 = null;
            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ExchangeTypesItemsExchangerDescriptionForUserMessage.objectGID.");
            }

            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of ExchangeTypesItemsExchangerDescriptionForUserMessage.objectType.");
            }

            uint _itemTypeDescriptionsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0; _i3 < _itemTypeDescriptionsLen; _i3++)
            {
                _item3 = new BidExchangerObjectInfo();
                _item3.Deserialize(reader);
                itemTypeDescriptions[_i3] = _item3;
            }

        }

    }
}


