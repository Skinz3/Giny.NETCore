using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildLevelUpActivity : GuildLogbookEntryBasicInformation  
    { 
        public new const ushort Id = 7901;
        public override ushort TypeId => Id;

        public byte newGuildLevel;

        public GuildLevelUpActivity()
        {
        }
        public GuildLevelUpActivity(byte newGuildLevel,int id,double date)
        {
            this.newGuildLevel = newGuildLevel;
            this.id = id;
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (newGuildLevel < 0 || newGuildLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + newGuildLevel + ") on element newGuildLevel.");
            }

            writer.WriteByte((byte)newGuildLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            newGuildLevel = (byte)reader.ReadSByte();
            if (newGuildLevel < 0 || newGuildLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + newGuildLevel + ") on element of GuildLevelUpActivity.newGuildLevel.");
            }

        }


    }
}








