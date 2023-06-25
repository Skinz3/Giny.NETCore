using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Spells
{
    [D2OClass("SpellLevel")]
    [Table("spellslevels")]
    public class SpellLevelRecord : ITable
    {
        [Container]
        private static Dictionary<long, SpellLevelRecord> SpellsLevels = new Dictionary<long, SpellLevelRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("spellId")]
        public short SpellId
        {
            get;
            set;
        }
        [D2OField("grade")]
        public byte Grade
        {
            get;
            set;
        }
        [D2OField("spellBreed")]
        public short SpellBreed
        {
            get;
            set;
        }
        [D2OField("apCost")]
        public short ApCost
        {
            get;
            set;
        }
        [D2OField("minRange")]
        public short MinRange
        {
            get;
            set;
        }
        [D2OField("range")]
        public short MaxRange
        {
            get;
            set;
        }
        [D2OField("castInLine")]
        public bool CastInLine
        {
            get;
            set;
        }
        [D2OField("castInDiagonal")]
        public bool CastInDiagonal
        {
            get;
            set;
        }
        [D2OField("castTestLos")]
        public bool CastTestLos
        {
            get;
            set;
        }
        [D2OField("criticalHitProbability")]
        public short CriticalHitProbability
        {
            get;
            set;
        }
        [D2OField("needFreeCell")]
        public bool NeedFreeCell
        {
            get;
            set;
        }
        [D2OField("needTakenCell")]
        public bool NeedTakenCell
        {
            get;
            set;
        }
        [D2OField("needFreeTrapCell")]
        public bool NeedFreeTrapCell
        {
            get;
            set;
        }
        [D2OField("rangeCanBeBoosted")]
        public bool RangeCanBeBoosted
        {
            get;
            set;
        }
        [D2OField("maxStack")]
        public int MaxStack
        {
            get;
            set;
        }
        [D2OField("maxCastPerTurn")]
        public short MaxCastPerTurn
        {
            get;
            set;
        }
        [D2OField("maxCastPerTarget")]
        public short MaxCastPerTarget
        {
            get;
            set;
        }
        [D2OField("minCastInterval")]
        public short MinCastInterval
        {
            get;
            set;
        }
        [D2OField("initialCooldown")]
        public long InitialCooldown
        {
            get;
            set;
        }
        [D2OField("globalCooldown")]
        public short GlobalCooldown
        {
            get;
            set;
        }
        [D2OField("minPlayerLevel")]
        public short MinPlayerLevel
        {
            get;
            set;
        }
        [D2OField("hideEffects")]
        public bool HideEffects
        {
            get;
            set;
        }
        [D2OField("hidden")]
        public bool Hidden
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("statesRequired")]
        public int[] StatesRequired
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("statesAuthorized")]
        public int[] StatesAuthorized
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("statesForbidden")]
        public int[] StatesForbidden
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("effects")]
        public EffectCollection Effects
        {
            get;
            set;
        }
        [ProtoSerialize]
        [D2OField("criticalEffect")]
        public EffectCollection CriticalEffects
        {
            get;
            set;
        }
        [D2OField("additionalEffectsZones")]
        public List<string> AdditionalEffectsZones
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "{" + Id + "} Grade : (" + Grade + ")";
        }
        public static SpellLevelRecord GetSpellLevel(long levelId)
        {
            SpellLevelRecord result = null;
            SpellsLevels.TryGetValue(levelId, out result);
            return result;
        }
        public static IEnumerable<SpellLevelRecord> GetSpellLevels()
        {
            return SpellsLevels.Values;
        }
    }
}
