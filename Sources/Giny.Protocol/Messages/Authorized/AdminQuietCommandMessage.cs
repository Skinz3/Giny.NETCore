using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AdminQuietCommandMessage : AdminCommandMessage
    {
        public new const ushort Id = 3781;
        public override ushort MessageId => Id;


        public AdminQuietCommandMessage()
        {
        }
        public AdminQuietCommandMessage(Uuid messageUuid, string content)
        {
            this.messageUuid = messageUuid;
            this.content = content;
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


