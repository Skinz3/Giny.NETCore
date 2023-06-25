using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ActorOrientation  
    { 
        public const ushort Id = 6283;
        public virtual ushort TypeId => Id;

        public double id;
        public byte direction;

        public ActorOrientation()
        {
        }
        public ActorOrientation(double id,byte direction)
        {
            this.id = id;
            this.direction = direction;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            writer.WriteByte((byte)direction);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ActorOrientation.id.");
            }

            direction = (byte)reader.ReadByte();
            if (direction < 0)
            {
                throw new System.Exception("Forbidden value (" + direction + ") on element of ActorOrientation.direction.");
            }

        }


    }
}








