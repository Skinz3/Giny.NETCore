using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MapCoordinatesAndId : MapCoordinates  
    { 
        public new const ushort Id = 1303;
        public override ushort TypeId => Id;

        public double mapId;

        public MapCoordinatesAndId()
        {
        }
        public MapCoordinatesAndId(double mapId,short worldX,short worldY)
        {
            this.mapId = mapId;
            this.worldX = worldX;
            this.worldY = worldY;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of MapCoordinatesAndId.mapId.");
            }

        }


    }
}








