using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HouseBuyResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 5020;
        public override ushort MessageId => Id;

        public int houseId;
        public int instanceId;
        public bool secondHand;
        public bool bought;
        public long realPrice;

        public HouseBuyResultMessage()
        {
        }
        public HouseBuyResultMessage(int houseId,int instanceId,bool secondHand,bool bought,long realPrice)
        {
            this.houseId = houseId;
            this.instanceId = instanceId;
            this.secondHand = secondHand;
            this.bought = bought;
            this.realPrice = realPrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,secondHand);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,bought);
            writer.WriteByte((byte)_box0);
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element instanceId.");
            }

            writer.WriteInt((int)instanceId);
            if (realPrice < 0 || realPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + realPrice + ") on element realPrice.");
            }

            writer.WriteVarLong((long)realPrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            secondHand = BooleanByteWrapper.GetFlag(_box0,0);
            bought = BooleanByteWrapper.GetFlag(_box0,1);
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HouseBuyResultMessage.houseId.");
            }

            instanceId = (int)reader.ReadInt();
            if (instanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + instanceId + ") on element of HouseBuyResultMessage.instanceId.");
            }

            realPrice = (long)reader.ReadVarUhLong();
            if (realPrice < 0 || realPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + realPrice + ") on element of HouseBuyResultMessage.realPrice.");
            }

        }


    }
}








