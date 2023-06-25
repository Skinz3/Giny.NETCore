using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterGrade", "")]
    public class MonsterGrade : IDataObject , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint grade;
        public int monsterId;
        public uint level;
        public int vitality;
        public int paDodge;
        public int pmDodge;
        public int wisdom;
        public int earthResistance;
        public int airResistance;
        public int fireResistance;
        public int waterResistance;
        public int neutralResistance;
        public int gradeXp;
        public int lifePoints;
        public int actionPoints;
        public int movementPoints;
        public int damageReflect;
        public uint hiddenLevel;
        public int strength;
        public int intelligence;
        public int chance;
        public int agility;
        public int bonusRange;
        public int startingSpellId;
        public MonsterBonusCharacteristics bonusCharacteristics;

        [D2OIgnore]
        public uint Grade
        {
            get
            {
                return grade;
            }
            set
            {
                grade = value;
            }
        }
        [D2OIgnore]
        public int MonsterId
        {
            get
            {
                return monsterId;
            }
            set
            {
                monsterId = value;
            }
        }
        [D2OIgnore]
        public uint Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        [D2OIgnore]
        public int Vitality
        {
            get
            {
                return vitality;
            }
            set
            {
                vitality = value;
            }
        }
        [D2OIgnore]
        public int PaDodge
        {
            get
            {
                return paDodge;
            }
            set
            {
                paDodge = value;
            }
        }
        [D2OIgnore]
        public int PmDodge
        {
            get
            {
                return pmDodge;
            }
            set
            {
                pmDodge = value;
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
        public int GradeXp
        {
            get
            {
                return gradeXp;
            }
            set
            {
                gradeXp = value;
            }
        }
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
        public int ActionPoints
        {
            get
            {
                return actionPoints;
            }
            set
            {
                actionPoints = value;
            }
        }
        [D2OIgnore]
        public int MovementPoints
        {
            get
            {
                return movementPoints;
            }
            set
            {
                movementPoints = value;
            }
        }
        [D2OIgnore]
        public int DamageReflect
        {
            get
            {
                return damageReflect;
            }
            set
            {
                damageReflect = value;
            }
        }
        [D2OIgnore]
        public uint HiddenLevel
        {
            get
            {
                return hiddenLevel;
            }
            set
            {
                hiddenLevel = value;
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
        public int BonusRange
        {
            get
            {
                return bonusRange;
            }
            set
            {
                bonusRange = value;
            }
        }
        [D2OIgnore]
        public int StartingSpellId
        {
            get
            {
                return startingSpellId;
            }
            set
            {
                startingSpellId = value;
            }
        }
        [D2OIgnore]
        public MonsterBonusCharacteristics BonusCharacteristics
        {
            get
            {
                return bonusCharacteristics;
            }
            set
            {
                bonusCharacteristics = value;
            }
        }

    }}
