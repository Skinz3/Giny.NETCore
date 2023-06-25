using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterAlignmentWarEffortProgressionMessage : NetworkMessage  
    { 
        public  const ushort Id = 3065;
        public override ushort MessageId => Id;

        public long alignmentWarEffortDailyLimit;
        public long alignmentWarEffortDailyDonation;
        public long alignmentWarEffortPersonalDonation;

        public CharacterAlignmentWarEffortProgressionMessage()
        {
        }
        public CharacterAlignmentWarEffortProgressionMessage(long alignmentWarEffortDailyLimit,long alignmentWarEffortDailyDonation,long alignmentWarEffortPersonalDonation)
        {
            this.alignmentWarEffortDailyLimit = alignmentWarEffortDailyLimit;
            this.alignmentWarEffortDailyDonation = alignmentWarEffortDailyDonation;
            this.alignmentWarEffortPersonalDonation = alignmentWarEffortPersonalDonation;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (alignmentWarEffortDailyLimit < 0 || alignmentWarEffortDailyLimit > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortDailyLimit + ") on element alignmentWarEffortDailyLimit.");
            }

            writer.WriteVarLong((long)alignmentWarEffortDailyLimit);
            if (alignmentWarEffortDailyDonation < 0 || alignmentWarEffortDailyDonation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortDailyDonation + ") on element alignmentWarEffortDailyDonation.");
            }

            writer.WriteVarLong((long)alignmentWarEffortDailyDonation);
            if (alignmentWarEffortPersonalDonation < 0 || alignmentWarEffortPersonalDonation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortPersonalDonation + ") on element alignmentWarEffortPersonalDonation.");
            }

            writer.WriteVarLong((long)alignmentWarEffortPersonalDonation);
        }
        public override void Deserialize(IDataReader reader)
        {
            alignmentWarEffortDailyLimit = (long)reader.ReadVarUhLong();
            if (alignmentWarEffortDailyLimit < 0 || alignmentWarEffortDailyLimit > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortDailyLimit + ") on element of CharacterAlignmentWarEffortProgressionMessage.alignmentWarEffortDailyLimit.");
            }

            alignmentWarEffortDailyDonation = (long)reader.ReadVarUhLong();
            if (alignmentWarEffortDailyDonation < 0 || alignmentWarEffortDailyDonation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortDailyDonation + ") on element of CharacterAlignmentWarEffortProgressionMessage.alignmentWarEffortDailyDonation.");
            }

            alignmentWarEffortPersonalDonation = (long)reader.ReadVarUhLong();
            if (alignmentWarEffortPersonalDonation < 0 || alignmentWarEffortPersonalDonation > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + alignmentWarEffortPersonalDonation + ") on element of CharacterAlignmentWarEffortProgressionMessage.alignmentWarEffortPersonalDonation.");
            }

        }


    }
}








