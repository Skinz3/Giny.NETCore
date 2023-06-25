using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Monster", "")]
    public class Monster : IDataObject , IIndexedData
    {        public const string MODULE = "Monsters";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint gfxId;
        public int race;
        public List<MonsterGrade> grades;
        public string look;
        public bool useSummonSlot;
        public bool useBombSlot;
        public bool canPlay;
        public bool canTackle;
        public List<AnimFunMonsterData> animFunList;
        public bool isBoss;
        public List<MonsterDrop> drops;
        public List<MonsterDrop> temporisDrops;
        public List<uint> subareas;
        public List<uint> spells;
        public int favoriteSubareaId;
        public bool isMiniBoss;
        public bool isQuestMonster;
        public uint correspondingMiniBossId;
        public double speedAdjust;
        public int creatureBoneId;
        public bool canBePushed;
        public bool canBeCarried;
        public bool canUsePortal;
        public bool canSwitchPos;
        public bool canSwitchPosOnTarget;
        public bool fastAnimsFun;
        public List<uint> incompatibleIdols;
        public bool allIdolsDisabled;
        public List<uint> incompatibleChallenges;
        public bool useRaceValues;
        public int aggressiveZoneSize;
        public int aggressiveLevelDiff;
        public string aggressiveImmunityCriterion;
        public int aggressiveAttackDelay;
        public int scaleGradeRef;
        public List<List<double>> characRatios;

        [D2OIgnore]
        public int Id_
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        [D2OIgnore]
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public uint GfxId
        {
            get
            {
                return gfxId;
            }
            set
            {
                gfxId = value;
            }
        }
        [D2OIgnore]
        public int Race
        {
            get
            {
                return race;
            }
            set
            {
                race = value;
            }
        }
        [D2OIgnore]
        public List<MonsterGrade> Grades
        {
            get
            {
                return grades;
            }
            set
            {
                grades = value;
            }
        }
        [D2OIgnore]
        public string Look
        {
            get
            {
                return look;
            }
            set
            {
                look = value;
            }
        }
        [D2OIgnore]
        public bool UseSummonSlot
        {
            get
            {
                return useSummonSlot;
            }
            set
            {
                useSummonSlot = value;
            }
        }
        [D2OIgnore]
        public bool UseBombSlot
        {
            get
            {
                return useBombSlot;
            }
            set
            {
                useBombSlot = value;
            }
        }
        [D2OIgnore]
        public bool CanPlay
        {
            get
            {
                return canPlay;
            }
            set
            {
                canPlay = value;
            }
        }
        [D2OIgnore]
        public bool CanTackle
        {
            get
            {
                return canTackle;
            }
            set
            {
                canTackle = value;
            }
        }
        [D2OIgnore]
        public List<AnimFunMonsterData> AnimFunList
        {
            get
            {
                return animFunList;
            }
            set
            {
                animFunList = value;
            }
        }
        [D2OIgnore]
        public bool IsBoss
        {
            get
            {
                return isBoss;
            }
            set
            {
                isBoss = value;
            }
        }
        [D2OIgnore]
        public List<MonsterDrop> Drops
        {
            get
            {
                return drops;
            }
            set
            {
                drops = value;
            }
        }
        [D2OIgnore]
        public List<MonsterDrop> TemporisDrops
        {
            get
            {
                return temporisDrops;
            }
            set
            {
                temporisDrops = value;
            }
        }
        [D2OIgnore]
        public List<uint> Subareas
        {
            get
            {
                return subareas;
            }
            set
            {
                subareas = value;
            }
        }
        [D2OIgnore]
        public List<uint> Spells
        {
            get
            {
                return spells;
            }
            set
            {
                spells = value;
            }
        }
        [D2OIgnore]
        public int FavoriteSubareaId
        {
            get
            {
                return favoriteSubareaId;
            }
            set
            {
                favoriteSubareaId = value;
            }
        }
        [D2OIgnore]
        public bool IsMiniBoss
        {
            get
            {
                return isMiniBoss;
            }
            set
            {
                isMiniBoss = value;
            }
        }
        [D2OIgnore]
        public bool IsQuestMonster
        {
            get
            {
                return isQuestMonster;
            }
            set
            {
                isQuestMonster = value;
            }
        }
        [D2OIgnore]
        public uint CorrespondingMiniBossId
        {
            get
            {
                return correspondingMiniBossId;
            }
            set
            {
                correspondingMiniBossId = value;
            }
        }
        [D2OIgnore]
        public double SpeedAdjust
        {
            get
            {
                return speedAdjust;
            }
            set
            {
                speedAdjust = value;
            }
        }
        [D2OIgnore]
        public int CreatureBoneId
        {
            get
            {
                return creatureBoneId;
            }
            set
            {
                creatureBoneId = value;
            }
        }
        [D2OIgnore]
        public bool CanBePushed
        {
            get
            {
                return canBePushed;
            }
            set
            {
                canBePushed = value;
            }
        }
        [D2OIgnore]
        public bool CanBeCarried
        {
            get
            {
                return canBeCarried;
            }
            set
            {
                canBeCarried = value;
            }
        }
        [D2OIgnore]
        public bool CanUsePortal
        {
            get
            {
                return canUsePortal;
            }
            set
            {
                canUsePortal = value;
            }
        }
        [D2OIgnore]
        public bool CanSwitchPos
        {
            get
            {
                return canSwitchPos;
            }
            set
            {
                canSwitchPos = value;
            }
        }
        [D2OIgnore]
        public bool CanSwitchPosOnTarget
        {
            get
            {
                return canSwitchPosOnTarget;
            }
            set
            {
                canSwitchPosOnTarget = value;
            }
        }
        [D2OIgnore]
        public bool FastAnimsFun
        {
            get
            {
                return fastAnimsFun;
            }
            set
            {
                fastAnimsFun = value;
            }
        }
        [D2OIgnore]
        public List<uint> IncompatibleIdols
        {
            get
            {
                return incompatibleIdols;
            }
            set
            {
                incompatibleIdols = value;
            }
        }
        [D2OIgnore]
        public bool AllIdolsDisabled
        {
            get
            {
                return allIdolsDisabled;
            }
            set
            {
                allIdolsDisabled = value;
            }
        }
        [D2OIgnore]
        public List<uint> IncompatibleChallenges
        {
            get
            {
                return incompatibleChallenges;
            }
            set
            {
                incompatibleChallenges = value;
            }
        }
        [D2OIgnore]
        public bool UseRaceValues
        {
            get
            {
                return useRaceValues;
            }
            set
            {
                useRaceValues = value;
            }
        }
        [D2OIgnore]
        public int AggressiveZoneSize
        {
            get
            {
                return aggressiveZoneSize;
            }
            set
            {
                aggressiveZoneSize = value;
            }
        }
        [D2OIgnore]
        public int AggressiveLevelDiff
        {
            get
            {
                return aggressiveLevelDiff;
            }
            set
            {
                aggressiveLevelDiff = value;
            }
        }
        [D2OIgnore]
        public string AggressiveImmunityCriterion
        {
            get
            {
                return aggressiveImmunityCriterion;
            }
            set
            {
                aggressiveImmunityCriterion = value;
            }
        }
        [D2OIgnore]
        public int AggressiveAttackDelay
        {
            get
            {
                return aggressiveAttackDelay;
            }
            set
            {
                aggressiveAttackDelay = value;
            }
        }
        [D2OIgnore]
        public int ScaleGradeRef
        {
            get
            {
                return scaleGradeRef;
            }
            set
            {
                scaleGradeRef = value;
            }
        }
        [D2OIgnore]
        public List<List<double>> CharacRatios
        {
            get
            {
                return characRatios;
            }
            set
            {
                characRatios = value;
            }
        }

    }}
