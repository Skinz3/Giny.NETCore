using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildMemberInfo : SocialMember
    {
        public new const ushort Id = 5050;
        public override ushort TypeId => Id;

        public long givenExperience;
        public byte experienceGivenPercent;
        public byte alignmentSide;
        public short moodSmileyId;
        public int achievementPoints;
        public bool havenBagShared;
        public PlayerNote note;

        public GuildMemberInfo()
        {
        }
        public GuildMemberInfo(long givenExperience, byte experienceGivenPercent, byte alignmentSide, short moodSmileyId, int achievementPoints, bool havenBagShared, PlayerNote note, long id, string name, short level, byte breed, bool sex, byte connected, short hoursSinceLastConnection, int accountId, PlayerStatus status, int rankId, double enrollmentDate)
        {
            this.givenExperience = givenExperience;
            this.experienceGivenPercent = experienceGivenPercent;
            this.alignmentSide = alignmentSide;
            this.moodSmileyId = moodSmileyId;
            this.achievementPoints = achievementPoints;
            this.havenBagShared = havenBagShared;
            this.note = note;
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
            if (givenExperience < 0 || givenExperience > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + givenExperience + ") on element givenExperience.");
            }

            writer.WriteVarLong((long)givenExperience);
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element experienceGivenPercent.");
            }

            writer.WriteByte((byte)experienceGivenPercent);
            writer.WriteByte((byte)alignmentSide);
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element moodSmileyId.");
            }

            writer.WriteVarShort((short)moodSmileyId);
            writer.WriteInt((int)achievementPoints);
            writer.WriteBoolean((bool)havenBagShared);
            note.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            givenExperience = (long)reader.ReadVarUhLong();
            if (givenExperience < 0 || givenExperience > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + givenExperience + ") on element of GuildMemberInfo.givenExperience.");
            }

            experienceGivenPercent = (byte)reader.ReadByte();
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element of GuildMemberInfo.experienceGivenPercent.");
            }

            alignmentSide = (byte)reader.ReadByte();
            moodSmileyId = (short)reader.ReadVarUhShort();
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element of GuildMemberInfo.moodSmileyId.");
            }

            achievementPoints = (int)reader.ReadInt();
            havenBagShared = (bool)reader.ReadBoolean();
            note = new PlayerNote();
            note.Deserialize(reader);
        }


    }
}


