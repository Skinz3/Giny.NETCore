using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceKickRequestMessage : NetworkMessage
    {
        public const ushort Id = 3588;
        public override ushort MessageId => Id;

        public long kickedId;

        public AllianceKickRequestMessage()
        {
        }
        public AllianceKickRequestMessage(long kickedId)
        {
            this.kickedId = kickedId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (kickedId < -9007199254740992 || kickedId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element kickedId.");
            }

            writer.WriteVarLong((long)kickedId);
        }
        public override void Deserialize(IDataReader reader)
        {
            kickedId = (long)reader.ReadVarLong();
            if (kickedId < -9007199254740992 || kickedId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + kickedId + ") on element of AllianceKickRequestMessage.kickedId.");
            }

        }

    }
}


