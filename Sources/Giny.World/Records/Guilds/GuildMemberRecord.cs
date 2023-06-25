using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Guilds;
using Giny.World.Network;
using Giny.World.Records.Characters;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public GuildRightsEnum Rights
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public short MoodSmileyId
        {
            get;
            set;
        }

        public GuildMemberRecord()
        {

        }
        [WIP]
        public GuildMemberRecord(Character character, bool owner)
        {
            CharacterId = character.Id;
            ExperienceGivenPercent = 0;
            GivenExperience = 0;
            MoodSmileyId = 0;
            Rank = (short)(owner ? 1 : 0);
            Rights = owner ? GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS : 0;
        }

        [WIP]
        public GuildMember ToGuildMember(Guild guild)
        {
            bool connected = guild.IsMemberConnected(CharacterId);

            CharacterRecord record = CharacterRecord.GetCharacterRecord(CharacterId);

            WorldClient client = WorldServer.Instance.GetOnlineClient(x => x.Character.Name == record.Name);

            return new GuildMember()
            {
                accountId = connected ? client.Account.Id : 0,
                achievementPoints = 0,
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
                note = new PlayerNote("test", 0),
                sex = record.Sex,
                status = connected ? client.Character.GetPlayerStatus() : new PlayerStatus()
            };
        }

        public bool HasRight(GuildRightsEnum rights)
        {
            return Rights.HasFlag(rights) || Rights.HasFlag(GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS);
        }
    }
}
