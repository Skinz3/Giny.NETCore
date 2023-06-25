using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HavenBagFurnitureInformation  
    { 
        public const ushort Id = 3895;
        public virtual ushort TypeId => Id;

        public short cellId;
        public int funitureId;
        public byte orientation;

        public HavenBagFurnitureInformation()
        {
        }
        public HavenBagFurnitureInformation(short cellId,int funitureId,byte orientation)
        {
            this.cellId = cellId;
            this.funitureId = funitureId;
            this.orientation = orientation;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (cellId < 0)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
            writer.WriteInt((int)funitureId);
            if (orientation < 0)
            {
                throw new System.Exception("Forbidden value (" + orientation + ") on element orientation.");
            }

            writer.WriteByte((byte)orientation);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of HavenBagFurnitureInformation.cellId.");
            }

            funitureId = (int)reader.ReadInt();
            orientation = (byte)reader.ReadByte();
            if (orientation < 0)
            {
                throw new System.Exception("Forbidden value (" + orientation + ") on element of HavenBagFurnitureInformation.orientation.");
            }

        }


    }
}








