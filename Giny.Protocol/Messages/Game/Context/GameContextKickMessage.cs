using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextKickMessage : NetworkMessage  
    { 
        public  const ushort Id = 4389;
        public override ushort MessageId => Id;

        public double targetId;

        public GameContextKickMessage()
        {
        }
        public GameContextKickMessage(double targetId)
        {
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameContextKickMessage.targetId.");
            }

        }


    }
}








