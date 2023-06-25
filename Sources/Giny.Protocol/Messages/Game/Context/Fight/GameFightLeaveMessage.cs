using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightLeaveMessage : NetworkMessage  
    { 
        public  const ushort Id = 141;
        public override ushort MessageId => Id;

        public double charId;

        public GameFightLeaveMessage()
        {
        }
        public GameFightLeaveMessage(double charId)
        {
            this.charId = charId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (charId < -9.00719925474099E+15 || charId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + charId + ") on element charId.");
            }

            writer.WriteDouble((double)charId);
        }
        public override void Deserialize(IDataReader reader)
        {
            charId = (double)reader.ReadDouble();
            if (charId < -9.00719925474099E+15 || charId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + charId + ") on element of GameFightLeaveMessage.charId.");
            }

        }


    }
}








