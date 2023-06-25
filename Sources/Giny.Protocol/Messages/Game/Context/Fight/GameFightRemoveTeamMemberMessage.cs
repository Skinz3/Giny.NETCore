using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightRemoveTeamMemberMessage : NetworkMessage  
    { 
        public  const ushort Id = 6771;
        public override ushort MessageId => Id;

        public short fightId;
        public byte teamId;
        public double charId;

        public GameFightRemoveTeamMemberMessage()
        {
        }
        public GameFightRemoveTeamMemberMessage(short fightId,byte teamId,double charId)
        {
            this.fightId = fightId;
            this.teamId = teamId;
            this.charId = charId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteByte((byte)teamId);
            if (charId < -9.00719925474099E+15 || charId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + charId + ") on element charId.");
            }

            writer.WriteDouble((double)charId);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameFightRemoveTeamMemberMessage.fightId.");
            }

            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of GameFightRemoveTeamMemberMessage.teamId.");
            }

            charId = (double)reader.ReadDouble();
            if (charId < -9.00719925474099E+15 || charId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + charId + ") on element of GameFightRemoveTeamMemberMessage.charId.");
            }

        }


    }
}








