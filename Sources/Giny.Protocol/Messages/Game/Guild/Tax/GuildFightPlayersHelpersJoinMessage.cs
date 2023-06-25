using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFightPlayersHelpersJoinMessage : NetworkMessage  
    { 
        public  const ushort Id = 8503;
        public override ushort MessageId => Id;

        public double fightId;
        public CharacterMinimalPlusLookInformations playerInfo;

        public GuildFightPlayersHelpersJoinMessage()
        {
        }
        public GuildFightPlayersHelpersJoinMessage(double fightId,CharacterMinimalPlusLookInformations playerInfo)
        {
            this.fightId = fightId;
            this.playerInfo = playerInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0 || fightId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteDouble((double)fightId);
            playerInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (double)reader.ReadDouble();
            if (fightId < 0 || fightId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GuildFightPlayersHelpersJoinMessage.fightId.");
            }

            playerInfo = new CharacterMinimalPlusLookInformations();
            playerInfo.Deserialize(reader);
        }


    }
}








