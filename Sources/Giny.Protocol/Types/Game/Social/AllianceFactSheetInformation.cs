using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AllianceFactSheetInformation : AllianceInformation
    {
        public new const ushort Id = 2341;
        public override ushort TypeId => Id;

        public int creationDate;
        public short nbMembers;
        public short nbSubarea;
        public short nbTaxCollectors;
        public AllianceRecruitmentInformation recruitment;

        public AllianceFactSheetInformation()
        {
        }
        public AllianceFactSheetInformation(int creationDate, short nbMembers, short nbSubarea, short nbTaxCollectors, AllianceRecruitmentInformation recruitment, int allianceId, string allianceTag, string allianceName, SocialEmblem allianceEmblem)
        {
            this.creationDate = creationDate;
            this.nbMembers = nbMembers;
            this.nbSubarea = nbSubarea;
            this.nbTaxCollectors = nbTaxCollectors;
            this.recruitment = recruitment;
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
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element nbTaxCollectors.");
            }

            writer.WriteVarShort((short)nbTaxCollectors);
            recruitment.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creationDate = (int)reader.ReadInt();
            if (creationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + creationDate + ") on element of AllianceFactSheetInformation.creationDate.");
            }

            nbMembers = (short)reader.ReadVarUhShort();
            if (nbMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element of AllianceFactSheetInformation.nbMembers.");
            }

            nbSubarea = (short)reader.ReadVarUhShort();
            if (nbSubarea < 0)
            {
                throw new System.Exception("Forbidden value (" + nbSubarea + ") on element of AllianceFactSheetInformation.nbSubarea.");
            }

            nbTaxCollectors = (short)reader.ReadVarUhShort();
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element of AllianceFactSheetInformation.nbTaxCollectors.");
            }

            recruitment = new AllianceRecruitmentInformation();
            recruitment.Deserialize(reader);
        }


    }
}


