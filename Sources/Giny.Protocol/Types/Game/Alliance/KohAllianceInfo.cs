using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class KohAllianceInfo
    {
        public const ushort Id = 4416;
        public virtual ushort TypeId => Id;

        public AllianceInformation alliance;
        public long memberCount;
        public KohAllianceRoleMembers[] kohAllianceRoleMembers;
        public KohScore[] scores;
        public int matchDominationScores;

        public KohAllianceInfo()
        {
        }
        public KohAllianceInfo(AllianceInformation alliance, long memberCount, KohAllianceRoleMembers[] kohAllianceRoleMembers, KohScore[] scores, int matchDominationScores)
        {
            this.alliance = alliance;
            this.memberCount = memberCount;
            this.kohAllianceRoleMembers = kohAllianceRoleMembers;
            this.scores = scores;
            this.matchDominationScores = matchDominationScores;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            alliance.Serialize(writer);
            if (memberCount < 0 || memberCount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberCount + ") on element memberCount.");
            }

            writer.WriteVarLong((long)memberCount);
            writer.WriteShort((short)kohAllianceRoleMembers.Length);
            for (uint _i3 = 0; _i3 < kohAllianceRoleMembers.Length; _i3++)
            {
                (kohAllianceRoleMembers[_i3] as KohAllianceRoleMembers).Serialize(writer);
            }

            writer.WriteShort((short)scores.Length);
            for (uint _i4 = 0; _i4 < scores.Length; _i4++)
            {
                (scores[_i4] as KohScore).Serialize(writer);
            }

            if (matchDominationScores < 0)
            {
                throw new System.Exception("Forbidden value (" + matchDominationScores + ") on element matchDominationScores.");
            }

            writer.WriteVarInt((int)matchDominationScores);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            KohAllianceRoleMembers _item3 = null;
            KohScore _item4 = null;
            alliance = new AllianceInformation();
            alliance.Deserialize(reader);
            memberCount = (long)reader.ReadVarUhLong();
            if (memberCount < 0 || memberCount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberCount + ") on element of KohAllianceInfo.memberCount.");
            }

            uint _kohAllianceRoleMembersLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0; _i3 < _kohAllianceRoleMembersLen; _i3++)
            {
                _item3 = new KohAllianceRoleMembers();
                _item3.Deserialize(reader);
                kohAllianceRoleMembers[_i3] = _item3;
            }

            uint _scoresLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0; _i4 < _scoresLen; _i4++)
            {
                _item4 = new KohScore();
                _item4.Deserialize(reader);
                scores[_i4] = _item4;
            }

            matchDominationScores = (int)reader.ReadVarUhInt();
            if (matchDominationScores < 0)
            {
                throw new System.Exception("Forbidden value (" + matchDominationScores + ") on element of KohAllianceInfo.matchDominationScores.");
            }

        }


    }
}


