using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HouseInstanceInformations  
    { 
        public const ushort Id = 1868;
        public virtual ushort TypeId => Id;

        public int instanceId;
        public bool secondHand;
        public bool isLocked;
        public AccountTagInformation ownerTag;
        public bool hasOwner;
        public long price;
        public bool isSaleLocked;

        public HouseInstanceInformations()
        {
        }
        public HouseInstanceInformations(int instanceId,bool secondHand,bool isLocked,AccountTagInformation ownerTag,bool hasOwner,long price,bool isSaleLocked)
        {
            this.instanceId = instanceId;
            this.secondHand = secondHand;
            this.isLocked = isLocked;
            this.ownerTag = ownerTag;
            this.hasOwner = hasOwner;
            this.price = price;
            this.isSaleLocked = isSaleLocked;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,secondHand);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isLocked);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,hasOwner);
            _box0 = BooleanByteWrapper.SetFlag(_box0,3,isSaleLocked);
            writer.WriteByte((byte)_box0);
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }

            writer.WriteInt((int)instanceId);
            ownerTag.Serialize(writer);
            if (price < -9.00719925474099E+15 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            secondHand = BooleanByteWrapper.GetFlag(_box0,0);
            isLocked = BooleanByteWrapper.GetFlag(_box0,1);
            hasOwner = BooleanByteWrapper.GetFlag(_box0,2);
            isSaleLocked = BooleanByteWrapper.GetFlag(_box0,3);
            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseInstanceInformations.instanceId.");
            }

            ownerTag = new AccountTagInformation();
            ownerTag.Deserialize(reader);
            price = (long)reader.ReadVarLong();
            if (price < -9.00719925474099E+15 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of HouseInstanceInformations.price.");
            }

        }


    }
}








