using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaFightAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 8880;
        public override ushort MessageId => Id;

        public short fightId;
        public bool accept;

        public GameRolePlayArenaFightAnswerMessage()
        {
        }
        public GameRolePlayArenaFightAnswerMessage(short fightId,bool accept)
        {
            this.fightId = fightId;
            this.accept = accept;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteBoolean((bool)accept);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayArenaFightAnswerMessage.fightId.");
            }

            accept = (bool)reader.ReadBoolean();
        }


    }
}








