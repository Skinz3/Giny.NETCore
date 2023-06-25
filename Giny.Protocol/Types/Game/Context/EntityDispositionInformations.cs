using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class EntityDispositionInformations  
    { 
        public const ushort Id = 2227;
        public virtual ushort TypeId => Id;

        public short cellId;
        public byte direction;

        public EntityDispositionInformations()
        {
        }
        public EntityDispositionInformations(short cellId,byte direction)
        {
            this.cellId = cellId;
            this.direction = direction;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteShort((short)cellId);
            writer.WriteByte((byte)direction);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadShort();
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of EntityDispositionInformations.cellId.");
            }

            direction = (byte)reader.ReadByte();
            if (direction < 0)
            {
                throw new System.Exception("Forbidden value (" + direction + ") on element of EntityDispositionInformations.direction.");
            }

        }


    }
}








