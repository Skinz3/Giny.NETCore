using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class InventoryWeightMessage : NetworkMessage
    {
        public const ushort Id = 4358;
        public override ushort MessageId => Id;

        public int inventoryWeight;
        public int weightMax;

        public InventoryWeightMessage()
        {
        }
        public InventoryWeightMessage(int inventoryWeight, int weightMax)
        {
            this.inventoryWeight = inventoryWeight;
            this.weightMax = weightMax;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (inventoryWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + inventoryWeight + ") on element inventoryWeight.");
            }

            writer.WriteVarInt((int)inventoryWeight);
            if (weightMax < 0)
            {
                throw new System.Exception("Forbidden value (" + weightMax + ") on element weightMax.");
            }

            writer.WriteVarInt((int)weightMax);
        }
        public override void Deserialize(IDataReader reader)
        {
            inventoryWeight = (int)reader.ReadVarUhInt();
            if (inventoryWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + inventoryWeight + ") on element of InventoryWeightMessage.inventoryWeight.");
            }

            weightMax = (int)reader.ReadVarUhInt();
            if (weightMax < 0)
            {
                throw new System.Exception("Forbidden value (" + weightMax + ") on element of InventoryWeightMessage.weightMax.");
            }

        }

    }
}


