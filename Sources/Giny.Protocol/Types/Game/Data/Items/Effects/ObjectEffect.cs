using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ObjectEffect  
    { 
        public const ushort Id = 9255;
        public virtual ushort TypeId => Id;

        public short actionId;

        public ObjectEffect()
        {
        }
        public ObjectEffect(short actionId)
        {
            this.actionId = actionId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteVarShort((short)actionId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            actionId = (short)reader.ReadVarUhShort();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of ObjectEffect.actionId.");
            }

        }


    }
}








