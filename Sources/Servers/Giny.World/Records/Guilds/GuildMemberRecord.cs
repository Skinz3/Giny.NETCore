using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Guilds;
using Giny.World.Records.Characters;
using ProtoBuf;

namespace Giny.World.Records.Guilds
{
    [ProtoContract]
    public class GuildMemberRecord
    {
        [ProtoMember(1)]
        public long CharacterId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public int Rank
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public long GivenExperience
        {
            get;
            set;
        }

        [ProtoMember(4)]
        public byte ExperienceGivenPercent
        {
            get;
            set;
        }

        [ProtoMember(5)]
        public short MoodSmileyId
        {
            get;
            set;
        }

        [ProtoMember(6)]
        public string Note
        {
            get;
            set;
        }

        [ProtoMember(7)]
        public DateTime NoteLastEditDate
        {
            get;
            set;
        }

        public GuildMemberRecord()
        {

        }

        [Annotation]
        public GuildMemberRecord(Character character, int rankId)
        {
            CharacterId = character.Id;
            ExperienceGivenPercent = 0;
            GivenExperience = 0;
            MoodSmileyId = 0;
            Rank = rankId;
            Note = "";
            NoteLastEditDate = DateTime.Now;
        }

        [Annotation]
        public GuildMemberInfo ToGuildMember(Guild guild)
        {
            bool connected = guild.IsMemberConnected(CharacterId);

            CharacterRecord record = CharacterRecord.GetCharacterRecord(CharacterId);

            Character character = guild.GetOnlineMember(record.Name);

            return new GuildMemberInfo()
            {
                enrollmentDate = 0,
                accountId = connected ? character.Client.Account.Id : record.AccountId,
                achievementPoints = connected ? (character.AchievementPoints ?? 0) : 0,
                alignmentSide = 0,
                breed = record.BreedId,
                connected = (byte)(connected ? 1 : 0),
                experienceGivenPercent = ExperienceGivenPercent,
                givenExperience = GivenExperience,
                havenBagShared = false,
                hoursSinceLastConnection = 0,
                id = CharacterId,
                level = ExperienceManager.Instance.GetCharacterLevel(record.Experience),
                moodSmileyId = MoodSmileyId,
                name = record.Name,
                rankId = Rank,
                note = new PlayerNote(Note, NoteLastEditDate.GetUnixTimeStamp()),
                sex = record.Sex,
                status = connected ? character.GetPlayerStatus() : new PlayerStatus((byte)PlayerStatusEnum.PLAYER_STATUS_OFFLINE)
            };
        }

        public CharacterMinimalSocialPublicInformations ToCharacterMinimalGuildPublicInformations()
        {
            CharacterRecord record = CharacterRecord.GetCharacterRecord(CharacterId);

            return new CharacterMinimalSocialPublicInformations()
            {
                rank = new RankPublicInformation(1, 1, 1, "Meneur"),
                id = CharacterId,
                name = record.Name,
                level = ExperienceManager.Instance.GetCharacterLevel(record.Experience),
            };
        }

        public bool HasRight(GuildRightsEnum rights, Guild guild)
        {
            var rank = guild.GetGuildRank(Rank);

            if (rank != null)
                return rank.Rights.Contains(rights) || rank.Rights.Contains(GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS);

            return false;
        }

    }


}