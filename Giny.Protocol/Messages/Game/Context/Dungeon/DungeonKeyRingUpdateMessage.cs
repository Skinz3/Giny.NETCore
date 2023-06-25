using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonKeyRingUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 6572;
        public override ushort MessageId => Id;

        public short dungeonId;
        public bool available;

        public DungeonKeyRingUpdateMessage()
        {
        }
        public DungeonKeyRingUpdateMessage(short dungeonId,bool available)
        {
            this.dungeonId = dungeonId;
            this.available = available;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            writer.WriteBoolean((bool)available);
        }
        public override void Deserialize(IDataReader reader)
        {
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of DungeonKeyRingUpdateMessage.dungeonId.");
            }

            available = (bool)reader.ReadBoolean();
        }


    }
}








