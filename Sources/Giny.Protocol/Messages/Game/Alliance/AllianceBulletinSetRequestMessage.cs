using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceBulletinSetRequestMessage : SocialNoticeSetRequestMessage
    {
        public new const ushort Id = 7394;
        public override ushort MessageId => Id;

        public string content;

        public AllianceBulletinSetRequestMessage()
        {
        }
        public AllianceBulletinSetRequestMessage(string content)
        {
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)content);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            content = (string)reader.ReadUTF();
        }

    }
}


