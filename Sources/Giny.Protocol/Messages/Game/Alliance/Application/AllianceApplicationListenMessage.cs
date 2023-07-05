using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationListenMessage : NetworkMessage
    {
        public const ushort Id = 121;
        public override ushort MessageId => Id;

        public bool listen;

        public AllianceApplicationListenMessage()
        {
        }
        public AllianceApplicationListenMessage(bool listen)
        {
            this.listen = listen;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)listen);
        }
        public override void Deserialize(IDataReader reader)
        {
            listen = (bool)reader.ReadBoolean();
        }

    }
}


