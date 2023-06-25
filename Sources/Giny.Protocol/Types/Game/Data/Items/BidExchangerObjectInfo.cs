using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BidExchangerObjectInfo  
    { 
        public const ushort Id = 7649;
        public virtual ushort TypeId => Id;

        public int objectUID;
        public short objectGID;
        public int objectType;
        public ObjectEffect[] effects;
        public long[] prices;

        public BidExchangerObjectInfo()
        {
        }
        public BidExchangerObjectInfo(int objectUID,short objectGID,int objectType,ObjectEffect[] effects,long[] prices)
        {
            this.objectUID = objectUID;
            this.objectGID = objectGID;
            this.objectType = objectType;
            this.effects = effects;
            this.prices = prices;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
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
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id4 = 0;
            ObjectEffect _item4 = null;
            double _val5 = double.NaN;
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of BidExchangerObjectInfo.objectUID.");
            }

            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of BidExchangerObjectInfo.objectGID.");
            }

            objectType = (int)reader.ReadInt();
            if (objectType < 0)
            {
                throw new System.Exception("Forbidden value (" + objectType + ") on element of BidExchangerObjectInfo.objectType.");
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








