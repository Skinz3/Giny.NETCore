using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightLoot
    {
        public const ushort Id = 5092;
        public virtual ushort TypeId => Id;

        public FightLootObject[] objects;
        public long kamas;

        public FightLoot()
        {
        }
        public FightLoot(FightLootObject[] objects, long kamas)
        {
            this.objects = objects;
            this.kamas = kamas;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)objects.Length);
            for (uint _i1 = 0; _i1 < objects.Length; _i1++)
            {
                (objects[_i1] as FightLootObject).Serialize(writer);
            }

            if (kamas < 0 || kamas > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element kamas.");
            }

            writer.WriteVarLong((long)kamas);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            FightLootObject _item1 = null;
            uint _objectsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _objectsLen; _i1++)
            {
                _item1 = new FightLootObject();
                _item1.Deserialize(reader);
                objects[_i1] = _item1;
            }

            kamas = (long)reader.ReadVarUhLong();
            if (kamas < 0 || kamas > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kamas + ") on element of FightLoot.kamas.");
            }

        }


    }
}


