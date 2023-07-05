using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AdminCommandMessage : NetworkMessage
    {
        public const ushort Id = 3305;
        public override ushort MessageId => Id;

        public Uuid messageUuid;
        public string content;

        public AdminCommandMessage()
        {
        }
        public AdminCommandMessage(Uuid messageUuid, string content)
        {
            this.messageUuid = messageUuid;
            this.content = content;
        }
        public override void Serialize(IDataWriter writer)
        {
            messageUuid.Serialize(writer);
            writer.WriteUTF((string)content);
        }
        public override void Deserialize(IDataReader reader)
        {
            messageUuid = new Uuid();
            messageUuid.Deserialize(reader);
            content = (string)reader.ReadUTF();
        }

    }
}


