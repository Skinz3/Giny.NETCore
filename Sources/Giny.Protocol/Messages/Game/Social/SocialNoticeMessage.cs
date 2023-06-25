using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SocialNoticeMessage : NetworkMessage  
    { 
        public  const ushort Id = 7363;
        public override ushort MessageId => Id;

        public string content;
        public int timestamp;
        public long memberId;
        public string memberName;

        public SocialNoticeMessage()
        {
        }
        public SocialNoticeMessage(string content,int timestamp,long memberId,string memberName)
        {
            this.content = content;
            this.timestamp = timestamp;
            this.memberId = memberId;
            this.memberName = memberName;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)content);
            if (timestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element timestamp.");
            }

            writer.WriteInt((int)timestamp);
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteUTF((string)memberName);
        }
        public override void Deserialize(IDataReader reader)
        {
            content = (string)reader.ReadUTF();
            timestamp = (int)reader.ReadInt();
            if (timestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element of SocialNoticeMessage.timestamp.");
            }

            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of SocialNoticeMessage.memberId.");
            }

            memberName = (string)reader.ReadUTF();
        }


    }
}








