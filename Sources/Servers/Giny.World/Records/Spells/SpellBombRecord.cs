using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Spells
{
    [D2OClass("SpellBomb")]
    [Table("spell_bombs")]
    public class SpellBombRecord : IRecord
    {
        [Container]
        private static Dictionary<long, SpellBombRecord> SpellBombs = new Dictionary<long, SpellBombRecord>();


        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }

        [D2OField("chainReactionSpellId")]
        public short ChainReactionSpellId
        {
            get;
            set;
        }
        [D2OField("explodSpellId")]
        public short ExplodSpellId
        {
            get;
            set;
        }
        [D2OField("wallId")]
        public byte WallId
        {
            get;
            set;
        }
        [D2OField("instantSpellId")]
        public short InstantSpellId
        {
            get;
            set;
        }
        [D2OField("comboCoeff")]
        public double ComboCoeff
        {
            get;
            set;
        }

        public static SpellBombRecord? GetSpellBomb(long id)
        {
            if (SpellBombs.ContainsKey(id))
            {
                return SpellBombs[id];
            }
            else
            {
                return null;
            }
        }
    }
}
