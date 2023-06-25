using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTeamMemberInformations  
    { 
        public const ushort Id = 6434;
        public virtual ushort TypeId => Id;

        public double id;

        public FightTeamMemberInformations()
        {
        }
        public FightTeamMemberInformations(double id)
        {
            this.id = id;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9.00719925474099E+15 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of FightTeamMemberInformations.id.");
            }

        }


    }
}








