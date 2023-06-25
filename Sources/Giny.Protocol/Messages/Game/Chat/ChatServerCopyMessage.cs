using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatServerCopyMessage : ChatAbstractServerMessage  
    { 
        public new const ushort Id = 5300;
        public override ushort MessageId => Id;

        public long receiverId;
        public string receiverName;

        public ChatServerCopyMessage()
        {
        }
        public ChatServerCopyMessage(long receiverId,string receiverName,byte channel,string content,int timestamp,string fingerprint)
        {
            this.receiverId = receiverId;
            this.receiverName = receiverName;
            this.channel = channel;
            this.content = content;
            this.timestamp = timestamp;
            this.fingerprint = fingerprint;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (receiverId < 0 || receiverId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + receiverId + ") on element receiverId.");
            }

            writer.WriteVarLong((long)receiverId);
            writer.WriteUTF((string)receiverName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            receiverId = (long)reader.ReadVarUhLong();
            if (receiverId < 0 || receiverId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + receiverId + ") on element of ChatServerCopyMessage.receiverId.");
            }

            receiverName = (string)reader.ReadUTF();
        }


    }
}








