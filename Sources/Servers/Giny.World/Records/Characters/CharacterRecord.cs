using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
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
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public CharacterJob[] Jobs
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
        public List<CharacterAchievement> Achievements
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


        public CharacterRecord()
        {

        }
        public CharacterBaseInformations GetCharacterBaseInformations()
        {
            return new CharacterBaseInformations(Sex, Id, Name, ExperienceManager.Instance.GetCharacterLevel(Experience),
            Look.ToEntityLook(), BreedId);
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
            return new CharacterRecord()
            {
                AccountId = accountId,
                BreedId = breedId,
                MapId = ConfigFile.Instance.SpawnMapId,
                CellId = ConfigFile.Instance.SpawnCellId,
                CosmeticId = cosmeticId,
                Direction = 0,
                Experience = ExperienceManager.Instance.GetCharacterXPForLevel(ConfigFile.Instance.StartLevel),
                Id = id,
                Look = look,
                Name = name,
                Sex = sex,
                Stats = EntityStats.New(ConfigFile.Instance.StartLevel, breedId),
                Kamas = 0,
                KnownEmotes = new List<short>() { 1 },
                Shortcuts = new List<CharacterShortcut>(),
                SpawnPointMapId = ConfigFile.Instance.SpawnMapId,
                KnownOrnaments = new List<short>(),
                ActiveOrnamentId = 0,
                Spells = new List<CharacterSpell>(),
                ActiveTitleId = 0,
                KnownTitles = new List<short>(),
                Achievements = new List<CharacterAchievement>(),
                Jobs = CharacterJob.New().ToArray(),
                GuildId = 0,
            };
        }
        public static CharacterRecord GetCharacterRecord(long id)
        {
            return Characters.TryGetValue(id);
        }
        public static IEnumerable<CharacterRecord> GetCharacterRecords()
        {
            return Characters.Values;
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
