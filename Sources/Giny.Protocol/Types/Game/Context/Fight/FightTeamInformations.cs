using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTeamInformations : AbstractFightTeamInformations  
    { 
        public new const ushort Id = 7466;
        public override ushort TypeId => Id;

        public FightTeamMemberInformations[] teamMembers;

        public FightTeamInformations()
        {
        }
        public FightTeamInformations(FightTeamMemberInformations[] teamMembers,byte teamId,double leaderId,byte teamSide,byte teamTypeId,byte nbWaves)
        {
            this.teamMembers = teamMembers;
            this.teamId = teamId;
            this.leaderId = leaderId;
            this.teamSide = teamSide;
            this.teamTypeId = teamTypeId;
            this.nbWaves = nbWaves;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)teamMembers.Length);
            for (uint _i1 = 0;_i1 < teamMembers.Length;_i1++)
            {
                writer.WriteShort((short)(teamMembers[_i1] as FightTeamMemberInformations).TypeId);
                (teamMembers[_i1] as FightTeamMemberInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            FightTeamMemberInformations _item1 = null;
            base.Deserialize(reader);
            uint _teamMembersLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _teamMembersLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<FightTeamMemberInformations>((short)_id1);
                _item1.Deserialize(reader);
                teamMembers[_i1] = _item1;
            }

        }


    }
}








