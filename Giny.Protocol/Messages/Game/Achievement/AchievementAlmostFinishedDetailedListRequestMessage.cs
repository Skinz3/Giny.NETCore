using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementAlmostFinishedDetailedListRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7726;
        public override ushort MessageId => Id;


        public AchievementAlmostFinishedDetailedListRequestMessage()
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








