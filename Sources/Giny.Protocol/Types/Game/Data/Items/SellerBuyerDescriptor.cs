using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SellerBuyerDescriptor  
    { 
        public const ushort Id = 632;
        public virtual ushort TypeId => Id;

        public int[] quantities;
        public int[] types;
        public float taxPercentage;
        public float taxModificationPercentage;
        public byte maxItemLevel;
        public int maxItemPerAccount;
        public int npcContextualId;
        public short unsoldDelay;

        public SellerBuyerDescriptor()
        {
        }
        public SellerBuyerDescriptor(int[] quantities,int[] types,float taxPercentage,float taxModificationPercentage,byte maxItemLevel,int maxItemPerAccount,int npcContextualId,short unsoldDelay)
        {
            this.quantities = quantities;
            this.types = types;
            this.taxPercentage = taxPercentage;
            this.taxModificationPercentage = taxModificationPercentage;
            this.maxItemLevel = maxItemLevel;
            this.maxItemPerAccount = maxItemPerAccount;
            this.npcContextualId = npcContextualId;
            this.unsoldDelay = unsoldDelay;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)quantities.Length);
            for (uint _i1 = 0;_i1 < quantities.Length;_i1++)
            {
                if (quantities[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + quantities[_i1] + ") on element 1 (starting at 1) of quantities.");
                }

                writer.WriteVarInt((int)quantities[_i1]);
            }

            writer.WriteShort((short)types.Length);
            for (uint _i2 = 0;_i2 < types.Length;_i2++)
            {
                if (types[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + types[_i2] + ") on element 2 (starting at 1) of types.");
                }

                writer.WriteVarInt((int)types[_i2]);
            }

            writer.WriteFloat((float)taxPercentage);
            writer.WriteFloat((float)taxModificationPercentage);
            if (maxItemLevel < 0 || maxItemLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + maxItemLevel + ") on element maxItemLevel.");
            }

            writer.WriteByte((byte)maxItemLevel);
            if (maxItemPerAccount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxItemPerAccount + ") on element maxItemPerAccount.");
            }

            writer.WriteVarInt((int)maxItemPerAccount);
            writer.WriteInt((int)npcContextualId);
            if (unsoldDelay < 0)
            {
                throw new System.Exception("Forbidden value (" + unsoldDelay + ") on element unsoldDelay.");
            }

            writer.WriteVarShort((short)unsoldDelay);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _quantitiesLen = (uint)reader.ReadUShort();
            quantities = new int[_quantitiesLen];
            for (uint _i1 = 0;_i1 < _quantitiesLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhInt();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of quantities.");
                }

                quantities[_i1] = (int)_val1;
            }

            uint _typesLen = (uint)reader.ReadUShort();
            types = new int[_typesLen];
            for (uint _i2 = 0;_i2 < _typesLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of types.");
                }

                types[_i2] = (int)_val2;
            }

            taxPercentage = (float)reader.ReadFloat();
            taxModificationPercentage = (float)reader.ReadFloat();
            maxItemLevel = (byte)reader.ReadSByte();
            if (maxItemLevel < 0 || maxItemLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + maxItemLevel + ") on element of SellerBuyerDescriptor.maxItemLevel.");
            }

            maxItemPerAccount = (int)reader.ReadVarUhInt();
            if (maxItemPerAccount < 0)
            {
                throw new System.Exception("Forbidden value (" + maxItemPerAccount + ") on element of SellerBuyerDescriptor.maxItemPerAccount.");
            }

            npcContextualId = (int)reader.ReadInt();
            unsoldDelay = (short)reader.ReadVarUhShort();
            if (unsoldDelay < 0)
            {
                throw new System.Exception("Forbidden value (" + unsoldDelay + ") on element of SellerBuyerDescriptor.unsoldDelay.");
            }

        }


    }
}








