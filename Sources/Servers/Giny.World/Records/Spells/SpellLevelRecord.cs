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
    [Table("spell_levels")]
    public class SpellLevelRecord : IRecord
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
        public long CriticalHitProbability
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
        public long MaxCastPerTurn
        {
            get;
            set;
        }
        [D2OField("maxCastPerTarget")]
        public int MaxCastPerTarget
        {
            get;
            set;
        }
        [D2OField("minCastInterval")]
        public int MinCastInterval
        {
            get;
            set;
        }
        [D2OField("initialCooldown")]
        public long InitialCooldown // long -> ankama wtf stuff
        {
            get;
            set;
        }
        [D2OField("globalCooldown")]
        public int GlobalCooldown
        {
            get;
            set;
        }
        [D2OField("minPlayerLevel")]
        public int MinPlayerLevel
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
        [D2OField("statesCriterion")]
        public string StatesCriterion
        {
            get;
            set;
        }
        [Ignore]
        public int[] StatesRequired
        {
            get;
            set;
        } = new int[0];
        [Ignore]
        public int[] StatesAuthorized
        {
            get;
            set;
        } = new int[0];
        [Ignore]
        public int[] StatesForbidden
        {
            get;
            set;
        } = new int[0];
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


        public static List<SpellLevelRecord> GetLevelsCastingSpell(int targetSpellId, byte? grade = null)
        {
            List<SpellLevelRecord> results = new List<SpellLevelRecord>();

            foreach (var level in GetSpellLevels())
            {
                foreach (var effect in level.Effects.OfType<EffectDice>())
                {
                    if (grade.HasValue)
                    {
                        if (effect.IsSpellCastEffect() && effect.Min == targetSpellId && effect.Max == grade && !results.Contains(level))
                        {
                            results.Add(level);
                        }
                    }
                    else
                    {
                        if (effect.IsSpellCastEffect() && effect.Min == targetSpellId && !results.Contains(level))
                        {
                            results.Add(level);
                        }
                    }

                }
            }

            return results;
        }

        public static IEnumerable<SpellLevelRecord> GetSpellLevels()
        {
            return SpellsLevels.Values;
        }
    }
}
