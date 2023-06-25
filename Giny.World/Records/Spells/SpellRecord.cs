using Giny.Core.DesignPattern;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Spells
{
    [D2OClass("Spell")]
    [Table("spells")]
    public class SpellRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, SpellRecord> Spells = new ConcurrentDictionary<long, SpellRecord>();

        long ITable.Id => Id;

        [D2OField("id")]
        [Primary]
        public short Id
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

        [TypeOverride("mediumtext")]
        [I18NField]
        [D2OField("descriptionId")]
        public string Description
        {
            get;
            set;
        }
        [D2OField("spellLevels")]
        [ProtoSerialize]
        public int[] SpellLevels
        {
            get;
            set;
        }
        [D2OField("verbose_cast")]
        public bool Verbose
        {
            get;
            set;
        }

        [Ignore]
        public SpellRecord VariantRecord
        {
            get;
            set;
        }
        [Ignore]
        public List<SpellLevelRecord> Levels
        {
            get;
            set;
        }
        [Ignore]
        public short MinimumLevel
        {
            get
            {
                return Levels.Min(x => x.MinPlayerLevel);
            }
        }

        [Update]
        public SpellCategoryEnum Category
        {
            get;
            set;
        } = SpellCategoryEnum.None;

        [StartupInvoke("Spells bindings", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var spell in Spells.Values)
            {
                var variantSpellId = SpellVariantRecord.GetVariant(spell.Id);
                if (variantSpellId != -1)
                    spell.VariantRecord = GetSpellRecord(variantSpellId);

                spell.Levels = new List<SpellLevelRecord>();

                foreach (var levelId in spell.SpellLevels)
                {
                    SpellLevelRecord level = SpellLevelRecord.GetSpellLevel(levelId);
                    spell.Levels.Add(level);
                }
            }
        }
        public override string ToString()
        {
            return string.Format("({0}) {1}", Id, Name);
        }
        public SpellLevelRecord GetLevel(byte grade)
        {
            if (grade == 0)
            {
                return Levels.Last();
            }
            if (grade <= Levels.Count)
            {
                return Levels[grade - 1];
            }
            else
            {
                return Levels.Last();
            }
        }

        public static IEnumerable<SpellRecord> GetSpellRecords()
        {
            return Spells.Values;
        }
        public static SpellRecord GetSpellRecord(short spellId)
        {
            SpellRecord result = null;
            Spells.TryGetValue(spellId, out result);
            return result;
        }
        public static SpellRecord GetSpellRecord(string name)
        {
            return Spells.Values.FirstOrDefault(x => x.Name == name);
        }
    }
}
