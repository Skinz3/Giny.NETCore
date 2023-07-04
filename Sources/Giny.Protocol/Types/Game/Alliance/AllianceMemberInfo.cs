using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AllianceMemberInfo : SocialMember
    {
        public new const ushort Id = 1094;
        public override ushort TypeId => Id;

        public int avaRoleId;

        public AllianceMemberInfo()
        {
        }
        public AllianceMemberInfo(int avaRoleId, long id, string name, short level, byte breed, bool sex, byte connected, short hoursSinceLastConnection, int accountId, PlayerStatus status, int rankId, double enrollmentDate)
        {
            this.avaRoleId = avaRoleId;
            this.id = id;
            this.name = name;
            this.level = level;
            this.breed = breed;
            this.sex = sex;
            this.connected = connected;
            this.hoursSinceLastConnection = hoursSinceLastConnection;
            this.accountId = accountId;
            this.status = status;
            this.rankId = rankId;
            this.enrollmentDate = enrollmentDate;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)avaRoleId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            avaRoleId = (int)reader.ReadInt();
        }


    }
}


