using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MapCoordinates  
    { 
        public const ushort Id = 5479;
        public virtual ushort TypeId => Id;

        public short worldX;
        public short worldY;

        public MapCoordinates()
        {
        }
        public MapCoordinates(short worldX,short worldY)
        {
            this.worldX = worldX;
            this.worldY = worldY;
        }
        public virtual void Serialize(IDataWriter writer)
        {
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
        }
        public virtual void Deserialize(IDataReader reader)
        {
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of MapCoordinates.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of MapCoordinates.worldY.");
            }

        }


    }
}








