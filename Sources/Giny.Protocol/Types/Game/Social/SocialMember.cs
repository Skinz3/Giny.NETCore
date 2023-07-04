using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SocialMember : CharacterMinimalInformations
    {
        public new const ushort Id = 7475;
        public override ushort TypeId => Id;

        public byte breed;
        public bool sex;
        public byte connected;
        public short hoursSinceLastConnection;
        public int accountId;
        public PlayerStatus status;
        public int rankId;
        public double enrollmentDate;

        public SocialMember()
        {
        }
        public SocialMember(byte breed, bool sex, byte connected, short hoursSinceLastConnection, int accountId, PlayerStatus status, int rankId, double enrollmentDate, long id, string name, short level)
        {
            this.breed = breed;
            this.sex = sex;
            this.connected = connected;
            this.hoursSinceLastConnection = hoursSinceLastConnection;
            this.accountId = accountId;
            this.status = status;
            this.rankId = rankId;
            this.enrollmentDate = enrollmentDate;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            writer.WriteByte((byte)connected);
            if (hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535)
            {
                throw new System.Exception("Forbidden value (" + hoursSinceLastConnection + ") on element hoursSinceLastConnection.");
            }

            writer.WriteShort((short)hoursSinceLastConnection);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
            writer.WriteInt((int)rankId);
            if (enrollmentDate < -9007199254740992 || enrollmentDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + enrollmentDate + ") on element enrollmentDate.");
            }

            writer.WriteDouble((double)enrollmentDate);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            breed = (byte)reader.ReadByte();
            sex = (bool)reader.ReadBoolean();
            connected = (byte)reader.ReadByte();
            if (connected < 0)
            {
                throw new System.Exception("Forbidden value (" + connected + ") on element of SocialMember.connected.");
            }

            hoursSinceLastConnection = (short)reader.ReadUShort();
            if (hoursSinceLastConnection < 0 || hoursSinceLastConnection > 65535)
            {
                throw new System.Exception("Forbidden value (" + hoursSinceLastConnection + ") on element of SocialMember.hoursSinceLastConnection.");
            }

            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of SocialMember.accountId.");
            }

            uint _id6 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id6);
            status.Deserialize(reader);
            rankId = (int)reader.ReadInt();
            enrollmentDate = (double)reader.ReadDouble();
            if (enrollmentDate < -9007199254740992 || enrollmentDate > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + enrollmentDate + ") on element of SocialMember.enrollmentDate.");
            }

        }


    }
}


