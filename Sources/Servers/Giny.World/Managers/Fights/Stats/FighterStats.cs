using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Breeds;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Effects.Summons;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Stats;
using Giny.World.Records.Characters;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Stats
{
    public class FighterStats : EntityStats
    {
        public const short NaturalErosion = 10;

        public const short MaxErosion = 50;


        public GameActionFightInvisibilityStateEnum InvisibilityState
        {
            get;
            set;
        }

        public Characteristic Erosion => this[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT];

        public int ShieldPoints => this[CharacteristicEnum.SHIELD].TotalInContext();
        public short DamageMultiplier
        {
            get;
            set;
        }

        public void SetShield(int delta)
        {
            if (delta >= 0)
            {
                this[CharacteristicEnum.SHIELD].Context = delta;
            }
            else
            {
                return;
                throw new Exception("Invalid shield value.");
            }
        }
        /// <summary>
        /// ! Dont change order ! 
        /// Import because of MovementPoints.OnContextChanged (notify client)
        /// needs good Used value
        /// </summary>
        public void ResetUsedPoints()
        {
            var mpUsed = MovementPoints.Used;
            var apUsed = ActionPoints.Used;

            MovementPoints.Used = 0;
            ActionPoints.Used = 0;
            MovementPoints.Context += mpUsed;
            ActionPoints.Context += apUsed;


        }
        /// <summary>
        /// ! Dont change order ! 
        /// Import because of MovementPoints.OnContextChanged (notify client)
        /// needs good Used value
        /// </summary>
        public void GainAp(short amount)
        {
            ActionPoints.Used -= amount;
            ActionPoints.Context += amount;

        }
        /// <summary>
        /// ! Dont change order ! 
        /// Import because of MovementPoints.OnContextChanged (notify client)
        /// needs good Used value
        /// </summary>
        public void GainMp(short amount)
        {
            MovementPoints.Used -= amount;
            MovementPoints.Context += amount;
        }
        /// <summary>
        /// ! Dont change order ! 
        /// Import because of MovementPoints.OnContextChanged (notify client)
        /// needs good Used value
        /// </summary>
        public void UseMp(short amount)
        {
            MovementPoints.Used += amount;
            MovementPoints.Context -= amount;
        }
        /// <summary>
        /// ! Dont change order ! 
        /// Import because of MovementPoints.OnContextChanged (notify client)
        /// needs good Used value
        /// </summary>
        public void UseAp(short amount)
        {
            ActionPoints.Used += amount;
            ActionPoints.Context -= amount;
        }

        public FighterStats(Character character)
        {
            foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in character.Stats.GetCharacteristics<Characteristic>())
            {
                this[stat.Key] = stat.Value.Clone();
            }

            this.CriticalHitWeapon = character.Stats.CriticalHitWeapon;
            this.Energy = character.Stats.Energy;

            this.MaxEnergyPoints = character.Stats.MaxEnergyPoints;
            InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;

            this.Initialize();

        }
        public FighterStats(FighterStats other)
        {
            foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in other.GetCharacteristics<Characteristic>())
            {
                this[stat.Key] = stat.Value.Clone();
            }

            this.CriticalHitWeapon = other.CriticalHitWeapon;
            this.Energy = other.Energy;

            this.MaxEnergyPoints = other.MaxEnergyPoints;
            this.InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        /*
         * Todo : Summoned / SummonerId
         * bonusCharacteristic.lifePoints * (caster.lifePoints - caster.vitality.total)  + (grade.lifePoints * statsCoeff)
         */
        public FighterStats(MonsterGrade monsterGrade, Fighter? summoner = null, double coeff = 1d)
        {
            this[CharacteristicEnum.HIT_POINTS] = LifeCharacteristic.New((int)(monsterGrade.LifePoints * coeff));
            this[CharacteristicEnum.INITIATIVE] = InitiativeCharacteristic.Zero();
            this[CharacteristicEnum.ACTION_POINTS] = ApCharacteristic.New(monsterGrade.ActionPoints);
            this[CharacteristicEnum.AIR_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.AIR_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.AirResistance);
            this[CharacteristicEnum.ALL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.CRITICAL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.CRITICAL_HIT] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.DAMAGE_PERCENT] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY] = PointDodgeCharacteristic.New(monsterGrade.ApDodge);
            this[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY] = PointDodgeCharacteristic.New(monsterGrade.MpDodge);
            this[CharacteristicEnum.EARTH_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.EARTH_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.EarthResistance);
            this[CharacteristicEnum.FIRE_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.FIRE_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.FireResistance);
            this[CharacteristicEnum.GLYPH_POWER] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.HEAL_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.INTELLIGENCE] = DetailedCharacteristic.New(monsterGrade.Intelligence);
            this[CharacteristicEnum.WISDOM] = DetailedCharacteristic.New((short)(monsterGrade.Wisdom * coeff));
            this[CharacteristicEnum.CHANCE] = DetailedCharacteristic.New((short)(monsterGrade.Chance * coeff));
            this[CharacteristicEnum.AGILITY] = DetailedCharacteristic.New((short)(monsterGrade.Agility * coeff));
            this[CharacteristicEnum.STRENGTH] = DetailedCharacteristic.New((short)(monsterGrade.Strength * coeff));
            this[CharacteristicEnum.VITALITY] = DetailedCharacteristic.New((short)(monsterGrade.Vitality * coeff));
            this[CharacteristicEnum.MOVEMENT_POINTS] = MpCharacteristic.New(monsterGrade.MovementPoints);
            this[CharacteristicEnum.NEUTRAL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.NeutralResistance);
            this[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.AP_REDUCTION] = RelativeCharacteristic.Zero();
            this[CharacteristicEnum.MP_REDUCTION] = RelativeCharacteristic.Zero();
            this[CharacteristicEnum.MAGIC_FIND] = RelativeCharacteristic.Zero();
            this[CharacteristicEnum.PUSH_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.PUSH_DAMAGE_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.RANGE] = RangeCharacteristic.Zero();
            this[CharacteristicEnum.REFLECT_DAMAGE] = DetailedCharacteristic.New(monsterGrade.DamageReflect);
            this[CharacteristicEnum.RUNE_POWER] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.MAX_SUMMONED_CREATURES_BOOST] = Characteristic.New(BaseSummonsCount);
            this[CharacteristicEnum.TACKLE_BLOCK] = RelativeCharacteristic.Zero();
            this[CharacteristicEnum.TACKLE_EVADE] = RelativeCharacteristic.Zero();
            this[CharacteristicEnum.TRAP_DAMAGE_BONUS] = Characteristic.Zero();
            this[CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.WATER_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.WATER_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.WaterResistance);
            this[CharacteristicEnum.WEAPON_POWER] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_WEAPON] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.WEIGHT] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.DAMAGE_PERCENT_SPELL] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.SHIELD] = Characteristic.New(0);
            this[CharacteristicEnum.PERMANENT_DAMAGE_PERCENT] = ErosionCharacteristic.New(NaturalErosion);
            this[CharacteristicEnum.HEAL_MULTIPLIER] = DetailedCharacteristic.New(100);
            this[CharacteristicEnum.BOMB_COMBO_BONUS] = DetailedCharacteristic.New(0);

            InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;

            this.Initialize();


            if (summoner != null)
            {
                this.ApplyBonusCharacteristics(monsterGrade.BonusCharacteristics, summoner);
            }


            this.CriticalHitWeapon = 0;
            this.Energy = 0;
            this.GlobalDamageReduction = 0;
            this.MaxEnergyPoints = 0;




        }

        private void ApplyBonusCharacteristics(MonsterBonusCharacteristics bonus, Fighter summoner)
        {
            var delta = (bonus.LifePoints / 100d) * (summoner.Stats.Life.Total() - summoner.Stats[CharacteristicEnum.VITALITY].TotalInContext());

            Life.Base += (int)delta;

            AddStatsPercentSummoner(summoner, bonus.Agility, CharacteristicEnum.AGILITY);
            AddStatsPercentSummoner(summoner, bonus.Strength, CharacteristicEnum.STRENGTH);
            AddStatsPercentSummoner(summoner, bonus.Intelligence, CharacteristicEnum.INTELLIGENCE);
            AddStatsPercentSummoner(summoner, bonus.Chance, CharacteristicEnum.CHANCE);
            AddStatsPercentSummoner(summoner, bonus.Wisdom, CharacteristicEnum.WISDOM);


            AddStatsPercentSummoner(summoner, bonus.TackleBlock, CharacteristicEnum.TACKLE_BLOCK);
            AddStatsPercentSummoner(summoner, bonus.TackleEvade, CharacteristicEnum.TACKLE_EVADE);


            AddStatsPercentSummoner(summoner, bonus.FireResistance, CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT);
            AddStatsPercentSummoner(summoner, bonus.WaterResistance, CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT);
            AddStatsPercentSummoner(summoner, bonus.EarthResistance, CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT);
            AddStatsPercentSummoner(summoner, bonus.AirResistance, CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT);
            AddStatsPercentSummoner(summoner, bonus.NeutralResistance, CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT);

            //  bonus.APRemoval ?

            AddStatsPercentSummoner(summoner, bonus.BonusEarthDamage, CharacteristicEnum.EARTH_DAMAGE_BONUS);
            AddStatsPercentSummoner(summoner, bonus.BonusFireDamage, CharacteristicEnum.FIRE_DAMAGE_BONUS);
            AddStatsPercentSummoner(summoner, bonus.BonusWaterDamage, CharacteristicEnum.WATER_DAMAGE_BONUS);
            AddStatsPercentSummoner(summoner, bonus.BonusAirDamage, CharacteristicEnum.AIR_DAMAGE_BONUS);


        }
        private void AddStatsPercentSummoner(Fighter summoner, int percent, CharacteristicEnum characteristic)
        {
            this[characteristic].Base += (short)(percent / 100d * summoner.Stats[characteristic].Total());
        }
        public GameFightCharacteristics GetGameFightCharacteristics(Fighter owner, CharacterFighter target)
        {
            Fighter summoner = owner.GetSummoner();

            bool summoned = summoner != null;
            var summonerId = summoned ? summoner.Id : 0;

            var result = new GameFightCharacteristics(new CharacterCharacteristics(owner.Stats.GetAllCharacterCharacteristics()),
                summonerId, summoned,
                (byte)owner.GetInvisibilityStateFor(target));

          

            return result;
        }
        [Annotation]
        public GameFightCharacteristics GetGameFightCharacteristics(Fighter owner, CharacterFighter target, CharacteristicEnum selected)
        {

            Fighter summoner = owner.GetSummoner();

            bool summoned = summoner != null;
            var summonerId = summoned ? summoner.Id : 0;

            return new GameFightCharacteristics(new CharacterCharacteristics(owner.Stats.GetCharacterCharacteristics(selected)),
                summonerId, summoned,
                (byte)owner.GetInvisibilityStateFor(target));
        }

       
    }
}
