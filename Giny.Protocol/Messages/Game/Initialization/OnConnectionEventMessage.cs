using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class OnConnectionEventMessage : NetworkMessage  
    { 
        public  const ushort Id = 2940;
        public override ushort MessageId => Id;

        public byte eventType;

        public OnConnectionEventMessage()
        {
        }
        public OnConnectionEventMessage(byte eventType)
        {
            this.eventType = eventType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)eventType);
        }
        public override void Deserialize(IDataReader reader)
        {
            eventType = (byte)reader.ReadByte();
            if (eventType < 0)
            {
                throw new System.Exception("Forbidden value (" + eventType + ") on element of OnConnectionEventMessage.eventType.");
            }

        }


    }
}








