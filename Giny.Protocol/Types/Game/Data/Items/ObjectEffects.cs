using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffects  
    { 
        public const ushort Id = 1337;
        public virtual ushort TypeId => Id;

        public ObjectEffect[] effects;

        public ObjectEffects()
        {
        }
        public ObjectEffects(ObjectEffect[] effects)
        {
            this.effects = effects;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)effects.Length);
            for (uint _i1 = 0;_i1 < effects.Length;_i1++)
            {
                writer.WriteShort((short)(effects[_i1] as ObjectEffect).TypeId);
                (effects[_i1] as ObjectEffect).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            ObjectEffect _item1 = null;
            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _effectsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id1);
                _item1.Deserialize(reader);
                effects[_i1] = _item1;
            }

        }


    }
}








