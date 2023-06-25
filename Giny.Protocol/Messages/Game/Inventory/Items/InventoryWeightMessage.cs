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
        public  const ushort Id = 4739;
        public override ushort MessageId => Id;

        public int inventoryWeight;
        public int shopWeight;
        public int weightMax;

        public InventoryWeightMessage()
        {
        }
        public InventoryWeightMessage(int inventoryWeight,int shopWeight,int weightMax)
        {
            this.inventoryWeight = inventoryWeight;
            this.shopWeight = shopWeight;
            this.weightMax = weightMax;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (inventoryWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + inventoryWeight + ") on element inventoryWeight.");
            }

            writer.WriteVarInt((int)inventoryWeight);
            if (shopWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + shopWeight + ") on element shopWeight.");
            }

            writer.WriteVarInt((int)shopWeight);
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

            shopWeight = (int)reader.ReadVarUhInt();
            if (shopWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + shopWeight + ") on element of InventoryWeightMessage.shopWeight.");
            }

            weightMax = (int)reader.ReadVarUhInt();
            if (weightMax < 0)
            {
                throw new System.Exception("Forbidden value (" + weightMax + ") on element of InventoryWeightMessage.weightMax.");
            }

        }


    }
}








