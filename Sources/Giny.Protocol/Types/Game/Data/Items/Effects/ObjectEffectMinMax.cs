using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffectMinMax : ObjectEffect  
    { 
        public new const ushort Id = 9563;
        public override ushort TypeId => Id;

        public int min;
        public int max;

        public ObjectEffectMinMax()
        {
        }
        public ObjectEffectMinMax(int min,int max,short actionId)
        {
            this.min = min;
            this.max = max;
            this.actionId = actionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (min < 0)
            {
                throw new System.Exception("Forbidden value (" + min + ") on element min.");
            }

            writer.WriteVarInt((int)min);
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element max.");
            }

            writer.WriteVarInt((int)max);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            min = (int)reader.ReadVarUhInt();
            if (min < 0)
            {
                throw new System.Exception("Forbidden value (" + min + ") on element of ObjectEffectMinMax.min.");
            }

            max = (int)reader.ReadVarUhInt();
            if (max < 0)
            {
                throw new System.Exception("Forbidden value (" + max + ") on element of ObjectEffectMinMax.max.");
            }

        }


    }
}








