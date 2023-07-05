using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlignmentWarEffortDonateRequestMessage : NetworkMessage
    {
        public const ushort Id = 1453;
        public override ushort MessageId => Id;

        public long donation;

        public AlignmentWarEffortDonateRequestMessage()
        {
        }
        public AlignmentWarEffortDonateRequestMessage(long donation)
        {
            this.donation = donation;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (donation < 0 || donation > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + donation + ") on element donation.");
            }

            writer.WriteVarLong((long)donation);
        }
        public override void Deserialize(IDataReader reader)
        {
            donation = (long)reader.ReadVarUhLong();
            if (donation < 0 || donation > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + donation + ") on element of AlignmentWarEffortDonateRequestMessage.donation.");
            }

        }

    }
}


