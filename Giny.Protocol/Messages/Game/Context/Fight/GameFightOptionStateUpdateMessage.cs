using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightOptionStateUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 2151;
        public override ushort MessageId => Id;

        public short fightId;
        public byte teamId;
        public byte option;
        public bool state;

        public GameFightOptionStateUpdateMessage()
        {
        }
        public GameFightOptionStateUpdateMessage(short fightId,byte teamId,byte option,bool state)
        {
            this.fightId = fightId;
            this.teamId = teamId;
            this.option = option;
            this.state = state;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteByte((byte)teamId);
            writer.WriteByte((byte)option);
            writer.WriteBoolean((bool)state);
        }
        public override void Deserialize(IDataReader reader)
        {
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of GameFightOptionStateUpdateMessage.fightId.");
            }

            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of GameFightOptionStateUpdateMessage.teamId.");
            }

            option = (byte)reader.ReadByte();
            if (option < 0)
            {
                throw new System.Exception("Forbidden value (" + option + ") on element of GameFightOptionStateUpdateMessage.option.");
            }

            state = (bool)reader.ReadBoolean();
        }


    }
}








