using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeWeightMessage : NetworkMessage  
    { 
        public  const ushort Id = 1225;
        public override ushort MessageId => Id;

        public int currentWeight;
        public int maxWeight;

        public ExchangeWeightMessage()
        {
        }
        public ExchangeWeightMessage(int currentWeight,int maxWeight)
        {
            this.currentWeight = currentWeight;
            this.maxWeight = maxWeight;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (currentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + currentWeight + ") on element currentWeight.");
            }

            writer.WriteVarInt((int)currentWeight);
            if (maxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + maxWeight + ") on element maxWeight.");
            }

            writer.WriteVarInt((int)maxWeight);
        }
        public override void Deserialize(IDataReader reader)
        {
            currentWeight = (int)reader.ReadVarUhInt();
            if (currentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + currentWeight + ") on element of ExchangeWeightMessage.currentWeight.");
            }

            maxWeight = (int)reader.ReadVarUhInt();
            if (maxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + maxWeight + ") on element of ExchangeWeightMessage.maxWeight.");
            }

        }


    }
}








