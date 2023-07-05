using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorAddedMessage : NetworkMessage
    {
        public const ushort Id = 832;
        public override ushort MessageId => Id;

        public long callerId;
        public TaxCollectorInformations description;

        public TaxCollectorAddedMessage()
        {
        }
        public TaxCollectorAddedMessage(long callerId, TaxCollectorInformations description)
        {
            this.callerId = callerId;
            this.description = description;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element callerId.");
            }

            writer.WriteVarLong((long)callerId);
            writer.WriteShort((short)description.TypeId);
            description.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            callerId = (long)reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element of TaxCollectorAddedMessage.callerId.");
            }

            uint _id2 = (uint)reader.ReadUShort();
            description = ProtocolTypeManager.GetInstance<TaxCollectorInformations>((short)_id2);
            description.Deserialize(reader);
        }

    }
}


