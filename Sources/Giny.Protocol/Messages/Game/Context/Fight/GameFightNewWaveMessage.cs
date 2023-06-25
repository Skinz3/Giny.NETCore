using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightNewWaveMessage : NetworkMessage  
    { 
        public  const ushort Id = 283;
        public override ushort MessageId => Id;

        public byte id;
        public byte teamId;
        public short nbTurnBeforeNextWave;

        public GameFightNewWaveMessage()
        {
        }
        public GameFightNewWaveMessage(byte id,byte teamId,short nbTurnBeforeNextWave)
        {
            this.id = id;
            this.teamId = teamId;
            this.nbTurnBeforeNextWave = nbTurnBeforeNextWave;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteByte((byte)id);
            writer.WriteByte((byte)teamId);
            writer.WriteShort((short)nbTurnBeforeNextWave);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (byte)reader.ReadByte();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameFightNewWaveMessage.id.");
            }

            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of GameFightNewWaveMessage.teamId.");
            }

            nbTurnBeforeNextWave = (short)reader.ReadShort();
        }


    }
}








