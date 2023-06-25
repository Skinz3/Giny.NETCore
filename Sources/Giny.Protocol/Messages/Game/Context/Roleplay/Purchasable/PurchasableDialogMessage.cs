using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PurchasableDialogMessage : NetworkMessage  
    { 
        public  const ushort Id = 9506;
        public override ushort MessageId => Id;

        public bool buyOrSell;
        public double purchasableId;
        public int purchasableInstanceId;
        public bool secondHand;
        public long price;

        public PurchasableDialogMessage()
        {
        }
        public PurchasableDialogMessage(bool buyOrSell,double purchasableId,int purchasableInstanceId,bool secondHand,long price)
        {
            this.buyOrSell = buyOrSell;
            this.purchasableId = purchasableId;
            this.purchasableInstanceId = purchasableInstanceId;
            this.secondHand = secondHand;
            this.price = price;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,buyOrSell);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,secondHand);
            writer.WriteByte((byte)_box0);
            if (purchasableId < 0 || purchasableId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + purchasableId + ") on element purchasableId.");
            }

            writer.WriteDouble((double)purchasableId);
            if (purchasableInstanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + purchasableInstanceId + ") on element purchasableInstanceId.");
            }

            writer.WriteInt((int)purchasableInstanceId);
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            buyOrSell = BooleanByteWrapper.GetFlag(_box0,0);
            secondHand = BooleanByteWrapper.GetFlag(_box0,1);
            purchasableId = (double)reader.ReadDouble();
            if (purchasableId < 0 || purchasableId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + purchasableId + ") on element of PurchasableDialogMessage.purchasableId.");
            }

            purchasableInstanceId = (int)reader.ReadInt();
            if (purchasableInstanceId < 0)
            {
                throw new System.Exception("Forbidden value (" + purchasableInstanceId + ") on element of PurchasableDialogMessage.purchasableInstanceId.");
            }

            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of PurchasableDialogMessage.price.");
            }

        }


    }
}








