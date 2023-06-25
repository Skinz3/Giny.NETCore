using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CurrentServerStatusUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 38;
        public override ushort MessageId => Id;

        public byte status;

        public CurrentServerStatusUpdateMessage()
        {
        }
        public CurrentServerStatusUpdateMessage(byte status)
        {
            this.status = status;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)status);
        }
        public override void Deserialize(IDataReader reader)
        {
            status = (byte)reader.ReadByte();
            if (status < 0)
            {
                throw new System.Exception("Forbidden value (" + status + ") on element of CurrentServerStatusUpdateMessage.status.");
            }

        }


    }
}








