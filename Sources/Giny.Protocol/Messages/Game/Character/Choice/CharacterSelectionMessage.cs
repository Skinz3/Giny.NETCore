using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CharacterSelectionMessage : NetworkMessage
    {
        public const ushort Id = 3884;
        public override ushort MessageId => Id;

        public long id;

        public CharacterSelectionMessage()
        {
        }
        public CharacterSelectionMessage(long id)
        {
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of CharacterSelectionMessage.id.");
            }

        }

    }
}


