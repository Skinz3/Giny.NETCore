using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorStaticInformations  
    { 
        public const ushort Id = 3183;
        public virtual ushort TypeId => Id;

        public short firstNameId;
        public short lastNameId;
        public GuildInformations guildIdentity;
        public long callerId;

        public TaxCollectorStaticInformations()
        {
        }
        public TaxCollectorStaticInformations(short firstNameId,short lastNameId,GuildInformations guildIdentity,long callerId)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.guildIdentity = guildIdentity;
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
            guildIdentity.Serialize(writer);
            if (callerId < 0 || callerId > 9.00719925474099E+15)
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

            guildIdentity = new GuildInformations();
            guildIdentity.Deserialize(reader);
            callerId = (long)reader.ReadVarUhLong();
            if (callerId < 0 || callerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + callerId + ") on element of TaxCollectorStaticInformations.callerId.");
            }

        }


    }
}








