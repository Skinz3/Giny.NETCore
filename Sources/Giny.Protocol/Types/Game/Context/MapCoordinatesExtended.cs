using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class MapCoordinatesExtended : MapCoordinatesAndId  
    { 
        public new const ushort Id = 7377;
        public override ushort TypeId => Id;

        public short subAreaId;

        public MapCoordinatesExtended()
        {
        }
        public MapCoordinatesExtended(short subAreaId,short worldX,short worldY,double mapId)
        {
            this.subAreaId = subAreaId;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of MapCoordinatesExtended.subAreaId.");
            }

        }


    }
}








