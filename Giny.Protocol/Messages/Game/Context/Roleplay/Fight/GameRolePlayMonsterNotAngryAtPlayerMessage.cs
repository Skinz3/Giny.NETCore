using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayMonsterNotAngryAtPlayerMessage : NetworkMessage  
    { 
        public  const ushort Id = 2418;
        public override ushort MessageId => Id;

        public long playerId;
        public double monsterGroupId;

        public GameRolePlayMonsterNotAngryAtPlayerMessage()
        {
        }
        public GameRolePlayMonsterNotAngryAtPlayerMessage(long playerId,double monsterGroupId)
        {
            this.playerId = playerId;
            this.monsterGroupId = monsterGroupId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            if (monsterGroupId < -9.00719925474099E+15 || monsterGroupId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + monsterGroupId + ") on element monsterGroupId.");
            }

            writer.WriteDouble((double)monsterGroupId);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GameRolePlayMonsterNotAngryAtPlayerMessage.playerId.");
            }

            monsterGroupId = (double)reader.ReadDouble();
            if (monsterGroupId < -9.00719925474099E+15 || monsterGroupId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + monsterGroupId + ") on element of GameRolePlayMonsterNotAngryAtPlayerMessage.monsterGroupId.");
            }

        }


    }
}








