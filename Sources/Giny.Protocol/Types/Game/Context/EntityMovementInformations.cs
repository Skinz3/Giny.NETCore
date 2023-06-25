using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class EntityMovementInformations  
    { 
        public const ushort Id = 2953;
        public virtual ushort TypeId => Id;

        public int id;
        public byte[] steps;

        public EntityMovementInformations()
        {
        }
        public EntityMovementInformations(int id,byte[] steps)
        {
            this.id = id;
            this.steps = steps;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)id);
            writer.WriteShort((short)steps.Length);
            for (uint _i2 = 0;_i2 < steps.Length;_i2++)
            {
                writer.WriteByte((byte)steps[_i2]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            int _val2 = 0;
            id = (int)reader.ReadInt();
            uint _stepsLen = (uint)reader.ReadUShort();
            steps = new byte[_stepsLen];
            for (uint _i2 = 0;_i2 < _stepsLen;_i2++)
            {
                _val2 = (int)reader.ReadByte();
                steps[_i2] = (byte)_val2;
            }

        }


    }
}








