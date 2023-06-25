using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildBulletinSetRequestMessage : SocialNoticeSetRequestMessage  
    { 
        public new const ushort Id = 9338;
        public override ushort MessageId => Id;

        public string content;
        public bool notifyMembers;

        public GuildBulletinSetRequestMessage()
        {
        }
        public GuildBulletinSetRequestMessage(string content,bool notifyMembers)
        {
            this.content = content;
            this.notifyMembers = notifyMembers;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)content);
            writer.WriteBoolean((bool)notifyMembers);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            content = (string)reader.ReadUTF();
            notifyMembers = (bool)reader.ReadBoolean();
        }


    }
}








