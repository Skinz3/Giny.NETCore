using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SequenceNumberMessage : NetworkMessage  
    { 
        public  const ushort Id = 116;
        public override ushort MessageId => Id;

        public short number;

        public SequenceNumberMessage()
        {
        }
        public SequenceNumberMessage(short number)
        {
            this.number = number;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (number < 0 || number > 65535)
            {
                throw new System.Exception("Forbidden value (" + number + ") on element number.");
            }

            writer.WriteShort((short)number);
        }
        public override void Deserialize(IDataReader reader)
        {
            number = (short)reader.ReadUShort();
            if (number < 0 || number > 65535)
            {
                throw new System.Exception("Forbidden value (" + number + ") on element of SequenceNumberMessage.number.");
            }

        }


    }
}








