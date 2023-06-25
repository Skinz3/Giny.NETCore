using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildInAllianceVersatileInformations : GuildVersatileInformations  
    { 
        public new const ushort Id = 3145;
        public override ushort TypeId => Id;

        public int allianceId;

        public GuildInAllianceVersatileInformations()
        {
        }
        public GuildInAllianceVersatileInformations(int allianceId,int guildId,long leaderId,byte guildLevel,byte nbMembers)
        {
            this.allianceId = allianceId;
            this.guildId = guildId;
            this.leaderId = leaderId;
            this.guildLevel = guildLevel;
            this.nbMembers = nbMembers;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of GuildInAllianceVersatileInformations.allianceId.");
            }

        }


    }
}








