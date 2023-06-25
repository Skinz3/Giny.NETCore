using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PaddockInformationsForSell  
    { 
        public const ushort Id = 3003;
        public virtual ushort TypeId => Id;

        public string guildOwner;
        public short worldX;
        public short worldY;
        public short subAreaId;
        public byte nbMount;
        public byte nbObject;
        public long price;

        public PaddockInformationsForSell()
        {
        }
        public PaddockInformationsForSell(string guildOwner,short worldX,short worldY,short subAreaId,byte nbMount,byte nbObject,long price)
        {
            this.guildOwner = guildOwner;
            this.worldX = worldX;
            this.worldY = worldY;
            this.subAreaId = subAreaId;
            this.nbMount = nbMount;
            this.nbObject = nbObject;
            this.price = price;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)guildOwner);
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element worldX.");
            }

            writer.WriteShort((short)worldX);
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element worldY.");
            }

            writer.WriteShort((short)worldY);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteByte((byte)nbMount);
            writer.WriteByte((byte)nbObject);
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            guildOwner = (string)reader.ReadUTF();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of PaddockInformationsForSell.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of PaddockInformationsForSell.worldY.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PaddockInformationsForSell.subAreaId.");
            }

            nbMount = (byte)reader.ReadByte();
            nbObject = (byte)reader.ReadByte();
            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of PaddockInformationsForSell.price.");
            }

        }


    }
}








