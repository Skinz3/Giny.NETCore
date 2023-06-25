using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkMulticraftCrafterMessage : NetworkMessage  
    { 
        public  const ushort Id = 7885;
        public override ushort MessageId => Id;

        public int skillId;

        public ExchangeStartOkMulticraftCrafterMessage()
        {
        }
        public ExchangeStartOkMulticraftCrafterMessage(int skillId)
        {
            this.skillId = skillId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarInt((int)skillId);
        }
        public override void Deserialize(IDataReader reader)
        {
            skillId = (int)reader.ReadVarUhInt();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of ExchangeStartOkMulticraftCrafterMessage.skillId.");
            }

        }


    }
}








