using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationDeletedMessage : NetworkMessage
    {
        public const ushort Id = 819;
        public override ushort MessageId => Id;

        public bool deleted;

        public AllianceApplicationDeletedMessage()
        {
        }
        public AllianceApplicationDeletedMessage(bool deleted)
        {
            this.deleted = deleted;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)deleted);
        }
        public override void Deserialize(IDataReader reader)
        {
            deleted = (bool)reader.ReadBoolean();
        }

    }
}


