using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseToSellFilterMessage : NetworkMessage  
    { 
        public  const ushort Id = 1342;
        public override ushort MessageId => Id;

        public int areaId;
        public byte atLeastNbRoom;
        public byte atLeastNbChest;
        public short skillRequested;
        public long maxPrice;
        public byte orderBy;

        public HouseToSellFilterMessage()
        {
        }
        public HouseToSellFilterMessage(int areaId,byte atLeastNbRoom,byte atLeastNbChest,short skillRequested,long maxPrice,byte orderBy)
        {
            this.areaId = areaId;
            this.atLeastNbRoom = atLeastNbRoom;
            this.atLeastNbChest = atLeastNbChest;
            this.skillRequested = skillRequested;
            this.maxPrice = maxPrice;
            this.orderBy = orderBy;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)areaId);
            if (atLeastNbRoom < 0)
            {
                throw new System.Exception("Forbidden value (" + atLeastNbRoom + ") on element atLeastNbRoom.");
            }

            writer.WriteByte((byte)atLeastNbRoom);
            if (atLeastNbChest < 0)
            {
                throw new System.Exception("Forbidden value (" + atLeastNbChest + ") on element atLeastNbChest.");
            }

            writer.WriteByte((byte)atLeastNbChest);
            if (skillRequested < 0)
            {
                throw new System.Exception("Forbidden value (" + skillRequested + ") on element skillRequested.");
            }

            writer.WriteVarShort((short)skillRequested);
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
            atLeastNbRoom = (byte)reader.ReadByte();
            if (atLeastNbRoom < 0)
            {
                throw new System.Exception("Forbidden value (" + atLeastNbRoom + ") on element of HouseToSellFilterMessage.atLeastNbRoom.");
            }

            atLeastNbChest = (byte)reader.ReadByte();
            if (atLeastNbChest < 0)
            {
                throw new System.Exception("Forbidden value (" + atLeastNbChest + ") on element of HouseToSellFilterMessage.atLeastNbChest.");
            }

            skillRequested = (short)reader.ReadVarUhShort();
            if (skillRequested < 0)
            {
                throw new System.Exception("Forbidden value (" + skillRequested + ") on element of HouseToSellFilterMessage.skillRequested.");
            }

            maxPrice = (long)reader.ReadVarUhLong();
            if (maxPrice < 0 || maxPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + maxPrice + ") on element of HouseToSellFilterMessage.maxPrice.");
            }

            orderBy = (byte)reader.ReadByte();
            if (orderBy < 0)
            {
                throw new System.Exception("Forbidden value (" + orderBy + ") on element of HouseToSellFilterMessage.orderBy.");
            }

        }


    }
}








