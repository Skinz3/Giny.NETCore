using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportToBuddyOfferMessage : NetworkMessage  
    { 
        public  const ushort Id = 9960;
        public override ushort MessageId => Id;

        public short dungeonId;
        public long buddyId;
        public int timeLeft;

        public TeleportToBuddyOfferMessage()
        {
        }
        public TeleportToBuddyOfferMessage(short dungeonId,long buddyId,int timeLeft)
        {
            this.dungeonId = dungeonId;
            this.buddyId = buddyId;
            this.timeLeft = timeLeft;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            if (buddyId < 0 || buddyId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + buddyId + ") on element buddyId.");
            }

            writer.WriteVarLong((long)buddyId);
            if (timeLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeft + ") on element timeLeft.");
            }

            writer.WriteVarInt((int)timeLeft);
        }
        public override void Deserialize(IDataReader reader)
        {
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of TeleportToBuddyOfferMessage.dungeonId.");
            }

            buddyId = (long)reader.ReadVarUhLong();
            if (buddyId < 0 || buddyId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + buddyId + ") on element of TeleportToBuddyOfferMessage.buddyId.");
            }

            timeLeft = (int)reader.ReadVarUhInt();
            if (timeLeft < 0)
            {
                throw new System.Exception("Forbidden value (" + timeLeft + ") on element of TeleportToBuddyOfferMessage.timeLeft.");
            }

        }


    }
}








