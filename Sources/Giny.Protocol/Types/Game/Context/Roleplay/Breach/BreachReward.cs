using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BreachReward  
    { 
        public const ushort Id = 1476;
        public virtual ushort TypeId => Id;

        public int id;
        public byte[] buyLocks;
        public string buyCriterion;
        public int remainingQty;
        public int price;

        public BreachReward()
        {
        }
        public BreachReward(int id,byte[] buyLocks,string buyCriterion,int remainingQty,int price)
        {
            this.id = id;
            this.buyLocks = buyLocks;
            this.buyCriterion = buyCriterion;
            this.remainingQty = remainingQty;
            this.price = price;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarInt((int)id);
            writer.WriteShort((short)buyLocks.Length);
            for (uint _i2 = 0;_i2 < buyLocks.Length;_i2++)
            {
                writer.WriteByte((byte)buyLocks[_i2]);
            }

            writer.WriteUTF((string)buyCriterion);
            writer.WriteVarInt((int)remainingQty);
            if (price < 0)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarInt((int)price);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            id = (int)reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of BreachReward.id.");
            }

            uint _buyLocksLen = (uint)reader.ReadUShort();
            buyLocks = new byte[_buyLocksLen];
            for (uint _i2 = 0;_i2 < _buyLocksLen;_i2++)
            {
                _val2 = (uint)reader.ReadByte();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of buyLocks.");
                }

                buyLocks[_i2] = (byte)_val2;
            }

            buyCriterion = (string)reader.ReadUTF();
            remainingQty = (int)reader.ReadVarInt();
            price = (int)reader.ReadVarUhInt();
            if (price < 0)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of BreachReward.price.");
            }

        }


    }
}








