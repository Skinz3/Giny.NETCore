using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightUpdateTeamMessage : NetworkMessage  
    { 
        public  const ushort Id = 985;
        public override ushort MessageId => Id;

        public short fightId;
        public FightTeamInformations team;

        public GameFightUpdateTeamMessage()
        {
        }
        public GameFightUpdateTeamMessage(short fightId,FightTeamInformations team)
        {
            this.fightId = fightId;
            this.team = team;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            team.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameFightUpdateTeamMessage.fightId.");
            }

            team = new FightTeamInformations();
            team.Deserialize(reader);
        }


    }
}








