using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations  
    { 
        public new const ushort Id = 5019;
        public override ushort TypeId => Id;

        public short firstNameId;
        public short lastNameId;
        public byte level;
        public int guildId;
        public double uid;

        public FightTeamMemberTaxCollectorInformations()
        {
        }
        public FightTeamMemberTaxCollectorInformations(short firstNameId,short lastNameId,byte level,int guildId,double uid,double id)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.level = level;
            this.guildId = guildId;
            this.uid = uid;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element firstNameId.");
            }

            writer.WriteVarShort((short)firstNameId);
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element lastNameId.");
            }

            writer.WriteVarShort((short)lastNameId);
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element guildId.");
            }

            writer.WriteVarInt((int)guildId);
            if (uid < 0 || uid > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteDouble((double)uid);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            firstNameId = (short)reader.ReadVarUhShort();
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element of FightTeamMemberTaxCollectorInformations.firstNameId.");
            }

            lastNameId = (short)reader.ReadVarUhShort();
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element of FightTeamMemberTaxCollectorInformations.lastNameId.");
            }

            level = (byte)reader.ReadSByte();
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of FightTeamMemberTaxCollectorInformations.level.");
            }

            guildId = (int)reader.ReadVarUhInt();
            if (guildId < 0)
            {
                throw new System.Exception("Forbidden value (" + guildId + ") on element of FightTeamMemberTaxCollectorInformations.guildId.");
            }

            uid = (double)reader.ReadDouble();
            if (uid < 0 || uid > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of FightTeamMemberTaxCollectorInformations.uid.");
            }

        }


    }
}








