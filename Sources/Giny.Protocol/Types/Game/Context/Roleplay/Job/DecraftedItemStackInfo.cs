using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class DecraftedItemStackInfo  
    { 
        public const ushort Id = 1111;
        public virtual ushort TypeId => Id;

        public int objectUID;
        public float bonusMin;
        public float bonusMax;
        public short[] runesId;
        public int[] runesQty;

        public DecraftedItemStackInfo()
        {
        }
        public DecraftedItemStackInfo(int objectUID,float bonusMin,float bonusMax,short[] runesId,int[] runesQty)
        {
            this.objectUID = objectUID;
            this.bonusMin = bonusMin;
            this.bonusMax = bonusMax;
            this.runesId = runesId;
            this.runesQty = runesQty;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            writer.WriteFloat((float)bonusMin);
            writer.WriteFloat((float)bonusMax);
            writer.WriteShort((short)runesId.Length);
            for (uint _i4 = 0;_i4 < runesId.Length;_i4++)
            {
                if (runesId[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + runesId[_i4] + ") on element 4 (starting at 1) of runesId.");
                }

                writer.WriteVarShort((short)runesId[_i4]);
            }

            writer.WriteShort((short)runesQty.Length);
            for (uint _i5 = 0;_i5 < runesQty.Length;_i5++)
            {
                if (runesQty[_i5] < 0)
                {
                    throw new System.Exception("Forbidden value (" + runesQty[_i5] + ") on element 5 (starting at 1) of runesQty.");
                }

                writer.WriteVarInt((int)runesQty[_i5]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val4 = 0;
            uint _val5 = 0;
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of DecraftedItemStackInfo.objectUID.");
            }

            bonusMin = (float)reader.ReadFloat();
            bonusMax = (float)reader.ReadFloat();
            uint _runesIdLen = (uint)reader.ReadUShort();
            runesId = new short[_runesIdLen];
            for (uint _i4 = 0;_i4 < _runesIdLen;_i4++)
            {
                _val4 = (uint)reader.ReadVarUhShort();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of runesId.");
                }

                runesId[_i4] = (short)_val4;
            }

            uint _runesQtyLen = (uint)reader.ReadUShort();
            runesQty = new int[_runesQtyLen];
            for (uint _i5 = 0;_i5 < _runesQtyLen;_i5++)
            {
                _val5 = (uint)reader.ReadVarUhInt();
                if (_val5 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val5 + ") on elements of runesQty.");
                }

                runesQty[_i5] = (int)_val5;
            }

        }


    }
}








