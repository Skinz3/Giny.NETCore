using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class BasicStatMessage : NetworkMessage
    {
        public const ushort Id = 1793;
        public override ushort MessageId => Id;

        public double timeSpent;
        public short statId;

        public BasicStatMessage()
        {
        }
        public BasicStatMessage(double timeSpent, short statId)
        {
            this.timeSpent = timeSpent;
            this.statId = statId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (timeSpent < 0 || timeSpent > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + timeSpent + ") on element timeSpent.");
            }

            writer.WriteDouble((double)timeSpent);
            writer.WriteVarShort((short)statId);
        }
        public override void Deserialize(IDataReader reader)
        {
            timeSpent = (double)reader.ReadDouble();
            if (timeSpent < 0 || timeSpent > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + timeSpent + ") on element of BasicStatMessage.timeSpent.");
            }

            statId = (short)reader.ReadVarUhShort();
            if (statId < 0)
            {
                throw new System.Exception("Forbidden value (" + statId + ") on element of BasicStatMessage.statId.");
            }

        }

    }
}


