using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildListApplicationModifiedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4943;
        public override ushort MessageId => Id;

        public GuildApplicationInformation apply;
        public byte state;
        public long playerId;

        public GuildListApplicationModifiedMessage()
        {
        }
        public GuildListApplicationModifiedMessage(GuildApplicationInformation apply,byte state,long playerId)
        {
            this.apply = apply;
            this.state = state;
            this.playerId = playerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            apply.Serialize(writer);
            writer.WriteByte((byte)state);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            apply = new GuildApplicationInformation();
            apply.Deserialize(reader);
            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of GuildListApplicationModifiedMessage.state.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildListApplicationModifiedMessage.playerId.");
            }

        }


    }
}








