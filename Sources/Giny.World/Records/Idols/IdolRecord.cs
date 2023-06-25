using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Idols;
using Giny.World.Records.Items;
using Giny.World.Records.Spells;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Idols
{
    [D2OClass("Idol")]
    [Table("idols")]
    public class IdolRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, IdolRecord> Idols = new ConcurrentDictionary<long, IdolRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [D2OField("itemId")]
        public long ItemId
        {
            get;
            set;
        }

        [D2OField("categoryId")]
        public int CategoryId
        {
            get;
            set;
        }

        [D2OField("groupOnly")]
        public bool GroupOnly
        {
            get;
            set;
        }

        [D2OField("score")]
        public int Score
        {
            get;
            set;
        }

        [D2OField("experienceBonus")]
        public int ExperienceBonus
        {
            get;
            set;
        }

        [D2OField("dropBonus")]
        public int DropBonus
        {
            get;
            set;
        }

        [D2OField("synergyIdolsIds")]
        public List<int> SynergyIdolsIds
        {
            get;
            set;
        }

        [D2OField("synergyIdolsCoeff")]
        public List<double> SynergyIdolsCoeff
        {
            get;
            set;
        }

        [D2OField("incompatibleMonsters")]
        public List<int> IncompatibleMonsters
        {
            get;
            set;
        }

        [Update]
        public long SpellLevelId
        {
            get;
            set;
        } = -1;

        [Ignore]
        public Spell Spell
        {
            get;
            set;
        }



        public Idol GetIdol()
        {
            return new Idol()
            {
                dropBonusPercent = (short)DropBonus,
                id = (short)Id,
                xpBonusPercent = (short)ExperienceBonus,
            };
        }


        public PartyIdol GetPartyIdol(long ownerId)
        {
            return new PartyIdol()
            {
                dropBonusPercent = (short)DropBonus,
                id = (short)Id,
                xpBonusPercent = (short)ExperienceBonus,
                ownersIds = new long[] { ownerId }
            };
        }

        [StartupInvoke("Idols bindings", StartupInvokePriority.SixthPath)]
        public static void Initialize()
        {
            foreach (var idol in Idols.Values)
            {
                SpellLevelRecord spellLevel = SpellLevelRecord.GetSpellLevel(idol.SpellLevelId);

                if (spellLevel != null)
                {
                    idol.Spell = new Spell(SpellRecord.GetSpellRecord(spellLevel.SpellId), spellLevel);
                }
                else
                {
                    ItemRecord record = ItemRecord.GetItem(idol.ItemId);
                    Logger.Write("Unknown spell level for idol : " + record.Name, Channels.Warning);
                }
            }
        }

        public static IdolRecord GetIdolRecordByItem(long itemId)
        {
            return Idols.Values.FirstOrDefault(x => x.ItemId == itemId);
        }
        public static IdolRecord GetIdolRecord(short idolId)
        {
            return Idols.TryGetValue(idolId);
        }

        public static IEnumerable<IdolRecord> GetIdolRecords()
        {
            return Idols.Values;
        }

    }
}
