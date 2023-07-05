using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TaxCollectorStaticInformations
    {
        public const ushort Id = 7297;
        public virtual ushort TypeId => Id;

        public short firstNameId;
        public short lastNameId;
        public AllianceInformation allianceIdentity;
        public long callerId;

        public TaxCollectorStaticInformations()
        {
        }
        public TaxCollectorStaticInformations(short firstNameId, short lastNameId, AllianceInformation allianceIdentity, long callerId)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.allianceIdentity = allianceIdentity;
            this.callerId = callerId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
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
            allianceIdentity.Serialize(writer);
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element callerId.");
            }

            writer.WriteVarLong((long)callerId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            firstNameId = (short)reader.ReadVarUhShort();
            if (firstNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + firstNameId + ") on element of TaxCollectorStaticInformations.firstNameId.");
            }

            lastNameId = (short)reader.ReadVarUhShort();
            if (lastNameId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastNameId + ") on element of TaxCollectorStaticInformations.lastNameId.");
            }

            allianceIdentity = new AllianceInformation();
            allianceIdentity.Deserialize(reader);
            callerId = (long)reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element of TaxCollectorStaticInformations.callerId.");
            }

        }


    }
}


