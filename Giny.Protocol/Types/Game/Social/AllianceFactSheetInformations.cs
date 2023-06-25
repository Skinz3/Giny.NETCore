using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AllianceFactSheetInformations : AllianceInformations  
    { 
        public new const ushort Id = 4137;
        public override ushort TypeId => Id;

        public int creationDate;
        public short nbGuilds;
        public short nbMembers;
        public short nbSubarea;

        public AllianceFactSheetInformations()
        {
        }
        public AllianceFactSheetInformations(int creationDate,short nbGuilds,short nbMembers,short nbSubarea,int allianceId,string allianceTag,string allianceName,GuildEmblem allianceEmblem)
        {
            this.creationDate = creationDate;
            this.nbGuilds = nbGuilds;
            this.nbMembers = nbMembers;
            this.nbSubarea = nbSubarea;
            this.allianceId = allianceId;
            this.allianceTag = allianceTag;
            this.allianceName = allianceName;
            this.allianceEmblem = allianceEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element creationDate.");
            }

            writer.WriteInt((int)creationDate);
            if (nbGuilds < 0)
            {
                throw new System.Exception("Forbidden value (" + nbGuilds + ") on element nbGuilds.");
            }

            writer.WriteVarShort((short)nbGuilds);
            if (nbMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element nbMembers.");
            }

            writer.WriteVarShort((short)nbMembers);
            if (nbSubarea < 0)
            {
                throw new System.Exception("Forbidden value (" + nbSubarea + ") on element nbSubarea.");
            }

            writer.WriteVarShort((short)nbSubarea);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creationDate = (int)reader.ReadInt();
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of AllianceFactSheetInformations.creationDate.");
            }

            nbGuilds = (short)reader.ReadVarUhShort();
            if (nbGuilds < 0)
            {
                throw new System.Exception("Forbidden value (" + nbGuilds + ") on element of AllianceFactSheetInformations.nbGuilds.");
            }

            nbMembers = (short)reader.ReadVarUhShort();
            if (nbMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element of AllianceFactSheetInformations.nbMembers.");
            }

            nbSubarea = (short)reader.ReadVarUhShort();
            if (nbSubarea < 0)
            {
                throw new System.Exception("Forbidden value (" + nbSubarea + ") on element of AllianceFactSheetInformations.nbSubarea.");
            }

        }


    }
}








