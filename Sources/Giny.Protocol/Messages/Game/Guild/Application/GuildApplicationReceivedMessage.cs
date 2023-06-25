using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildApplicationReceivedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8561;
        public override ushort MessageId => Id;

        public string playerName;
        public long playerId;

        public GuildApplicationReceivedMessage()
        {
        }
        public GuildApplicationReceivedMessage(string playerName,long playerId)
        {
            this.playerName = playerName;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)playerName);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerName = (string)reader.ReadUTF();
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildApplicationReceivedMessage.playerId.");
            }

        }


    }
}








