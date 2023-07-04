using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AnomalyOpenedMessage : NetworkMessage
    {
        public const ushort Id = 6379;
        public override ushort MessageId => Id;

        public short subAreaId;

        public AnomalyOpenedMessage()
        {
        }
        public AnomalyOpenedMessage(short subAreaId)
        {
            this.subAreaId = subAreaId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of AnomalyOpenedMessage.subAreaId.");
            }

        }

    }
}


