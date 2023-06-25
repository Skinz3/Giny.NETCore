using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Pool;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
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
        [WIP]
        public void RemoveGuild(Guild guild)
        {
            guild.Record.RemoveElement();
            Guilds.TryRemove(guild.Id);
        }
        public GuildCreationResultEnum CreateGuild(Character owner, string guildName, GuildEmblem guildEmblem)
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
                Motd = new GuildMotd(),
                Name = guildName
            };

            Guild instance = new Guild(record);

            if (instance.Join(owner, true))
            {
                Guilds.TryAdd(record.Id, instance);

                record.AddElement();
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
        [WIP]
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
    }
}
