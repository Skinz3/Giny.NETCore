using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPauseMessage : NetworkMessage  
    { 
        public  const ushort Id = 9521;
        public override ushort MessageId => Id;

        public bool isPaused;

        public GameFightPauseMessage()
        {
        }
        public GameFightPauseMessage(bool isPaused)
        {
            this.isPaused = isPaused;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)isPaused);
        }
        public override void Deserialize(IDataReader reader)
        {
            isPaused = (bool)reader.ReadBoolean();
        }


    }
}








