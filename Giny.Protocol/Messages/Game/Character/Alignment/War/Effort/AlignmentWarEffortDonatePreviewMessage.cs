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
        public  const ushort Id = 9897;
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
            if (xp < -9.00719925474099E+15 || xp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + xp + ") on element xp.");
            }

            writer.WriteDouble((double)xp);
        }
        public override void Deserialize(IDataReader reader)
        {
            xp = (double)reader.ReadDouble();
            if (xp < -9.00719925474099E+15 || xp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + xp + ") on element of AlignmentWarEffortDonatePreviewMessage.xp.");
            }

        }


    }
}








