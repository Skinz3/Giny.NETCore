using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Pool;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Characters;
using Giny.World.Records.Guilds;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Guilds
{
    public class GuildsManager : Singleton<GuildsManager>
    {
        public const int MaxMemberCount = 240;

        public const int MotdMaxLength = 255;

        private readonly ConcurrentDictionary<long, Guild> Guilds = new ConcurrentDictionary<long, Guild>();

        private UniqueIdProvider UniqueIdProvider
        {
            get;
            set;
        }
        GuildRightsEnum[] DefaultRights = new GuildRightsEnum[]
        {
            GuildRightsEnum.RIGHT_MANAGE_RANKS_AND_RIGHTS,
            GuildRightsEnum.RIGHT_MANAGE_CHEST_RIGHTS,
            GuildRightsEnum.RIGHT_MANAGE_SELF_XP_CONTRIBUTION,
            GuildRightsEnum.RIGHT_MANAGE_ALL_XP_CONTRIBUTION,
            GuildRightsEnum.RIGHT_MANAGE_RECRUITMENT,
            GuildRightsEnum.RIGHT_MANAGE_APPLY_AND_INVITATION,
                    GuildRightsEnum.RIGHT_UPDATE_MOTD,
                    GuildRightsEnum.RIGHT_OPEN_GUILD_CHEST,
                    GuildRightsEnum.RIGHT_USE_PADDOCKS,
                    GuildRightsEnum.RIGHT_KICK_MEMBER,
                    GuildRightsEnum.RIGHT_MANAGE_MEMBERS_NOTE,
                    GuildRightsEnum.RIGHT_UPDATE_BULLETIN,
                    GuildRightsEnum.RIGHT_MANAGE_PADDOCKS,
                    GuildRightsEnum.RIGHT_MANAGE_OTHER_MOUNTS,
                    GuildRightsEnum.RIGHT_MANAGE_OTHER_MOUNTS,
                    GuildRightsEnum.RIGHT_TAKE_FROM_GUILD_CHEST,
                    GuildRightsEnum.RIGHT_DROP_ON_GUILD_CHEST,
                    GuildRightsEnum.RIGHT_SHOW_TRANSACTION_PLAYER,
                    GuildRightsEnum.RIGHT_ASSIGN_RANKS,
                    GuildRightsEnum.RIGHT_BUY_FARM,
        };

        public static readonly double[][] XpPerGap = new double[][]
        {
            new double[]
            {
                0.0,
                10.0
            },
            new double[]
            {
                10.0,
                8.0
            },
            new double[]
            {
                20.0,
                6.0
            },
            new double[]
            {
                30.0,
                4.0
            },
            new double[]
            {
                40.0,
                3.0
            },
            new double[]
            {
                50.0,
                2.0
            },
            new double[]
            {
                60.0,
                1.5
            },
            new double[]
            {
                70.0,
                1.0
            }
        };

        [StartupInvoke("Guilds", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var guildRecord in GuildRecord.GetGuilds())
            {
                Guilds.TryAdd(guildRecord.Id, new Guild(guildRecord));
            }

            int lastId = 0;

            if (Guilds.Count > 0)
            {
                lastId = (int)Guilds.Keys.OrderByDescending(x => x).FirstOrDefault();
            }

            UniqueIdProvider = new UniqueIdProvider(lastId);

        }
        [Annotation]
        public void RemoveGuild(Guild guild)
        {
            guild.Record.RemoveLater();
            Guilds.TryRemove(guild.Id);
        }
        public GuildCreationResultEnum CreateGuild(Character owner, string guildName, SocialEmblem guildEmblem)
        {
            if (owner.HasGuild)
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_ALREADY_IN_GUILD;
            }

            GuildEmblemRecord emblem = new GuildEmblemRecord(guildEmblem.symbolShape, guildEmblem.symbolColor, guildEmblem.backgroundShape,
                guildEmblem.backgroundColor);

            if (GuildRecord.Exists(guildName))
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_NAME_ALREADY_EXISTS;
            }

            if (GuildRecord.Exists(emblem))
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_EMBLEM_ALREADY_EXISTS;
            }

            GuildRecord record = new GuildRecord()
            {
                Emblem = emblem,
                Experience = 0,
                Id = UniqueIdProvider.Pop(),
                CreationDate = DateTime.Now,
                Members = new List<GuildMemberRecord>(),
                Ranks = new List<GuildRankRecord>(),
                Motd = new GuildMotdRecord(),
                Bulletin = new GuildBulletinRecord(),
                Name = guildName,
                GlobalActivities = new List<GuildGlobalActivityRecord>(),
                ChestActivities = new List<GuildChestActivityRecord>(),
            };

            Guild instance = new Guild(record);

            //Creation des ranks par defaut
            instance.CreateRank(
                instance.GetRankNextOrder(),
                116,
                false,
                DefaultRights,
                Guild.BOSS_RANK_ID,
                "Maître de guilde"
            ); ;
            instance.CreateRank(instance.GetRankNextOrder(), 115, true, new GuildRightsEnum[0], Guild.OFFICER_RANK_ID, "Officier");
            instance.CreateRank(instance.GetRankNextOrder(), 114, true, new GuildRightsEnum[0], Guild.INITIATE_RANK_ID, "Initié");
            instance.CreateRank(instance.GetRankNextOrder(), 117, false, new GuildRightsEnum[0], Guild.NEWBIE_RANK_ID, "À l'essai");

            if (instance.Join(owner, Guild.BOSS_RANK_ID))
            {
                Guilds.TryAdd(record.Id, instance);

                record.AddLater();
                return GuildCreationResultEnum.GUILD_CREATE_OK;
            }
            else
            {
                return GuildCreationResultEnum.GUILD_CREATE_ERROR_REQUIREMENT_UNMET;
            }
        }

        public Guild GetGuild(long guildId)
        {
            return Guilds[guildId];
        }
        [Annotation]
        public void OnCharacterDeleted(CharacterRecord character)
        {
            Guild guild = GetGuild(character.GuildId);

            var member = guild.Record.GetMember(character.Id);

            guild.Record.Members.Remove(member);

            if (guild.Record.Members.Count == 0)
            {
                RemoveGuild(guild);
            }
        }

        public GuildSummaryMessage GetGuildSummaryMessage()
        {
            return new GuildSummaryMessage(Guilds.Select(x => x.Value.GetGuildFactSheetInformations()).ToArray(), 0, 1, 1);
        }
    }
}