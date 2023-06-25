using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildVersatileInformations  
    { 
        public const ushort Id = 9880;
        public virtual ushort TypeId => Id;

        public int guildId;
        public long leaderId;
        public byte guildLevel;
        public byte nbMembers;

        public GuildVersatileInformations()
        {
        }
        public GuildVersatileInformations(int guildId,long leaderId,byte guildLevel,byte nbMembers)
        {
            this.guildId = guildId;
            this.leaderId = leaderId;
            this.guildLevel = guildLevel;
            this.nbMembers = nbMembers;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element leaderId.");
            }

            writer.WriteVarLong((long)leaderId);
            if (guildLevel < 1 || guildLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + guildLevel + ") on element guildLevel.");
            }

            writer.WriteByte((byte)guildLevel);
            if (nbMembers < 1 || nbMembers > 240)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element nbMembers.");
            }

            writer.WriteByte((byte)nbMembers);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of GuildVersatileInformations.guildId.");
            }

            leaderId = (long)reader.ReadVarUhLong();
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element of GuildVersatileInformations.leaderId.");
            }

            guildLevel = (byte)reader.ReadSByte();
            if (guildLevel < 1 || guildLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + guildLevel + ") on element of GuildVersatileInformations.guildLevel.");
            }

            nbMembers = (byte)reader.ReadSByte();
            if (nbMembers < 1 || nbMembers > 240)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element of GuildVersatileInformations.nbMembers.");
            }

        }


    }
}








