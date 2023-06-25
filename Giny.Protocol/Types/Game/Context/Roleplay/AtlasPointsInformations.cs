using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AtlasPointsInformations  
    { 
        public const ushort Id = 6354;
        public virtual ushort TypeId => Id;

        public byte type;
        public MapCoordinatesExtended[] coords;

        public AtlasPointsInformations()
        {
        }
        public AtlasPointsInformations(byte type,MapCoordinatesExtended[] coords)
        {
            this.type = type;
            this.coords = coords;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteShort((short)coords.Length);
            for (uint _i2 = 0;_i2 < coords.Length;_i2++)
            {
                (coords[_i2] as MapCoordinatesExtended).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            MapCoordinatesExtended _item2 = null;
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of AtlasPointsInformations.type.");
            }

            uint _coordsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _coordsLen;_i2++)
            {
                _item2 = new MapCoordinatesExtended();
                _item2.Deserialize(reader);
                coords[_i2] = _item2;
            }

        }


    }
}








