using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockToSellFilterMessage : NetworkMessage  
    { 
        public  const ushort Id = 6894;
        public override ushort MessageId => Id;

        public int areaId;
        public byte atLeastNbMount;
        public byte atLeastNbMachine;
        public long maxPrice;
        public byte orderBy;

        public PaddockToSellFilterMessage()
        {
        }
        public PaddockToSellFilterMessage(int areaId,byte atLeastNbMount,byte atLeastNbMachine,long maxPrice,byte orderBy)
        {
            this.areaId = areaId;
            this.atLeastNbMount = atLeastNbMount;
            this.atLeastNbMachine = atLeastNbMachine;
            this.maxPrice = maxPrice;
            this.orderBy = orderBy;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)areaId);
            writer.WriteByte((byte)atLeastNbMount);
            writer.WriteByte((byte)atLeastNbMachine);
            if (maxPrice < 0 || maxPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + maxPrice + ") on element maxPrice.");
            }

            writer.WriteVarLong((long)maxPrice);
            writer.WriteByte((byte)orderBy);
        }
        public override void Deserialize(IDataReader reader)
        {
            areaId = (int)reader.ReadInt();
            atLeastNbMount = (byte)reader.ReadByte();
            atLeastNbMachine = (byte)reader.ReadByte();
            maxPrice = (long)reader.ReadVarUhLong();
            if (maxPrice < 0 || maxPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + maxPrice + ") on element of PaddockToSellFilterMessage.maxPrice.");
            }

            orderBy = (byte)reader.ReadByte();
            if (orderBy < 0)
            {
                throw new System.Exception("Forbidden value (" + orderBy + ") on element of PaddockToSellFilterMessage.orderBy.");
            }

        }


    }
}








