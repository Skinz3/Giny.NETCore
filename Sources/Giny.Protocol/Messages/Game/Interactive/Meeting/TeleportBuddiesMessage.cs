using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportBuddiesMessage : NetworkMessage  
    { 
        public  const ushort Id = 7478;
        public override ushort MessageId => Id;

        public short dungeonId;

        public TeleportBuddiesMessage()
        {
        }
        public TeleportBuddiesMessage(short dungeonId)
        {
            this.dungeonId = dungeonId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
        }
        public override void Deserialize(IDataReader reader)
        {
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of TeleportBuddiesMessage.dungeonId.");
            }

        }


    }
}








