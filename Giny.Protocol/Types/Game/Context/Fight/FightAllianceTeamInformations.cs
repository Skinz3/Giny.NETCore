using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightAllianceTeamInformations : FightTeamInformations  
    { 
        public new const ushort Id = 6879;
        public override ushort TypeId => Id;

        public byte relation;

        public FightAllianceTeamInformations()
        {
        }
        public FightAllianceTeamInformations(byte relation,byte teamId,double leaderId,byte teamSide,byte teamTypeId,byte nbWaves,FightTeamMemberInformations[] teamMembers)
        {
            this.relation = relation;
            this.teamId = teamId;
            this.leaderId = leaderId;
            this.teamSide = teamSide;
            this.teamTypeId = teamTypeId;
            this.nbWaves = nbWaves;
            this.teamMembers = teamMembers;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)relation);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            relation = (byte)reader.ReadByte();
            if (relation < 0)
            {
                throw new System.Exception("Forbidden value (" + relation + ") on element of FightAllianceTeamInformations.relation.");
            }

        }


    }
}








