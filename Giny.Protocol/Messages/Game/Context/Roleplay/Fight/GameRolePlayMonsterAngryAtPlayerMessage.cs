using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayMonsterAngryAtPlayerMessage : NetworkMessage  
    { 
        public  const ushort Id = 8302;
        public override ushort MessageId => Id;

        public long playerId;
        public double monsterGroupId;
        public double angryStartTime;
        public double attackTime;

        public GameRolePlayMonsterAngryAtPlayerMessage()
        {
        }
        public GameRolePlayMonsterAngryAtPlayerMessage(long playerId,double monsterGroupId,double angryStartTime,double attackTime)
        {
            this.playerId = playerId;
            this.monsterGroupId = monsterGroupId;
            this.angryStartTime = angryStartTime;
            this.attackTime = attackTime;
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
            if (angryStartTime < 0 || angryStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + angryStartTime + ") on element angryStartTime.");
            }

            writer.WriteDouble((double)angryStartTime);
            if (attackTime < 0 || attackTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + attackTime + ") on element attackTime.");
            }

            writer.WriteDouble((double)attackTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GameRolePlayMonsterAngryAtPlayerMessage.playerId.");
            }

            monsterGroupId = (double)reader.ReadDouble();
            if (monsterGroupId < -9.00719925474099E+15 || monsterGroupId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + monsterGroupId + ") on element of GameRolePlayMonsterAngryAtPlayerMessage.monsterGroupId.");
            }

            angryStartTime = (double)reader.ReadDouble();
            if (angryStartTime < 0 || angryStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + angryStartTime + ") on element of GameRolePlayMonsterAngryAtPlayerMessage.angryStartTime.");
            }

            attackTime = (double)reader.ReadDouble();
            if (attackTime < 0 || attackTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + attackTime + ") on element of GameRolePlayMonsterAngryAtPlayerMessage.attackTime.");
            }

        }


    }
}








