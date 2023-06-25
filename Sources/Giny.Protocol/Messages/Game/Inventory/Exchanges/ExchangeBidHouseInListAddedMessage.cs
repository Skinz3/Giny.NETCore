using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseInListAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 7717;
        public override ushort MessageId => Id;

        public int itemUID;
        public short objectGID;
        public int objectType;
        public ObjectEffect[] effects;
        public long[] prices;

        public ExchangeBidHouseInListAddedMessage()
        {
        }
        public ExchangeBidHouseInListAddedMessage(int itemUID,short objectGID,int objectType,ObjectEffect[] effects,long[] prices)
        {
            this.itemUID = itemUID;
            this.objectGID = objectGID;
            this.objectType = objectType;
            this.effects = effects;
            this.prices = prices;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)itemUID);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element objectType.");
            }

            writer.WriteInt((int)objectType);
            writer.WriteShort((short)effects.Length);
            for (uint _i4 = 0;_i4 < effects.Length;_i4++)
            {
                writer.WriteShort((short)(effects[_i4] as ObjectEffect).TypeId);
                (effects[_i4] as ObjectEffect).Serialize(writer);
            }

            writer.WriteShort((short)prices.Length);
            for (uint _i5 = 0;_i5 < prices.Length;_i5++)
            {
                if (prices[_i5] < 0 || prices[_i5] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + prices[_i5] + ") on element 5 (starting at 1) of prices.");
                }

                writer.WriteVarLong((long)prices[_i5]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id4 = 0;
            ObjectEffect _item4 = null;
            double _val5 = double.NaN;
            itemUID = (int)reader.ReadInt();
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ExchangeBidHouseInListAddedMessage.objectGID.");
            }

            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of ExchangeBidHouseInListAddedMessage.objectType.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _effectsLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id4);
                _item4.Deserialize(reader);
                effects[_i4] = _item4;
            }

            uint _pricesLen = (uint)reader.ReadUShort();
            prices = new long[_pricesLen];
            for (uint _i5 = 0;_i5 < _pricesLen;_i5++)
            {
                _val5 = (double)reader.ReadVarUhLong();
                if (_val5 < 0 || _val5 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val5 + ") on elements of prices.");
                }

                prices[_i5] = (long)_val5;
            }

        }


    }
}








