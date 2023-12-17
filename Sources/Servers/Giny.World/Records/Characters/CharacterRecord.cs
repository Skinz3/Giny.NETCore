using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO.Configuration;
using Giny.Core.Pool;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Achievements;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Shortcuts;
using Giny.World.Managers.Spells;
using Giny.World.Managers.Stats;
using Giny.World.Network;
using Giny.World.Records.Arena;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records;

namespace Giny.World.Records.Characters
{
    [Table("characters")]
    public class CharacterRecord : IRecord
    {
        [Container]
        private static readonly ConcurrentDictionary<long, CharacterRecord> Characters = new ConcurrentDictionary<long, CharacterRecord>();

        private static UniqueLongIdProvider idProvider;

        [Primary]
        public long Id
        {
            get;
            set;
        }
        [Update]
        public string Name
        {
            get;
            set;
        }
        public int AccountId
        {
            get;
            set;
        }
        [Update]
        public ServerEntityLook Look
        {
            get;
            set;
        }

        [Update]
        public ServerEntityLook? ContextualLook
        {
            get;
            set;
        }
        [Update]
        public byte BreedId
        {
            get;
            set;
        }
        [Update]
        public short CosmeticId
        {
            get;
            set;
        }
        public bool Sex
        {
            get;
            set;
        }
        [Update]
        public long MapId
        {
            get;
            set;
        }
        [Update]
        public short CellId
        {
            get;
            set;
        }
        [Update]
        public DirectionsEnum Direction
        {
            get;
            set;
        }
        [Update]
        public long Experience
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public EntityStats Stats
        {
            get;
            set;
        }
        [Update]
        public long Kamas
        {
            get;
            set;
        }

        [ProtoSerialize]
        [Update]
        public List<short> KnownEmotes
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public List<CharacterShortcut> Shortcuts
        {
            get;
            set;
        }
        [Update]
        public long SpawnPointMapId
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public List<short> KnownOrnaments
        {
            get;
            set;
        }
        [Update]
        public short ActiveOrnamentId
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public List<CharacterSpell> Spells
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public List<short> KnownTitles
        {
            get;
            set;
        }
        [Update]
        public short ActiveTitleId
        {
            get;
            set;
        }
        [ProtoSerialize]
        [Update]
        public List<CharacterJob> Jobs
        {
            get;
            set;
        }

        [Update]
        public long GuildId
        {
            get;
            set;
        }

        [ProtoSerialize]
        [Update]
        public ArenaRankRecord ArenaRank1vs1
        {
            get;
            set;
        }

        [ProtoSerialize]
        [Update]
        public ArenaRankRecord ArenaRank3vs3
        {
            get;
            set;
        }

        [ProtoSerialize]
        [Update]
        public List<CharacterAchievement> Achievements
        {
            get;
            set;
        }

        [Update]
        [ProtoSerialize]
        public HardcoreInformations HardcoreInformations
        {
            get;
            set;
        }

        [Ignore]
        public int? FightId
        {
            get;
            set;
        }

        [Ignore]
        public bool IsInFight => FightId != null;

        /*
         * If the client has selected a character and 
         * a context (roleplay or fight) has been created
         * 
         * Do not use it if not necessary. Use WorldClient.InGame() instead
         */
        [Ignore]
        public bool InGameContext
        {
            get;
            set;
        }


        public CharacterBaseInformations GetCharacterBaseInformations(bool characterList)
        {
            if (characterList && WorldServer.Instance.IsEpicOrHardcore())
            {
                return new CharacterHardcoreOrEpicInformations((byte)HardcoreInformations.DeathState,
                    HardcoreInformations.DeathCount, HardcoreInformations.DeathMaxLevel, Id,
                    Name, ExperienceManager.Instance.GetCharacterLevel(Experience),
                    GetActiveLook().ToEntityLook(), BreedId, Sex);
            }


            return new CharacterBaseInformations(Sex, Id, Name, ExperienceManager.Instance.GetCharacterLevel(Experience),
            GetActiveLook().ToEntityLook(), BreedId);
        }

        public ServerEntityLook GetActiveLook()
        {
            return ContextualLook != null ? ContextualLook : Look;
        }
        [StartupInvoke(StartupInvokePriority.Last)]
        public static void Initialize()
        {
            long lastId = Characters.Count > 0 ? Characters.Keys.OrderByDescending(x => x).First() : 0;
            idProvider = new UniqueLongIdProvider(lastId);

            foreach (var character in Characters.Values)
            {
                character.Stats.Initialize();

                foreach (var characterAchievement in character.Achievements)
                {
                    characterAchievement.Initialize();
                }
            }
        }


        public static bool NameExist(string name)
        {
            return Characters.Values.Any(x => x.Name.ToLower() == name.ToLower());
        }

        public static CharacterRecord Create(long id, string name, int accountId, ServerEntityLook look, byte breedId, short cosmeticId, bool sex)
        {
            WorldConfig config = ConfigManager<WorldConfig>.Instance;

            return new CharacterRecord()
            {
                AccountId = accountId,
                BreedId = breedId,
                MapId = config.SpawnMapId,
                CellId = ConfigManager<WorldConfig>.Instance.SpawnCellId,
                CosmeticId = cosmeticId,
                Direction = 0,
                Experience = ExperienceManager.Instance.GetCharacterXPForLevel(config.StartLevel),
                Id = id,
                Look = look,
                Name = name,
                Sex = sex,
                Stats = EntityStats.New(config.StartLevel),
                Kamas = 0,
                KnownEmotes = new List<short>() { 1 },
                Shortcuts = new List<CharacterShortcut>(),
                SpawnPointMapId = config.SpawnMapId,
                KnownOrnaments = new List<short>(),
                ActiveOrnamentId = 0,
                Spells = new List<CharacterSpell>(),
                ActiveTitleId = 0,
                KnownTitles = new List<short>(),
                Achievements = new List<CharacterAchievement>(),
                Jobs = CharacterJob.New(),
                GuildId = 0,
                HardcoreInformations = new HardcoreInformations(),
                ArenaRank1vs1 = ArenaRankRecord.New(),
                ArenaRank3vs3 = ArenaRankRecord.New(),
                ContextualLook = null,
            };
        }
        public static CharacterRecord GetCharacterRecord(long id)
        {
            return Characters.TryGetValue(id);
        }
        public static IEnumerable<CharacterRecord> GetCharacterRecords()
        {
            return Characters.Values.ToList();
        }
        public static List<CharacterRecord> GetCharactersByAccountId(int id)
        {
            return Characters.Values.Where(x => x.AccountId == id).ToList();
        }
        public static long NextId()
        {
            return idProvider.Pop();
        }

    }
}
