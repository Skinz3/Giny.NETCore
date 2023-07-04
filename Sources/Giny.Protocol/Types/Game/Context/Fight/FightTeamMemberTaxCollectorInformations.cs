using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations
    {
        public new const ushort Id = 6760;
        public override ushort TypeId => Id;

        public short firstNameId;
        public short lastNameId;
        public int groupId;
        public double uid;

        public FightTeamMemberTaxCollectorInformations()
        {
        }
        public FightTeamMemberTaxCollectorInformations(short firstNameId, short lastNameId, int groupId, double uid, double id)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.groupId = groupId;
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
            if (groupId < 0)
            {
                throw new System.Exception("Forbidden value (" + groupId + ") on element groupId.");
            }

            writer.WriteVarInt((int)groupId);
            if (uid < 0 || uid > 9007199254740992)
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

            groupId = (int)reader.ReadVarUhInt();
            if (groupId < 0)
            {
                throw new System.Exception("Forbidden value (" + groupId + ") on element of FightTeamMemberTaxCollectorInformations.groupId.");
            }

            uid = (double)reader.ReadDouble();
            if (uid < 0 || uid > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of FightTeamMemberTaxCollectorInformations.uid.");
            }

        }


    }
}


