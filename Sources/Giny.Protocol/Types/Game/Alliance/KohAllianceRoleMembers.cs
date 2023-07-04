using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class KohAllianceRoleMembers
    {
        public const ushort Id = 4626;
        public virtual ushort TypeId => Id;

        public long memberCount;
        public int roleAvAId;

        public KohAllianceRoleMembers()
        {
        }
        public KohAllianceRoleMembers(long memberCount, int roleAvAId)
        {
            this.memberCount = memberCount;
            this.roleAvAId = roleAvAId;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (memberCount < 0 || memberCount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberCount + ") on element memberCount.");
            }

            writer.WriteVarLong((long)memberCount);
            writer.WriteInt((int)roleAvAId);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            memberCount = (long)reader.ReadVarUhLong();
            if (memberCount < 0 || memberCount > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberCount + ") on element of KohAllianceRoleMembers.memberCount.");
            }

            roleAvAId = (int)reader.ReadInt();
        }


    }
}


