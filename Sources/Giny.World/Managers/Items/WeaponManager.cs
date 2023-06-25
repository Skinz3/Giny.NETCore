using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Records.Items;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items
{
    public class WeaponManager : Singleton<WeaponManager>
    {
        public const string WeaponTargetMask = "A,g";

        private readonly EffectsEnum[] WeaponEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_DamageWater,
            EffectsEnum.Effect_DamageEarth,
            EffectsEnum.Effect_DamageAir,
            EffectsEnum.Effect_DamageFire,
            EffectsEnum.Effect_DamageNeutral,
            EffectsEnum.Effect_StealHPWater,
            EffectsEnum.Effect_StealHPEarth,
            EffectsEnum.Effect_StealHPAir,
            EffectsEnum.Effect_StealHPFire,
            EffectsEnum.Effect_StealHPNeutral,
            EffectsEnum.Effect_RemoveAP,
            EffectsEnum.Effect_RemainingFights,
            EffectsEnum.Effect_StealKamas,
            EffectsEnum.Effect_HealHP_108,
        };

        private readonly EffectsEnum[] WeaponBoostableEffects = new EffectsEnum[]
        {
            EffectsEnum.Effect_DamageWater,
            EffectsEnum.Effect_DamageEarth,
            EffectsEnum.Effect_DamageAir,
            EffectsEnum.Effect_DamageFire,
            EffectsEnum.Effect_DamageNeutral,
            EffectsEnum.Effect_StealHPWater,
            EffectsEnum.Effect_StealHPEarth,
            EffectsEnum.Effect_StealHPAir,
            EffectsEnum.Effect_StealHPFire,
            EffectsEnum.Effect_StealHPNeutral,
            EffectsEnum.Effect_HealHP_108,
        };

        private readonly Dictionary<ItemTypeEnum, string> RawZones = new Dictionary<ItemTypeEnum, string>()
        {
            { ItemTypeEnum.BOW, "P1" },
            { ItemTypeEnum.WAND, "P1"},
            { ItemTypeEnum.STAFF, "T1" },
            { ItemTypeEnum.DAGGER, "P1" },
            { ItemTypeEnum.SWORD, "P1" },
            { ItemTypeEnum.HAMMER, "C1" },
            { ItemTypeEnum.SHOVEL, "L1" },
            { ItemTypeEnum.AXE, "P1" },
            { ItemTypeEnum.SCYTHE, "U1" },

        };
        public const short PunchSpellId = 0;

        public const string PunchRawZone = "P1";

        public SpellRecord PunchSpellRecord
        {
            get;
            private set;
        }

        [StartupInvoke("Weapon manager", StartupInvokePriority.Last)]
        public void Initialize()
        {
            PunchSpellRecord = SpellRecord.GetSpellRecord(PunchSpellId);

            foreach (var effect in PunchSpellRecord.Levels.Last().Effects)
            {
                effect.RawZone = PunchRawZone;
                effect.TargetMask = WeaponTargetMask;
                effect.RawTriggers = "I";
            }
            foreach (var effect in PunchSpellRecord.Levels.Last().CriticalEffects)
            {
                effect.RawZone = PunchRawZone;
                effect.TargetMask = WeaponTargetMask;
                effect.RawTriggers = "I";
            }
        }


        public SpellLevelRecord CreateWeaponSpellLevel(WeaponRecord weapon, CharacterItemRecord characterItem)
        {
            List<Effect> effects = characterItem.Effects.Select(x => (Effect)x.Clone()).ToList();

            effects.RemoveAll(x => !WeaponEffects.Contains(x.EffectEnum));

            foreach (var effect in effects)
            {
                string rawZone = PunchRawZone;

                if (RawZones.ContainsKey(weapon.TypeEnum))
                {
                    rawZone = RawZones[weapon.TypeEnum];
                }
                effect.RawZone = rawZone;
                effect.TargetMask = WeaponTargetMask;
                effect.RawTriggers = "I";
            }

            IEnumerable<Effect> criticalEffects = GetCriticalEffects(weapon, effects);

            return new SpellLevelRecord()
            {
                Id = -1,
                ApCost = weapon.ApCost,

                CastInDiagonal = weapon.CastInDiagonal,
                CastInLine = weapon.CastInLine,
                CastTestLos = weapon.CastTestLos,
                AdditionalEffectsZones = new List<string>(),
                CriticalEffects = new EffectCollection(criticalEffects),
                MaxCastPerTurn = weapon.MaxCastPerTurn,
                CriticalHitProbability = weapon.CriticalHitProbability,
                Effects = new EffectCollection(effects),
                GlobalCooldown = 0,
                Grade = 1,
                Hidden = false,
                HideEffects = false,
                InitialCooldown = 0,
                MaxCastPerTarget = 0,
                MaxRange = weapon.MaxRange,
                MaxStack = 0,
                MinCastInterval = 0,
                MinPlayerLevel = 0,
                MinRange = 0,
                NeedFreeCell = false,
                NeedFreeTrapCell = false,
                NeedTakenCell = false,
                RangeCanBeBoosted = false,
                SpellBreed = 0,
                SpellId = 0,
                StatesAuthorized = new int[0],
                StatesForbidden = new int[0], // affaiblie
                StatesRequired = new int[0]
            };
        }

        private IEnumerable<Effect> GetCriticalEffects(WeaponRecord weapon, List<Effect> effects)
        {
            List<Effect> results = new List<Effect>();

            foreach (EffectDice effect in effects.FindAll(x => WeaponBoostableEffects.Contains(x.EffectEnum)))
            {
                EffectDice newEffect = (EffectDice)effect.Clone();
                newEffect.Min += weapon.CriticalHitBonus;
                newEffect.Max += weapon.CriticalHitBonus;
                results.Add(newEffect);
            }
            return results;
        }
    }
}
