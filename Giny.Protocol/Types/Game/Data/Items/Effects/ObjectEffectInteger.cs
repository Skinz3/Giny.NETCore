using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffectInteger : ObjectEffect  
    { 
        public new const ushort Id = 6724;
        public override ushort TypeId => Id;

        public int value;

        public ObjectEffectInteger()
        {
        }
        public ObjectEffectInteger(int value,short actionId)
        {
            this.value = value;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (value < 0)
            {
                throw new System.Exception("Forbidden value (" + value + ") on element value.");
            }

            writer.WriteVarInt((int)value);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            value = (int)reader.ReadVarUhInt();
            if (value < 0)
            {
                throw new System.Exception("Forbidden value (" + value + ") on element of ObjectEffectInteger.value.");
            }

        }


    }
}








