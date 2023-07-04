using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TeleportHavenBagRequestMessage : NetworkMessage
    {
        public const ushort Id = 3408;
        public override ushort MessageId => Id;

        public long guestId;

        public TeleportHavenBagRequestMessage()
        {
        }
        public TeleportHavenBagRequestMessage(long guestId)
        {
            this.guestId = guestId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element guestId.");
            }

            writer.WriteVarLong((long)guestId);
        }
        public override void Deserialize(IDataReader reader)
        {
            guestId = (long)reader.ReadVarUhLong();
            if (guestId < 0 || guestId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + guestId + ") on element of TeleportHavenBagRequestMessage.guestId.");
            }

        }

    }
}


