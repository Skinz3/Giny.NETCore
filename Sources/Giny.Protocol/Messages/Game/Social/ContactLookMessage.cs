using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ContactLookMessage : NetworkMessage  
    { 
        public  const ushort Id = 2975;
        public override ushort MessageId => Id;

        public int requestId;
        public string playerName;
        public long playerId;
        public EntityLook look;

        public ContactLookMessage()
        {
        }
        public ContactLookMessage(int requestId,string playerName,long playerId,EntityLook look)
        {
            this.requestId = requestId;
            this.playerName = playerName;
            this.playerId = playerId;
            this.look = look;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element requestId.");
            }

            writer.WriteVarInt((int)requestId);
            writer.WriteUTF((string)playerName);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            look.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            requestId = (int)reader.ReadVarUhInt();
            if (requestId < 0)
            {
                throw new System.Exception("Forbidden value (" + requestId + ") on element of ContactLookMessage.requestId.");
            }

            playerName = (string)reader.ReadUTF();
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of ContactLookMessage.playerId.");
            }

            look = new EntityLook();
            look.Deserialize(reader);
        }


    }
}








