using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatServerMessage : ChatAbstractServerMessage  
    { 
        public new const ushort Id = 373;
        public override ushort MessageId => Id;

        public double senderId;
        public string senderName;
        public string prefix;
        public int senderAccountId;

        public ChatServerMessage()
        {
        }
        public ChatServerMessage(double senderId,string senderName,string prefix,int senderAccountId,byte channel,string content,int timestamp,string fingerprint)
        {
            this.senderId = senderId;
            this.senderName = senderName;
            this.prefix = prefix;
            this.senderAccountId = senderAccountId;
            this.channel = channel;
            this.content = content;
            this.timestamp = timestamp;
            this.fingerprint = fingerprint;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (senderId < -9.00719925474099E+15 || senderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + senderId + ") on element senderId.");
            }

            writer.WriteDouble((double)senderId);
            writer.WriteUTF((string)senderName);
            writer.WriteUTF((string)prefix);
            if (senderAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + senderAccountId + ") on element senderAccountId.");
            }

            writer.WriteInt((int)senderAccountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            senderId = (double)reader.ReadDouble();
            if (senderId < -9.00719925474099E+15 || senderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + senderId + ") on element of ChatServerMessage.senderId.");
            }

            senderName = (string)reader.ReadUTF();
            prefix = (string)reader.ReadUTF();
            senderAccountId = (int)reader.ReadInt();
            if (senderAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + senderAccountId + ") on element of ChatServerMessage.senderAccountId.");
            }

        }


    }
}








