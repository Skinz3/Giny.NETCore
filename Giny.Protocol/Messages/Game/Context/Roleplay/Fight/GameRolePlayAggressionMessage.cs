using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayAggressionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1626;
        public override ushort MessageId => Id;

        public long attackerId;
        public long defenderId;

        public GameRolePlayAggressionMessage()
        {
        }
        public GameRolePlayAggressionMessage(long attackerId,long defenderId)
        {
            this.attackerId = attackerId;
            this.defenderId = defenderId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (attackerId < 0 || attackerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + attackerId + ") on element attackerId.");
            }

            writer.WriteVarLong((long)attackerId);
            if (defenderId < 0 || defenderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + defenderId + ") on element defenderId.");
            }

            writer.WriteVarLong((long)defenderId);
        }
        public override void Deserialize(IDataReader reader)
        {
            attackerId = (long)reader.ReadVarUhLong();
            if (attackerId < 0 || attackerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + attackerId + ") on element of GameRolePlayAggressionMessage.attackerId.");
            }

            defenderId = (long)reader.ReadVarUhLong();
            if (defenderId < 0 || defenderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + defenderId + ") on element of GameRolePlayAggressionMessage.defenderId.");
            }

        }


    }
}








