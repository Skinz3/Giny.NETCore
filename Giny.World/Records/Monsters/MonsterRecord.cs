using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Time;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Monsters;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Monsters
{
    [D2OClass("Monster")]
    [Table("monsters")]
    public class MonsterRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, MonsterRecord> Monsters = new ConcurrentDictionary<long, MonsterRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("race")]
        public MonsterRacesEnum Race
        {
            get;
            set;
        }
        [D2OField("look")]
        public ServerEntityLook Look
        {
            get;
            set;
        }

        [D2OField("useSummonSlot")]
        public bool UseSummonSlot
        {
            get;
            set;
        }
        [D2OField("useBombSlot")]
        public bool UseBombSlot
        {
            get;
            set;
        }
        [D2OField("canPlay")]
        public bool CanPlay
        {
            get;
            set;
        }
        [D2OField("canTackle")]
        public bool CanTackle
        {
            get;
            set;
        }
        [D2OField("isBoss")]
        public bool IsBoss
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("spells")]
        public long[] Spells
        {
            get;
            set;
        }
        [Ignore]
        public ConcurrentDictionary<short, SpellRecord> SpellRecords
        {
            get;
            set;
        }
        [D2OField("isMiniBoss")]
        public bool IsMiniBoss
        {
            get;
            set;
        }
        [D2OField("isQuestMonster")]
        public bool IsQuestMonster
        {
            get;
            set;
        }
        [D2OField("correspondingMiniBossId")]
        public long CorrespondingMiniBossId
        {
            get;
            set;
        }
        [D2OField("canBePushed")]
        public bool CanBePushed
        {
            get;
            set;
        }
        [D2OField("canBeCarried")]
        public bool CanBeCarried
        {
            get;
            set;
        }
        [D2OField("canUsePortal")]
        public bool CanUsePortal
        {
            get;
            set;
        }
        [D2OField("canSwitchPos")]
        public bool CanSwitchPosition
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("drops")]
        public MonsterDrop[] Drops
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("grades")]
        public MonsterGrade[] Grades
        {
            get;
            set;
        }
        [Update]
        public int MinDroppedKamas
        {
            get;
            set;
        }
        [Update]
        public int MaxDroppedKamas
        {
            get;
            set;
        }

        [StartupInvoke(StartupInvokePriority.FourthPass)]
        public static void Initialize()
        {
            foreach (var monster in Monsters.Values)
            {
                monster.SpellRecords = new ConcurrentDictionary<short, SpellRecord>();

                foreach (var spellId in monster.Spells)
                {
                    SpellRecord spellRecord = SpellRecord.GetSpellRecord((short)spellId);

                    if (spellRecord != null)
                    {
                        monster.SpellRecords[spellRecord.Id] = spellRecord;
                    }
                }

                if (monster.Look.Colors.Count > 0)
                {
                    int[] colors = EntityLookManager.Instance.GetConvertedColors(monster.Look.Colors);
                    monster.Look.SetColors(colors);
                }
            }
        }

        /*
           public List<MonsterGrade> grades;
           public List<MonsterDrop> drops;
           public List<MonsterDrop> temporisDrops;
           public bool isMiniBoss;
           public bool isQuestMonster;
           public uint correspondingMiniBossId;
           public bool canBePushed;
           public bool canBeCarried;
           public bool canUsePortal;
           public bool canSwitchPos;
           public bool fastAnimsFun;
           public List<uint> incompatibleIdols;
           public bool allIdolsDisabled;
           public bool dareAvailable;
           public List<uint> incompatibleChallenges;
           public bool useRaceValues;
           public int aggressiveZoneSize;
           public int aggressiveLevelDiff;
           public string aggressiveImmunityCriterion;
           public int aggressiveAttackDelay; */
        public MonsterGrade GetGrade(byte gradeId)
        {
            return Grades.FirstOrDefault(x => x.GradeId == gradeId);
        }
        public MonsterGrade RandomGrade()
        {
            return Grades.Random();
        }

        public static MonsterRecord GetMonsterRecord(short monsterId)
        {
            if (Monsters.ContainsKey(monsterId))
            {
                return Monsters[monsterId];
            }
            else
            {
                return null;
            }
        }
        public static IEnumerable<MonsterRecord> GetMonsterRecords()
        {
            return Monsters.Values;
        }

        public override string ToString()
        {
            return "{" + Id + "} " + Name;
        }
    }
}
