using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportToBuddyAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 582;
        public override ushort MessageId => Id;

        public short dungeonId;
        public long buddyId;
        public bool accept;

        public TeleportToBuddyAnswerMessage()
        {
        }
        public TeleportToBuddyAnswerMessage(short dungeonId,long buddyId,bool accept)
        {
            this.dungeonId = dungeonId;
            this.buddyId = buddyId;
            this.accept = accept;
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
            writer.WriteBoolean((bool)accept);
        }
        public override void Deserialize(IDataReader reader)
        {
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of TeleportToBuddyAnswerMessage.dungeonId.");
            }

            buddyId = (long)reader.ReadVarUhLong();
            if (buddyId < 0 || buddyId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + buddyId + ") on element of TeleportToBuddyAnswerMessage.buddyId.");
            }

            accept = (bool)reader.ReadBoolean();
        }


    }
}








