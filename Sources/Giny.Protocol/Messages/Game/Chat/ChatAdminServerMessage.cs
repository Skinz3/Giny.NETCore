using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatAdminServerMessage : ChatServerMessage  
    { 
        public new const ushort Id = 6906;
        public override ushort MessageId => Id;


        public ChatAdminServerMessage()
        {
        }
        public ChatAdminServerMessage(byte channel,string content,int timestamp,string fingerprint,double senderId,string senderName,string prefix,int senderAccountId)
        {
            this.channel = channel;
            this.content = content;
            this.timestamp = timestamp;
            this.fingerprint = fingerprint;
            this.senderId = senderId;
            this.senderName = senderName;
            this.prefix = prefix;
            this.senderAccountId = senderAccountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








