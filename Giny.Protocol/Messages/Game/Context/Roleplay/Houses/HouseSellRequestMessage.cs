using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseSellRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3852;
        public override ushort MessageId => Id;

        public int instanceId;
        public long amount;
        public bool forSale;

        public HouseSellRequestMessage()
        {
        }
        public HouseSellRequestMessage(int instanceId,long amount,bool forSale)
        {
            this.instanceId = instanceId;
            this.amount = amount;
            this.forSale = forSale;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }

            writer.WriteInt((int)instanceId);
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element amount.");
            }

            writer.WriteVarLong((long)amount);
            writer.WriteBoolean((bool)forSale);
        }
        public override void Deserialize(IDataReader reader)
        {
            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseSellRequestMessage.instanceId.");
            }

            amount = (long)reader.ReadVarUhLong();
            if (amount < 0 || amount > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + amount + ") on element of HouseSellRequestMessage.amount.");
            }

            forSale = (bool)reader.ReadBoolean();
        }


    }
}








