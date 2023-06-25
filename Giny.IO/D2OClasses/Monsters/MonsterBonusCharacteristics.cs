using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterBonusCharacteristics", "")]
    public class MonsterBonusCharacteristics : IDataObject , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public int lifePoints;
        public int strength;
        public int wisdom;
        public int chance;
        public int agility;
        public int intelligence;
        public int earthResistance;
        public int fireResistance;
        public int waterResistance;
        public int airResistance;
        public int neutralResistance;
        public int tackleEvade;
        public int tackleBlock;
        public int bonusEarthDamage;
        public int bonusFireDamage;
        public int bonusWaterDamage;
        public int bonusAirDamage;
        public int APRemoval;

        [D2OIgnore]
        public int LifePoints
        {
            get
            {
                return lifePoints;
            }
            set
            {
                lifePoints = value;
            }
        }
        [D2OIgnore]
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }
        [D2OIgnore]
        public int Wisdom
        {
            get
            {
                return wisdom;
            }
            set
            {
                wisdom = value;
            }
        }
        [D2OIgnore]
        public int Chance
        {
            get
            {
                return chance;
            }
            set
            {
                chance = value;
            }
        }
        [D2OIgnore]
        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
            }
        }
        [D2OIgnore]
        public int Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                intelligence = value;
            }
        }
        [D2OIgnore]
        public int EarthResistance
        {
            get
            {
                return earthResistance;
            }
            set
            {
                earthResistance = value;
            }
        }
        [D2OIgnore]
        public int FireResistance
        {
            get
            {
                return fireResistance;
            }
            set
            {
                fireResistance = value;
            }
        }
        [D2OIgnore]
        public int WaterResistance
        {
            get
            {
                return waterResistance;
            }
            set
            {
                waterResistance = value;
            }
        }
        [D2OIgnore]
        public int AirResistance
        {
            get
            {
                return airResistance;
            }
            set
            {
                airResistance = value;
            }
        }
        [D2OIgnore]
        public int NeutralResistance
        {
            get
            {
                return neutralResistance;
            }
            set
            {
                neutralResistance = value;
            }
        }
        [D2OIgnore]
        public int TackleEvade
        {
            get
            {
                return tackleEvade;
            }
            set
            {
                tackleEvade = value;
            }
        }
        [D2OIgnore]
        public int TackleBlock
        {
            get
            {
                return tackleBlock;
            }
            set
            {
                tackleBlock = value;
            }
        }
        [D2OIgnore]
        public int BonusEarthDamage
        {
            get
            {
                return bonusEarthDamage;
            }
            set
            {
                bonusEarthDamage = value;
            }
        }
        [D2OIgnore]
        public int BonusFireDamage
        {
            get
            {
                return bonusFireDamage;
            }
            set
            {
                bonusFireDamage = value;
            }
        }
        [D2OIgnore]
        public int BonusWaterDamage
        {
            get
            {
                return bonusWaterDamage;
            }
            set
            {
                bonusWaterDamage = value;
            }
        }
        [D2OIgnore]
        public int BonusAirDamage
        {
            get
            {
                return bonusAirDamage;
            }
            set
            {
                bonusAirDamage = value;
            }
        }
        [D2OIgnore]
        public int _APRemoval
        {
            get
            {
                return APRemoval;
            }
            set
            {
                APRemoval = value;
            }
        }

    }}
