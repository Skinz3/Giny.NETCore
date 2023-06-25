using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightLoot  
    { 
        public const ushort Id = 4898;
        public virtual ushort TypeId => Id;

        public int[] objects;
        public long kamas;

        public FightLoot()
        {
        }
        public FightLoot(int[] objects,long kamas)
        {
            this.objects = objects;
            this.kamas = kamas;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objects.Length);
            for (uint _i1 = 0;_i1 < objects.Length;_i1++)
            {
                if (objects[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + objects[_i1] + ") on element 1 (starting at 1) of objects.");
                }

                writer.WriteVarInt((int)objects[_i1]);
            }

            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _objectsLen = (uint)reader.ReadUShort();
            objects = new int[_objectsLen];
            for (uint _i1 = 0;_i1 < _objectsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of objects.");
                }

                objects[_i1] = (int)_val1;
            }

            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of FightLoot.kamas.");
            }

        }


    }
}








