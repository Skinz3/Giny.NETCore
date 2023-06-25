using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaFighterStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 9616;
        public override ushort MessageId => Id;

        public short fightId;
        public long playerId;
        public bool accepted;

        public GameRolePlayArenaFighterStatusMessage()
        {
        }
        public GameRolePlayArenaFighterStatusMessage(short fightId,long playerId,bool accepted)
        {
            this.fightId = fightId;
            this.playerId = playerId;
            this.accepted = accepted;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteBoolean((bool)accepted);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayArenaFighterStatusMessage.fightId.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GameRolePlayArenaFighterStatusMessage.playerId.");
            }

            accepted = (bool)reader.ReadBoolean();
        }


    }
}








