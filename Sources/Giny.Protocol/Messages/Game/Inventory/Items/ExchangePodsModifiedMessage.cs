using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangePodsModifiedMessage : ExchangeObjectMessage  
    { 
        public new const ushort Id = 3914;
        public override ushort MessageId => Id;

        public int currentWeight;
        public int maxWeight;

        public ExchangePodsModifiedMessage()
        {
        }
        public ExchangePodsModifiedMessage(int currentWeight,int maxWeight,bool remote)
        {
            this.currentWeight = currentWeight;
            this.maxWeight = maxWeight;
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
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
            base.Deserialize(reader);
            currentWeight = (int)reader.ReadVarUhInt();
            if (currentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + currentWeight + ") on element of ExchangePodsModifiedMessage.currentWeight.");
            }

            maxWeight = (int)reader.ReadVarUhInt();
            if (maxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + maxWeight + ") on element of ExchangePodsModifiedMessage.maxWeight.");
            }

        }


    }
}








