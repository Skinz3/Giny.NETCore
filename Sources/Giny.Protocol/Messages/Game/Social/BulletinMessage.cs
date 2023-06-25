using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BulletinMessage : SocialNoticeMessage  
    { 
        public new const ushort Id = 5239;
        public override ushort MessageId => Id;

        public int lastNotifiedTimestamp;

        public BulletinMessage()
        {
        }
        public BulletinMessage(int lastNotifiedTimestamp,string content,int timestamp,long memberId,string memberName)
        {
            this.lastNotifiedTimestamp = lastNotifiedTimestamp;
            this.content = content;
            this.timestamp = timestamp;
            this.memberId = memberId;
            this.memberName = memberName;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (lastNotifiedTimestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNotifiedTimestamp + ") on element lastNotifiedTimestamp.");
            }

            writer.WriteInt((int)lastNotifiedTimestamp);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            lastNotifiedTimestamp = (int)reader.ReadInt();
            if (lastNotifiedTimestamp < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNotifiedTimestamp + ") on element of BulletinMessage.lastNotifiedTimestamp.");
            }

        }


    }
}








