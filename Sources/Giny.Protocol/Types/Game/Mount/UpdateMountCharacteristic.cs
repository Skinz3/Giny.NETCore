using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class UpdateMountCharacteristic  
    { 
        public const ushort Id = 7198;
        public virtual ushort TypeId => Id;

        public byte type;

        public UpdateMountCharacteristic()
        {
        }
        public UpdateMountCharacteristic(byte type)
        {
            this.type = type;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of UpdateMountCharacteristic.type.");
            }

        }


    }
}








