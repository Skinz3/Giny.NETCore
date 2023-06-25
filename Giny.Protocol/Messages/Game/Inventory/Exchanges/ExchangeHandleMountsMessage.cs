using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeHandleMountsMessage : NetworkMessage  
    { 
        public  const ushort Id = 4518;
        public override ushort MessageId => Id;

        public byte actionType;
        public int[] ridesId;

        public ExchangeHandleMountsMessage()
        {
        }
        public ExchangeHandleMountsMessage(byte actionType,int[] ridesId)
        {
            this.actionType = actionType;
            this.ridesId = ridesId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)actionType);
            writer.WriteShort((short)ridesId.Length);
            for (uint _i2 = 0;_i2 < ridesId.Length;_i2++)
            {
                if (ridesId[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ridesId[_i2] + ") on element 2 (starting at 1) of ridesId.");
                }

                writer.WriteVarInt((int)ridesId[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            actionType = (byte)reader.ReadByte();
            uint _ridesIdLen = (uint)reader.ReadUShort();
            ridesId = new int[_ridesIdLen];
            for (uint _i2 = 0;_i2 < _ridesIdLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of ridesId.");
                }

                ridesId[_i2] = (int)_val2;
            }

        }


    }
}








