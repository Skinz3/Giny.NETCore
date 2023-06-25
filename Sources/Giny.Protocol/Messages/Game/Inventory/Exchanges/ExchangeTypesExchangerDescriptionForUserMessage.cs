using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeTypesExchangerDescriptionForUserMessage : NetworkMessage  
    { 
        public  const ushort Id = 5329;
        public override ushort MessageId => Id;

        public int objectType;
        public int[] typeDescription;

        public ExchangeTypesExchangerDescriptionForUserMessage()
        {
        }
        public ExchangeTypesExchangerDescriptionForUserMessage(int objectType,int[] typeDescription)
        {
            this.objectType = objectType;
            this.typeDescription = typeDescription;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element objectType.");
            }

            writer.WriteInt((int)objectType);
            writer.WriteShort((short)typeDescription.Length);
            for (uint _i2 = 0;_i2 < typeDescription.Length;_i2++)
            {
                if (typeDescription[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + typeDescription[_i2] + ") on element 2 (starting at 1) of typeDescription.");
                }

                writer.WriteVarInt((int)typeDescription[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of ExchangeTypesExchangerDescriptionForUserMessage.objectType.");
            }

            uint _typeDescriptionLen = (uint)reader.ReadUShort();
            typeDescription = new int[_typeDescriptionLen];
            for (uint _i2 = 0;_i2 < _typeDescriptionLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of typeDescription.");
                }

                typeDescription[_i2] = (int)_val2;
            }

        }


    }
}








