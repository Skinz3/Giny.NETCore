using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonPartyFinderAvailableDungeonsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8110;
        public override ushort MessageId => Id;


        public DungeonPartyFinderAvailableDungeonsRequestMessage()
        {
        }
        public override void Serialize(IDataWriter writer)
        {
        }
        public override void Deserialize(IDataReader reader)
        {
        }


    }
}








