using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LoginQueueStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 2055;
        public override ushort MessageId => Id;

        public short position;
        public short total;

        public LoginQueueStatusMessage()
        {
        }
        public LoginQueueStatusMessage(short position,short total)
        {
            this.position = position;
            this.total = total;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (position < 0 || position > 65535)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element position.");
            }

            writer.WriteShort((short)position);
            if (total < 0 || total > 65535)
            {
                throw new System.Exception("Forbidden value (" + total + ") on element total.");
            }

            writer.WriteShort((short)total);
        }
        public override void Deserialize(IDataReader reader)
        {
            position = (short)reader.ReadUShort();
            if (position < 0 || position > 65535)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element of LoginQueueStatusMessage.position.");
            }

            total = (short)reader.ReadUShort();
            if (total < 0 || total > 65535)
            {
                throw new System.Exception("Forbidden value (" + total + ") on element of LoginQueueStatusMessage.total.");
            }

        }


    }
}








