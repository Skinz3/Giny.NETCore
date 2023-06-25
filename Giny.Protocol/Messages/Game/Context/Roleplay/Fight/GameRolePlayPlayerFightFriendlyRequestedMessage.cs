using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayPlayerFightFriendlyRequestedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6157;
        public override ushort MessageId => Id;

        public short fightId;
        public long sourceId;
        public long targetId;

        public GameRolePlayPlayerFightFriendlyRequestedMessage()
        {
        }
        public GameRolePlayPlayerFightFriendlyRequestedMessage(short fightId,long sourceId,long targetId)
        {
            this.fightId = fightId;
            this.sourceId = sourceId;
            this.targetId = targetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            if (sourceId < 0 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element sourceId.");
            }

            writer.WriteVarLong((long)sourceId);
            if (targetId < 0 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteVarLong((long)targetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameRolePlayPlayerFightFriendlyRequestedMessage.fightId.");
            }

            sourceId = (long)reader.ReadVarUhLong();
            if (sourceId < 0 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element of GameRolePlayPlayerFightFriendlyRequestedMessage.sourceId.");
            }

            targetId = (long)reader.ReadVarUhLong();
            if (targetId < 0 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameRolePlayPlayerFightFriendlyRequestedMessage.targetId.");
            }

        }


    }
}








