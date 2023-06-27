using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Experiences;
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

        public event Action LifePointsChanged;

        private int m_lifePoints;

        public int LifePoints
        {
            get
            {
                LifePointsChanged?.Invoke();
                return m_lifePoints;
            }
            set
            {
                m_lifePoints = value;
            }
        }

        [WIP("formule inexact (arrondi)")]
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
        public int TotalInitiative
        {
            get
            {
                return StatsFormulas.Instance.TotalInitiative(this);
            }
        }

        public Characteristic Strength
        {
            get
            {
                return this[CharacteristicEnum.STRENGTH];
            }
            set
            {
                this[CharacteristicEnum.STRENGTH] = value;
            }
        }
        public Characteristic Wisdom
        {
            get
            {
                return this[CharacteristicEnum.WISDOM];
            }
            set
            {
                this[CharacteristicEnum.WISDOM] = value;
            }
        }
        public Characteristic Chance
        {
            get
            {
                return this[CharacteristicEnum.CHANCE];
            }
            set
            {
                this[CharacteristicEnum.CHANCE] = value;
            }
        }
        public Characteristic Agility
        {
            get
            {
                return this[CharacteristicEnum.AGILITY];
            }
            set
            {
                this[CharacteristicEnum.AGILITY] = value;
            }
        }
        public Characteristic Intelligence
        {
            get
            {
                return this[CharacteristicEnum.INTELLIGENCE];
            }
            set
            {
                this[CharacteristicEnum.INTELLIGENCE] = value;
            }
        }
        public Characteristic ActionPoints
        {
            get
            {
                return this[CharacteristicEnum.ACTION_POINTS];
            }
            set
            {
                this[CharacteristicEnum.ACTION_POINTS] = value;
            }
        }
        public Characteristic MovementPoints
        {
            get
            {
                return this[CharacteristicEnum.MOVEMENT_POINTS];
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

        public virtual CharacterCharacteristic[] GetCharacterCharacteristics(CharacteristicEnum[] selected = null)
        {
            List<CharacterCharacteristic> results = new List<CharacterCharacteristic>();

            if (selected == null)
            {
                foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in this.GetCharacteristics())
                {
                    var characterCharacteristic = stat.Value.GetCharacterCharacteristicDetailed(stat.Key);


                    results.Add(characterCharacteristic);
                }
            }
            else
            {
                foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in this.GetCharacteristics().Where(x => selected.Contains(x.Key)))
                {
                    var characterCharacteristic = stat.Value.GetCharacterCharacteristicDetailed(stat.Key);
                    results.Add(characterCharacteristic);
                }
            }



            var c = -Math.Abs(MaxLifePoints - LifePoints);
            //results.Add(new CharacterCharacteristicValue(MaxLifePoints, (short)CharacteristicEnum.MAX_LIFE_POINTS));
            results.Add(new CharacterCharacteristicValue(MaxLifePoints, (short)CharacteristicEnum.HIT_POINTS));

            //results.Add(new CharacterCharacteristicValue(300, (short)CharacteristicEnum.SHIELD)); // kinda weird


            results.Add(new CharacterCharacteristicValue(c, (short)CharacteristicEnum.HIT_POINT_LOSS));
            results.Add(new CharacterCharacteristicValue(MaxEnergyPoints, (short)CharacteristicEnum.MAX_ENERGY_POINTS));
            results.Add(new CharacterCharacteristicValue(Energy, (short)CharacteristicEnum.ENERGY_POINTS));


            return results.ToArray();
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
                spellModifications = new CharacterSpellModification[0],
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
        public static EntityStats New(short level, byte breedId)
        {
            var stats = new EntityStats()
            {
                LifePoints = BreedManager.BreedDefaultLife,
                MaxLifePoints = BreedManager.BreedDefaultLife,
                MaxEnergyPoints = (short)(level * 100),
                Energy = (short)(level * 100),
                CriticalHitWeapon = 0,
            };


            stats[CharacteristicEnum.STATS_POINTS] = Characteristic.Zero();
            stats[CharacteristicEnum.ACTION_POINTS] = ApCharacteristic.New(ConfigFile.Instance.StartAp);
            stats[CharacteristicEnum.MOVEMENT_POINTS] = MpCharacteristic.New(ConfigFile.Instance.StartMp);
            stats[CharacteristicEnum.AGILITY] = Characteristic.Zero();
            stats[CharacteristicEnum.AIR_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.AIR_ELEMENT_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.ALL_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.DAMAGE_PERCENT] = Characteristic.Zero();
            stats[CharacteristicEnum.CHANCE] = Characteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.CRITICAL_HIT] = Characteristic.Zero();
            stats[CharacteristicEnum.INITIATIVE] = Characteristic.Zero();
            stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY] = PointDodgeCharacteristic.Zero();
            stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY] = PointDodgeCharacteristic.Zero();
            stats[CharacteristicEnum.EARTH_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.EARTH_ELEMENT_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.FIRE_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.FIRE_ELEMENT_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.GLYPH_POWER] = Characteristic.Zero();
            stats[CharacteristicEnum.RUNE_POWER] = Characteristic.Zero();
            stats[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT] = Characteristic.Zero();
            stats[CharacteristicEnum.HEAL_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.INTELLIGENCE] = Characteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.MAGIC_FIND] = RelativeCharacteristic.New(BreedManager.BreedDefaultProspecting);
            stats[CharacteristicEnum.PUSH_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.PUSH_DAMAGE_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.RANGE] = RangeCharacteristic.Zero();
            stats[CharacteristicEnum.REFLECT_DAMAGE] = Characteristic.Zero();
            stats[CharacteristicEnum.STRENGTH] = Characteristic.Zero();
            stats[CharacteristicEnum.MAX_SUMMONED_CREATURES_BOOST] = Characteristic.New(BaseSummonsCount);
            stats[CharacteristicEnum.TRAP_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT] = Characteristic.Zero();
            stats[CharacteristicEnum.VITALITY] = Characteristic.Zero();
            stats[CharacteristicEnum.WATER_DAMAGE_BONUS] = Characteristic.Zero();
            stats[CharacteristicEnum.WATER_ELEMENT_REDUCTION] = Characteristic.Zero();
            stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.Zero();
            stats[CharacteristicEnum.WEAPON_DAMAGE_PERCENT] = Characteristic.Zero();
            stats[CharacteristicEnum.WISDOM] = Characteristic.Zero();
            stats[CharacteristicEnum.TACKLE_BLOCK] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.TACKLE_EVADE] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.AP_REDUCTION] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.MP_REDUCTION] = RelativeCharacteristic.Zero();
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE] = Characteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE] = Characteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON] = Characteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON] = Characteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE] = Characteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE] = Characteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS] = Characteristic.New(100);
            stats[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS] = Characteristic.New(100);
            stats[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER] = Characteristic.New(100);
            stats[CharacteristicEnum.WEIGHT] = Characteristic.Zero();
            stats[CharacteristicEnum.DAMAGE_PERCENT_SPELL] = Characteristic.Zero();
            stats.Initialize();

            return stats;
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

        public Dictionary<CharacteristicEnum, Characteristic> GetCharacteristics()
        {
            return Characteristics;
        }

    }
}
