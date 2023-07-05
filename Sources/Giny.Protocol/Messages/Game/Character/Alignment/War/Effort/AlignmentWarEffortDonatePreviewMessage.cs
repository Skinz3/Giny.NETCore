using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlignmentWarEffortDonatePreviewMessage : NetworkMessage
    {
        public const ushort Id = 8313;
        public override ushort MessageId => Id;

        public double xp;

        public AlignmentWarEffortDonatePreviewMessage()
        {
        }
        public AlignmentWarEffortDonatePreviewMessage(double xp)
        {
            this.xp = xp;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (xp < -9007199254740992 || xp > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + xp + ") on element xp.");
            }

            writer.WriteDouble((double)xp);
        }
        public override void Deserialize(IDataReader reader)
        {
            xp = (double)reader.ReadDouble();
            if (xp < -9007199254740992 || xp > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + xp + ") on element of AlignmentWarEffortDonatePreviewMessage.xp.");
            }

        }

    }
}


