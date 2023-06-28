using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Monsters;
using Giny.World.Managers.Stats;
using Giny.World.Records.Characters;
using System;
using System.Collections.Generic;
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

        public short ApUsed
        {
            get;
            private set;
        }

        public short MpUsed
        {
            get;
            private set;
        }
        public short Erosion
        {
            get;
            private set;
        }

        public int ErodedLife
        {
            get
            {
                return BaseMaxLife - MaxLifePoints;
            }
        }
        public int BaseMaxLife
        {
            get;
            set;
        }

        public short ShieldPoints => this[CharacteristicEnum.SHIELD].TotalInContext();
        public short DamageMultiplier
        {
            get;
            set;
        }

        public short FinalDamagePercent
        {
            get;
            set;
        }

        public void AddErosion(short amount)
        {
            this.Erosion += amount;

            if (Erosion > MaxErosion)
            {
                Erosion = MaxErosion;
            }
        }
        public void RemoveErosion(short amount)
        {
            this.Erosion -= amount;

            if (Erosion < 0)
            {
                Erosion = 0;
            }
        }
        public void AddShield(short delta)
        {
            this[CharacteristicEnum.SHIELD].Context += delta;
        }
        public void RemoveShield(short delta)
        {
            this[CharacteristicEnum.SHIELD].Context -= delta;


            if (this[CharacteristicEnum.SHIELD].TotalInContext() < 0)
            {
                this[CharacteristicEnum.SHIELD].Context = 0;
            }
        }
        public void AddMaxVitality(short delta)
        {
            this.BaseMaxLife += delta;
            this.MaxLifePoints += delta;
            this.LifePoints += delta;
        }
        public void RemoveMaxVitality(short delta)
        {
            this.BaseMaxLife -= delta;
            this.MaxLifePoints -= delta;
            this.LifePoints -= delta;

            if (LifePoints < 0)
            {
                LifePoints = 0;
            }
            if (MaxLifePoints < 0)
            {
                MaxLifePoints = 0;
            }
        }
        public void RemoveVitality(short delta)
        {
            this.LifePoints -= delta;

            if (LifePoints < 0)
            {
                LifePoints = 0;
            }
        }
        public void AddVitality(short delta)
        {
            this.LifePoints += delta;

            if (LifePoints >= MaxLifePoints)
            {
                LifePoints = MaxLifePoints;
            }
        }
        public void SetShield(short delta)
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
        public void ResetUsedPoints()
        {
            MovementPoints.Context += MpUsed;
            ActionPoints.Context += ApUsed;
            MpUsed = 0;
            ApUsed = 0;
        }
        public void GainAp(short amount)
        {
            ActionPoints.Context += amount;
            ApUsed -= amount;

        }
        public void GainMp(short amount)
        {
            MovementPoints.Context += amount;
            MpUsed -= amount;
        }

        public void UseMp(short amount)
        {
            MovementPoints.Context -= amount;
            MpUsed += amount;
        }
        public void UseAp(short amount)
        {
            ActionPoints.Context -= amount;
            ApUsed += amount;
        }

        public FighterStats(Character character)
        {
            foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in character.Stats.GetCharacteristics<Characteristic>())
            {
                this[stat.Key] = stat.Value.Clone();
            }

            this.CriticalHitWeapon = character.Stats.CriticalHitWeapon;
            this.Energy = character.Stats.Energy;
            this.LifePoints = character.Stats.LifePoints;
            this.MaxLifePoints = character.Stats.MaxLifePoints;
            this.MaxEnergyPoints = character.Stats.MaxEnergyPoints;
            InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.BaseMaxLife = MaxLifePoints;
            this.Erosion = NaturalErosion;

        }
        public FighterStats(FighterStats other)
        {
            foreach (KeyValuePair<CharacteristicEnum, Characteristic> stat in other.GetCharacteristics<Characteristic>())
            {
                this[stat.Key] = stat.Value.Clone();
            }

            this.CriticalHitWeapon = other.CriticalHitWeapon;
            this.Energy = other.Energy;
            this.LifePoints = other.LifePoints;
            this.MaxLifePoints = other.MaxLifePoints;
            this.MaxEnergyPoints = other.MaxEnergyPoints;
            this.InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.BaseMaxLife = MaxLifePoints;
            this.Erosion = NaturalErosion;
            this.Initialize();
        }
        /*
         * Todo : Summoned / SummonerId
         */
        public FighterStats(MonsterGrade monsterGrade, double coeff = 1d)
        {
            this[CharacteristicEnum.ACTION_POINTS] = ApCharacteristic.New(monsterGrade.ActionPoints);
            this[CharacteristicEnum.AIR_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.AIR_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.AirResistance);
            this[CharacteristicEnum.ALL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();


            this[CharacteristicEnum.CRITICAL_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.CRITICAL_HIT] = DetailedCharacteristic.Zero();
            this.CriticalHitWeapon = 0;
            this[CharacteristicEnum.DAMAGE_PERCENT] = DetailedCharacteristic.Zero();

            this[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY] = PointDodgeCharacteristic.New(monsterGrade.ApDodge);
            this[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY] = PointDodgeCharacteristic.New(monsterGrade.MpDodge);
            this[CharacteristicEnum.EARTH_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.EARTH_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.EarthResistance);
            this.Energy = 0;
            this[CharacteristicEnum.FIRE_DAMAGE_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.FIRE_ELEMENT_REDUCTION] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT] = ResistanceCharacteristic.New(monsterGrade.FireResistance);
            this.GlobalDamageReduction = 0;
            this[CharacteristicEnum.GLYPH_POWER] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.HEAL_BONUS] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.INITIATIVE] = DetailedCharacteristic.Zero();
            this[CharacteristicEnum.INTELLIGENCE] = DetailedCharacteristic.New(monsterGrade.Intelligence);
            this[CharacteristicEnum.WISDOM] = DetailedCharacteristic.New((short)(monsterGrade.Wisdom * coeff));
            this[CharacteristicEnum.CHANCE] = DetailedCharacteristic.New((short)(monsterGrade.Chance * coeff));
            this[CharacteristicEnum.AGILITY] = DetailedCharacteristic.New((short)(monsterGrade.Agility * coeff));
            this[CharacteristicEnum.STRENGTH] = DetailedCharacteristic.New((short)(monsterGrade.Strength * coeff));
            this[CharacteristicEnum.VITALITY] = DetailedCharacteristic.New((short)(monsterGrade.Vitality * coeff));
            this.MaxLifePoints = (int)(monsterGrade.LifePoints * coeff);
            this.MaxEnergyPoints = 0;

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


            InvisibilityState = GameActionFightInvisibilityStateEnum.VISIBLE;
            this.BaseMaxLife = MaxLifePoints;
            this.LifePoints = MaxLifePoints;
            this.Erosion = NaturalErosion;
            this.Initialize();
        }

        public GameFightCharacteristics GetGameFightCharacteristics(Fighter owner, CharacterFighter target)
        {
            Fighter summoner = owner.GetSummoner();

            bool summoned = summoner != null;
            var summonerId = summoned ? summoner.Id : 0;

            return new GameFightCharacteristics(new CharacterCharacteristics(owner.Stats.GetCharacterCharacteristics()),
                summonerId, summoned,
                (byte)owner.GetInvisibilityStateFor(target));
        }
        [WIP]
        public GameFightCharacteristics GetGameFightCharacteristics(Fighter owner, CharacterFighter target, CharacteristicEnum? selected = null)
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
