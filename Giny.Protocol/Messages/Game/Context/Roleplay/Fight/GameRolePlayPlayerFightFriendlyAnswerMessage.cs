using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayPlayerFightFriendlyAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 2841;
        public override ushort MessageId => Id;

        public short fightId;
        public bool accept;

        public GameRolePlayPlayerFightFriendlyAnswerMessage()
        {
        }
        public GameRolePlayPlayerFightFriendlyAnswerMessage(short fightId,bool accept)
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
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayPlayerFightFriendlyAnswerMessage.fightId.");
            }

            accept = (bool)reader.ReadBoolean();
        }


    }
}








