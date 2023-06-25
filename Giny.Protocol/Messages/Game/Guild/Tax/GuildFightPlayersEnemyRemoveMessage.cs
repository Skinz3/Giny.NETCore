using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildFightPlayersEnemyRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 9870;
        public override ushort MessageId => Id;

        public double fightId;
        public long playerId;

        public GuildFightPlayersEnemyRemoveMessage()
        {
        }
        public GuildFightPlayersEnemyRemoveMessage(double fightId,long playerId)
        {
            this.fightId = fightId;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0 || fightId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteDouble((double)fightId);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (double)reader.ReadDouble();
            if (fightId < 0 || fightId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GuildFightPlayersEnemyRemoveMessage.fightId.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildFightPlayersEnemyRemoveMessage.playerId.");
            }

        }


    }
}








