using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightJoinRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8671;
        public override ushort MessageId => Id;

        public double fighterId;
        public short fightId;

        public GameFightJoinRequestMessage()
        {
        }
        public GameFightJoinRequestMessage(double fighterId,short fightId)
        {
            this.fighterId = fighterId;
            this.fightId = fightId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fighterId < -9.00719925474099E+15 || fighterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fighterId + ") on element fighterId.");
            }

            writer.WriteDouble((double)fighterId);
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fighterId = (double)reader.ReadDouble();
            if (fighterId < -9.00719925474099E+15 || fighterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fighterId + ") on element of GameFightJoinRequestMessage.fighterId.");
            }

            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameFightJoinRequestMessage.fightId.");
            }

        }


    }
}








