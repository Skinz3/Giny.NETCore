using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BasicGuildInformations : AbstractSocialGroupInfos  
    { 
        public new const ushort Id = 9709;
        public override ushort TypeId => Id;

        public int guildId;
        public string guildName;
        public byte guildLevel;

        public BasicGuildInformations()
        {
        }
        public BasicGuildInformations(int guildId,string guildName,byte guildLevel)
        {
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
            writer.WriteUTF((string)guildName);
            if (guildLevel < 0 || guildLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + guildLevel + ") on element guildLevel.");
            }

            writer.WriteByte((byte)guildLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of BasicGuildInformations.guildId.");
            }

            guildName = (string)reader.ReadUTF();
            guildLevel = (byte)reader.ReadSByte();
            if (guildLevel < 0 || guildLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + guildLevel + ") on element of BasicGuildInformations.guildLevel.");
            }

        }


    }
}








