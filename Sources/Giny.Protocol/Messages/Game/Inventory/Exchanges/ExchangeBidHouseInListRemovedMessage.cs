using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeBidHouseInListRemovedMessage : NetworkMessage
    {
        public const ushort Id = 8842;
        public override ushort MessageId => Id;

        public int itemUID;
        public int objectGID;
        public int objectType;

        public ExchangeBidHouseInListRemovedMessage()
        {
        }
        public ExchangeBidHouseInListRemovedMessage(int itemUID, int objectGID, int objectType)
        {
            this.itemUID = itemUID;
            this.objectGID = objectGID;
            this.objectType = objectType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)itemUID);
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
        }
        public override void Deserialize(IDataReader reader)
        {
            itemUID = (int)reader.ReadInt();
            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ExchangeBidHouseInListRemovedMessage.objectGID.");
            }

            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of ExchangeBidHouseInListRemovedMessage.objectType.");
            }

        }

    }
}


