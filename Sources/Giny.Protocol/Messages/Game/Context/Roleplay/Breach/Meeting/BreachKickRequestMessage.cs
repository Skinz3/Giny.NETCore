using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class BreachKickRequestMessage : NetworkMessage
    {
        public const ushort Id = 5258;
        public override ushort MessageId => Id;

        public long target;

        public BreachKickRequestMessage()
        {
        }
        public BreachKickRequestMessage(long target)
        {
            this.target = target;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (target < 0 || target > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element target.");
            }

            writer.WriteVarLong((long)target);
        }
        public override void Deserialize(IDataReader reader)
        {
            target = (long)reader.ReadVarUhLong();
            if (target < 0 || target > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element of BreachKickRequestMessage.target.");
            }

        }

    }
}


