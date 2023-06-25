using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkMulticraftCustomerMessage : NetworkMessage  
    { 
        public  const ushort Id = 3366;
        public override ushort MessageId => Id;

        public int skillId;
        public byte crafterJobLevel;

        public ExchangeStartOkMulticraftCustomerMessage()
        {
        }
        public ExchangeStartOkMulticraftCustomerMessage(int skillId,byte crafterJobLevel)
        {
            this.skillId = skillId;
            this.crafterJobLevel = crafterJobLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarInt((int)skillId);
            if (crafterJobLevel < 0 || crafterJobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + crafterJobLevel + ") on element crafterJobLevel.");
            }

            writer.WriteByte((byte)crafterJobLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            skillId = (int)reader.ReadVarUhInt();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of ExchangeStartOkMulticraftCustomerMessage.skillId.");
            }

            crafterJobLevel = (byte)reader.ReadSByte();
            if (crafterJobLevel < 0 || crafterJobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + crafterJobLevel + ") on element of ExchangeStartOkMulticraftCustomerMessage.crafterJobLevel.");
            }

        }


    }
}








