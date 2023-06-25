using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Fights.Cast;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Spells
{
    [D2OClass("SpellState")]
    [Table("spellStates")]
    public class SpellStateRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, SpellStateRecord> SpellStates = new ConcurrentDictionary<long, SpellStateRecord>();

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
        [D2OField("preventsSpellCast")]
        public bool PreventsSpellCast
        {
            get;
            set;
        }
        [D2OField("preventsFight")]
        public bool PreventsFight
        {
            get;
            set;
        }
        [D2OField("isSilent")]
        public bool IsSilent
        {
            get;
            set;
        }
        [D2OField("cantDealDamage")]
        public bool CantDealDamage
        {
            get;
            set;
        }
        [D2OField("invulnerable")]
        public bool Invulnerable
        {
            get;
            set;
        }
        [D2OField("invulnerable")]
        public bool Incurable
        {
            get;
            set;
        }
        [D2OField("cantBeMoved")]
        public bool CantBeMoved
        {
            get;
            set;
        }
        [D2OField("cantBePushed")]
        public bool CantBePushed
        {
            get;
            set;
        }
        [D2OField("cantSwitchPosition")]
        public bool CantSwitchPosition
        {
            get;
            set;
        }
        [D2OField("invulnerableMelee")]
        public bool InvulnerableMelee
        {
            get;
            set;
        }
        [D2OField("invulnerableRange")]
        public bool InvulnerableRange
        {
            get;
            set;
        }
        [D2OField("cantTackle")]
        public bool CantTackle
        {
            get;
            set;
        }
        [D2OField("cantBeTackled")]
        public bool CantBeTackled
        {
            get;
            set;
        }

        public static SpellStateRecord GetSpellStateRecord(long id)
        {
            return SpellStates[id];
        }

        public override string ToString()
        {
            return "{" + Id + "} " + Name;
        }
    }
}
