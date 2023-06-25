using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonPartyFinderListenErrorMessage : NetworkMessage  
    { 
        public  const ushort Id = 336;
        public override ushort MessageId => Id;

        public short dungeonId;

        public DungeonPartyFinderListenErrorMessage()
        {
        }
        public DungeonPartyFinderListenErrorMessage(short dungeonId)
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
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of DungeonPartyFinderListenSystem.ExceptionMessage.dungeonId.");
            }

        }


    }
}








