using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Formulas;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Breeds;
using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class EntityStats
    {
        public const short BaseSummonsCount = 1;


        private int m_lifePoints;

        public int LifePoints
        {
            get
            {
                return m_lifePoints;
            }
            set
            {
                m_lifePoints = value;
            }
        }

        [Annotation("formule inexact (arrondi)")]
        public double LifePercentage => (LifePoints / (double)MaxLifePoints) * 100;


        [ProtoMember(1)]
        public int MaxLifePoints
        {
            get;
            set;
        }

        public int MissingLife
        {
            get
            {
                return MaxLifePoints - LifePoints;
            }
        }
        [ProtoMember(2)]
        public short MaxEnergyPoints
        {
            get;
            set;
        }
        /// <summary>
        /// Deprecated ? 
        /// </summary>
        [ProtoMember(3)]
        public short CriticalHitWeapon
        {
            get;
            set;
        }
        /// <summary>
        /// Deprecated ? 
        /// </summary>
        [ProtoMember(4)]
        public short GlobalDamageReduction
        {
            get;
            set;
        }
        [ProtoMember(5)]
        private Dictionary<CharacteristicEnum, Characteristic> Characteristics
        {
            get;
            set;
        } = new Dictionary<CharacteristicEnum, Characteristic>();


        public short Energy
        {
            get;
            set;
        }

        public DetailedCharacteristic Strength
        {
            get
            {
                return (DetailedCharacteristic)this[CharacteristicEnum.STRENGTH];
            }
            set
            {
                this[CharacteristicEnum.STRENGTH] = value;
            }
        }
        public DetailedCharacteristic Wisdom
        {
            get
            {
                return (DetailedCharacteristic)this[CharacteristicEnum.WISDOM];
            }
            set
            {
                this[CharacteristicEnum.WISDOM] = value;
            }
        }
        public DetailedCharacteristic Chance
        {
            get
            {
                return (DetailedCharacteristic)this[CharacteristicEnum.CHANCE];
            }
            set
            {
                this[CharacteristicEnum.CHANCE] = value;
            }
        }
        public DetailedCharacteristic Agility
        {
            get
            {
                return (DetailedCharacteristic)this[CharacteristicEnum.AGILITY];
            }
            set
            {
                this[CharacteristicEnum.AGILITY] = value;
            }
        }
        public DetailedCharacteristic Intelligence
        {
            get
            {
                return (DetailedCharacteristic)this[CharacteristicEnum.INTELLIGENCE];
            }
            set
            {
                this[CharacteristicEnum.INTELLIGENCE] = value;
            }
        }
        public UsableCharacteristic ActionPoints
        {
            get
            {
                return (UsableCharacteristic)this[CharacteristicEnum.ACTION_POINTS];
            }
            set
            {
                this[CharacteristicEnum.ACTION_POINTS] = value;
            }
        }
        public UsableCharacteristic MovementPoints
        {
            get
            {
                return (UsableCharacteristic)this[CharacteristicEnum.MOVEMENT_POINTS];
            }
            set
            {
                this[CharacteristicEnum.MOVEMENT_POINTS] = value;
            }
        }

        public void Initialize()
        {
            this.LifePoints = this.MaxLifePoints;
            this.Energy = this.MaxEnergyPoints;

            ((RelativeCharacteristic)this[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY]).Bind(Wisdom);
            ((RelativeCharacteristic)this[CharacteristicEnum.AP_REDUCTION]).Bind(Wisdom);

            ((RelativeCharacteristic)this[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY]).Bind(Wisdom);
            ((RelativeCharacteristic)this[CharacteristicEnum.MP_REDUCTION]).Bind(Wisdom);


            ((RelativeCharacteristic)this[CharacteristicEnum.TACKLE_BLOCK]).Bind(Agility);
            ((RelativeCharacteristic)this[CharacteristicEnum.TACKLE_EVADE]).Bind(Agility);

            ((RelativeCharacteristic)this[CharacteristicEnum.MAGIC_FIND]).Bind(Chance);

            ((InitiativeCharacteristic)this[CharacteristicEnum.INITIATIVE]).Bind(this);

        }
        public Characteristic GetCharacteristic(StatsBoostEnum statId)
        {
            switch (statId)
            {
                case StatsBoostEnum.STRENGTH:
                    return Strength;
                case StatsBoostEnum.VITALITY:
                    return this[CharacteristicEnum.VITALITY];
                case StatsBoostEnum.WISDOM:
                    return Wisdom;
                case StatsBoostEnum.CHANCE:
                    return Chance;
                case StatsBoostEnum.AGILITY:
                    return Agility;
                case StatsBoostEnum.INTELLIGENCE:
                    return Intelligence;
            }

            throw new Exception("Unknown statId " + statId);
        }

        public int Total()
        {
            return Strength.Total() + Chance.Total() + Intelligence.Total() + Agility.Total();
        }


        public CharacterCharacteristic[] GetCharacterCharacteristics(CharacteristicEnum? characteristicEnum = null)
        {

            if (characteristicEnum.HasValue)
            {
                if (characteristicEnum == CharacteristicEnum.HIT_POINTS)
                {
                    return new CharacterCharacteristic[] { new CharacterCharacteristicValue(GetHitPoints(), (short)CharacteristicEnum.HIT_POINTS) };
                }
                else if (characteristicEnum == CharacteristicEnum.HIT_POINT_LOSS)
                {
                    return new CharacterCharacteristic[] { new CharacterCharacteristicValue(GetMissingLife(), (short)CharacteristicEnum.HIT_POINT_LOSS) };
                }
                var characterCharateristic = this.GetCharacteristic<Characteristic>(characteristicEnum.Value).GetCharacterCharacteristic(characteristicEnum.Value);
                return new CharacterCharacteristic[] { characterCharateristic };
            }
            else
            {
                return GetCharacterCharacteristics();
            }

        }

        private CharacterCharacteristic[] GetCharacterCharacteristics()
        {
            List<CharacterCharacteristic> results = new List<CharacterCharacteristic>();

            foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in this.GetCharacteristics<Characteristic>())
            {
                var characterCharacteristic = stat.Value.GetCharacterCharacteristic(stat.Key);
                results.Add(characterCharacteristic);
            }


            results.Add(new CharacterCharacteristicValue(GetHitPoints(), (short)CharacteristicEnum.HIT_POINTS));
            results.Add(new CharacterCharacteristicValue(GetMissingLife(), (short)CharacteristicEnum.HIT_POINT_LOSS));

            results.Add(new CharacterCharacteristicValue(MaxEnergyPoints, (short)CharacteristicEnum.MAX_ENERGY_POINTS));
            results.Add(new CharacterCharacteristicValue(Energy, (short)CharacteristicEnum.ENERGY_POINTS));


            results.Add(new CharacterCharacteristicDetailed(100, 0, 0, 0, 0, 143)); // wtf ankama, for client, 143 is dealt heal multiplier.

            return results.ToArray();
        }
        public virtual int GetMissingLife()
        {
            return -MissingLife;
        }
        public virtual int GetHitPoints()
        {
            return MaxLifePoints - this[CharacteristicEnum.VITALITY].TotalInContext();
        }
        public CharacterCharacteristicsInformations GetCharacterCharacteristicsInformations(Character character)
        {
            var alignementInfos = new ActorExtendedAlignmentInformations(0, 0, 0, 0, 0, 0, 0, 0);

            return new CharacterCharacteristicsInformations()
            {
                alignmentInfos = alignementInfos,
                experienceBonusLimit = 0,
                characteristics = GetCharacterCharacteristics(),
                probationTime = 0,
                spellModifiers = new SpellModifierMessage[0],
                criticalHitWeapon = CriticalHitWeapon,
                experience = character.Record.Experience,
                kamas = character.Record.Kamas,
                experienceLevelFloor = character.LowerBoundExperience,
                experienceNextLevelFloor = character.UpperBoundExperience,
            };
        }
        public Characteristic this[CharacteristicEnum characteristicEnum]
        {
            get
            {
                return this.Characteristics[characteristicEnum];
            }
            set
            {
                if (!this.Characteristics.ContainsKey(characteristicEnum))
                {
                    this.Characteristics.Add(characteristicEnum, value);
                }
                else
                {
                    this.Characteristics[characteristicEnum] = value;
                }
            }
        }
        public static EntityStats New(short level)
        {
            var stats = new EntityStats()
            {
                LifePoints = BreedManager.BreedDefaultLife,
                MaxLifePoints = BreedManager.BreedDefaultLife,
                MaxEnergyPoints = (short)(level * 100),
                Energy = (short)(level * 100),
                CriticalHitWeapon = 0,
            };

            stats[CharacteristicEnum.INITIATIVE] = InitiativeCharacteristic.Zero();
            stats[CharacteristicEnum.STATS_POINTS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.ACTION_POINTS] = ApCharacteristic.New(ConfigFile.Instance.StartAp);
            stats[CharacteristicEnum.MOVEMENT_POINTS] = MpCharacteristic.New(ConfigFile.Instance.StartMp);
            stats[CharacteristicEnum.AGILITY] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.AIR_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.ALL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.DAMAGE_PERCENT] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.CHANCE] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_HIT] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY] = PointDodgeCharacteristic.Zero();
            stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY] = PointDodgeCharacteristic.Zero();
            stats[CharacteristicEnum.EARTH_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.FIRE_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.GLYPH_POWER] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.RUNE_POWER] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.HEAL_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.INTELLIGENCE] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.MAGIC_FIND] = RelativeCharacteristic.New(BreedManager.BreedDefaultProspecting);
            stats[CharacteristicEnum.PUSH_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.PUSH_DAMAGE_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.RANGE] = RangeCharacteristic.Zero();
            stats[CharacteristicEnum.REFLECT_DAMAGE] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.STRENGTH] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.MAX_SUMMONED_CREATURES_BOOST] = DetailedCharacteristic.New(BaseSummonsCount);
            stats[CharacteristicEnum.TRAP_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.VITALITY] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.WATER_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.WEAPON_POWER] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.WISDOM] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.TACKLE_BLOCK] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.TACKLE_EVADE] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.AP_REDUCTION] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.MP_REDUCTION] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER] = DetailedCharacteristic.New(100);
            stats[CharacteristicEnum.WEIGHT] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.DAMAGE_PERCENT_SPELL] = DetailedCharacteristic.Zero();
            stats[CharacteristicEnum.SHIELD] = Characteristic.Zero();
            stats[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT] = ErosionCharacteristic.New(FighterStats.NaturalErosion);
            stats.Initialize();

            return stats;
        }

        public T GetCharacteristic<T>(CharacteristicEnum characteristicEnum) where T : Characteristic
        {
            return Characteristics[characteristicEnum] as T;
        }
        public EffectSchoolEnum GetBestElement()
        {
            Dictionary<EffectSchoolEnum, Characteristic> values = new Dictionary<EffectSchoolEnum, Characteristic>
            {
                { EffectSchoolEnum.Earth, Strength },
                { EffectSchoolEnum.Fire, Intelligence },
                { EffectSchoolEnum.Air,  Agility },
                { EffectSchoolEnum.Water,Chance },
            };

            EffectSchoolEnum result = values.OrderByDescending(x => x.Value.TotalInContext()).First().Key; // context of context free ?

            return result;
        }

        public Dictionary<CharacteristicEnum, T> GetCharacteristics<T>() where T : Characteristic
        {
            Dictionary<CharacteristicEnum, T> results = new Dictionary<CharacteristicEnum, T>();

            foreach (var characteristic in Characteristics)
            {
                if (characteristic.Value is T)
                {
                    results.Add(characteristic.Key, (T)characteristic.Value);
                }
            }
            return results;
        }

    }
}
