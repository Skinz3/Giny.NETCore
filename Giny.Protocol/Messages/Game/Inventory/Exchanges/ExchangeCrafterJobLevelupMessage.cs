using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeCrafterJobLevelupMessage : NetworkMessage  
    { 
        public  const ushort Id = 7102;
        public override ushort MessageId => Id;

        public byte crafterJobLevel;

        public ExchangeCrafterJobLevelupMessage()
        {
        }
        public ExchangeCrafterJobLevelupMessage(byte crafterJobLevel)
        {
            this.crafterJobLevel = crafterJobLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (crafterJobLevel < 0 || crafterJobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + crafterJobLevel + ") on element crafterJobLevel.");
            }

            writer.WriteByte((byte)crafterJobLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            crafterJobLevel = (byte)reader.ReadSByte();
            if (crafterJobLevel < 0 || crafterJobLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + crafterJobLevel + ") on element of ExchangeCrafterJobLevelupMessage.crafterJobLevel.");
            }

        }


    }
}








