using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeObjectMoveToTabMessage : NetworkMessage
    {
        public const ushort Id = 5263;
        public override ushort MessageId => Id;

        public int objectUID;
        public int quantity;
        public int tabNumber;

        public ExchangeObjectMoveToTabMessage()
        {
        }
        public ExchangeObjectMoveToTabMessage(int objectUID, int quantity, int tabNumber)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
            this.tabNumber = tabNumber;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            writer.WriteVarInt((int)quantity);
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element tabNumber.");
            }

            writer.WriteVarInt((int)tabNumber);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ExchangeObjectMoveToTabMessage.objectUID.");
            }

            quantity = (int)reader.ReadVarInt();
            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of ExchangeObjectMoveToTabMessage.tabNumber.");
            }

        }

    }
}


