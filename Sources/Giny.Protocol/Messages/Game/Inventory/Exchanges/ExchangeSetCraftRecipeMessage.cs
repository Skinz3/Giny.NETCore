using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeSetCraftRecipeMessage : NetworkMessage
    {
        public const ushort Id = 883;
        public override ushort MessageId => Id;

        public int objectGID;

        public ExchangeSetCraftRecipeMessage()
        {
        }
        public ExchangeSetCraftRecipeMessage(int objectGID)
        {
            this.objectGID = objectGID;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarInt((int)objectGID);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ExchangeSetCraftRecipeMessage.objectGID.");
            }

        }

    }
}


