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
    [D2OClass("SpellVariant")]
    [Table("spellvariants")]
    public class SpellVariantRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, SpellVariantRecord> SpellVariants = new ConcurrentDictionary<long, SpellVariantRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("breedId")]
        public byte BreedId
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("spellIds")]
        public short[] SpellIds
        {
            get;
            set;
        }
        public static short GetVariant(short spellId)
        {
            var spell = SpellVariants.FirstOrDefault(x => x.Value.SpellIds[0] == spellId);

            if (spell.Value == null)
            {
                var spell2 = SpellVariants.FirstOrDefault(x => x.Value.SpellIds[1] == spellId);

                if (spell2.Value == null)
                {
                    return -1;
                }
                return spell2.Value.SpellIds[0];
            }
            else
            {
                return spell.Value.SpellIds[1];
            }
        }
        public static IEnumerable<SpellVariantRecord> GetSpellVariantRecords()
        {
            return SpellVariants.Values;
        }
    }
}
