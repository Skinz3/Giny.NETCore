using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameMapSpeedMovementMessage : NetworkMessage  
    { 
        public  const ushort Id = 7714;
        public override ushort MessageId => Id;

        public int speedMultiplier;

        public GameMapSpeedMovementMessage()
        {
        }
        public GameMapSpeedMovementMessage(int speedMultiplier)
        {
            this.speedMultiplier = speedMultiplier;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)speedMultiplier);
        }
        public override void Deserialize(IDataReader reader)
        {
            speedMultiplier = (int)reader.ReadInt();
        }


    }
}








