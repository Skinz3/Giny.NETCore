using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NotificationUpdateFlagMessage : NetworkMessage  
    { 
        public  const ushort Id = 4857;
        public override ushort MessageId => Id;

        public short index;

        public NotificationUpdateFlagMessage()
        {
        }
        public NotificationUpdateFlagMessage(short index)
        {
            this.index = index;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element index.");
            }

            writer.WriteVarShort((short)index);
        }
        public override void Deserialize(IDataReader reader)
        {
            index = (short)reader.ReadVarUhShort();
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element of NotificationUpdateFlagMessage.index.");
            }

        }


    }
}








