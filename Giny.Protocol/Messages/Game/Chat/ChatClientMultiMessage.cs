using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatClientMultiMessage : ChatAbstractClientMessage  
    { 
        public new const ushort Id = 9499;
        public override ushort MessageId => Id;

        public byte channel;

        public ChatClientMultiMessage()
        {
        }
        public ChatClientMultiMessage(byte channel,string content)
        {
            this.channel = channel;
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)channel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            channel = (byte)reader.ReadByte();
            if (channel < 0)
            {
                throw new System.Exception("Forbidden value (" + channel + ") on element of ChatClientMultiMessage.channel.");
            }

        }


    }
}








