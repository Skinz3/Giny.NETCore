using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeBidHouseGenericItemRemovedMessage : NetworkMessage
    {
        public const ushort Id = 5061;
        public override ushort MessageId => Id;

        public int objGenericId;

        public ExchangeBidHouseGenericItemRemovedMessage()
        {
        }
        public ExchangeBidHouseGenericItemRemovedMessage(int objGenericId)
        {
            this.objGenericId = objGenericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + objGenericId + ") on element objGenericId.");
            }

            writer.WriteVarInt((int)objGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            objGenericId = (int)reader.ReadVarUhInt();
            if (objGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + objGenericId + ") on element of ExchangeBidHouseGenericItemRemovedMessage.objGenericId.");
            }

        }

    }
}


