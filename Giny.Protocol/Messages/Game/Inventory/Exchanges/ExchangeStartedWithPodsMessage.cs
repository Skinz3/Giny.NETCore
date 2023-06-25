using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartedWithPodsMessage : ExchangeStartedMessage  
    { 
        public new const ushort Id = 5367;
        public override ushort MessageId => Id;

        public double firstCharacterId;
        public int firstCharacterCurrentWeight;
        public int firstCharacterMaxWeight;
        public double secondCharacterId;
        public int secondCharacterCurrentWeight;
        public int secondCharacterMaxWeight;

        public ExchangeStartedWithPodsMessage()
        {
        }
        public ExchangeStartedWithPodsMessage(double firstCharacterId,int firstCharacterCurrentWeight,int firstCharacterMaxWeight,double secondCharacterId,int secondCharacterCurrentWeight,int secondCharacterMaxWeight,byte exchangeType)
        {
            this.firstCharacterId = firstCharacterId;
            this.firstCharacterCurrentWeight = firstCharacterCurrentWeight;
            this.firstCharacterMaxWeight = firstCharacterMaxWeight;
            this.secondCharacterId = secondCharacterId;
            this.secondCharacterCurrentWeight = secondCharacterCurrentWeight;
            this.secondCharacterMaxWeight = secondCharacterMaxWeight;
            this.exchangeType = exchangeType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (firstCharacterId < -9.00719925474099E+15 || firstCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterId + ") on element firstCharacterId.");
            }

            writer.WriteDouble((double)firstCharacterId);
            if (firstCharacterCurrentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterCurrentWeight + ") on element firstCharacterCurrentWeight.");
            }

            writer.WriteVarInt((int)firstCharacterCurrentWeight);
            if (firstCharacterMaxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterMaxWeight + ") on element firstCharacterMaxWeight.");
            }

            writer.WriteVarInt((int)firstCharacterMaxWeight);
            if (secondCharacterId < -9.00719925474099E+15 || secondCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterId + ") on element secondCharacterId.");
            }

            writer.WriteDouble((double)secondCharacterId);
            if (secondCharacterCurrentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterCurrentWeight + ") on element secondCharacterCurrentWeight.");
            }

            writer.WriteVarInt((int)secondCharacterCurrentWeight);
            if (secondCharacterMaxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterMaxWeight + ") on element secondCharacterMaxWeight.");
            }

            writer.WriteVarInt((int)secondCharacterMaxWeight);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            firstCharacterId = (double)reader.ReadDouble();
            if (firstCharacterId < -9.00719925474099E+15 || firstCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterId + ") on element of ExchangeStartedWithPodsMessage.firstCharacterId.");
            }

            firstCharacterCurrentWeight = (int)reader.ReadVarUhInt();
            if (firstCharacterCurrentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterCurrentWeight + ") on element of ExchangeStartedWithPodsMessage.firstCharacterCurrentWeight.");
            }

            firstCharacterMaxWeight = (int)reader.ReadVarUhInt();
            if (firstCharacterMaxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + firstCharacterMaxWeight + ") on element of ExchangeStartedWithPodsMessage.firstCharacterMaxWeight.");
            }

            secondCharacterId = (double)reader.ReadDouble();
            if (secondCharacterId < -9.00719925474099E+15 || secondCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterId + ") on element of ExchangeStartedWithPodsMessage.secondCharacterId.");
            }

            secondCharacterCurrentWeight = (int)reader.ReadVarUhInt();
            if (secondCharacterCurrentWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterCurrentWeight + ") on element of ExchangeStartedWithPodsMessage.secondCharacterCurrentWeight.");
            }

            secondCharacterMaxWeight = (int)reader.ReadVarUhInt();
            if (secondCharacterMaxWeight < 0)
            {
                throw new System.Exception("Forbidden value (" + secondCharacterMaxWeight + ") on element of ExchangeStartedWithPodsMessage.secondCharacterMaxWeight.");
            }

        }


    }
}








