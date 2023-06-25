using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildLevelUpMessage : NetworkMessage  
    { 
        public  const ushort Id = 3477;
        public override ushort MessageId => Id;

        public byte newLevel;

        public GuildLevelUpMessage()
        {
        }
        public GuildLevelUpMessage(byte newLevel)
        {
            this.newLevel = newLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (newLevel < 2 || newLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element newLevel.");
            }

            writer.WriteByte((byte)newLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            newLevel = (byte)reader.ReadSByte();
            if (newLevel < 2 || newLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + newLevel + ") on element of GuildLevelUpMessage.newLevel.");
            }

        }


    }
}








