using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterRace", "")]
    public class MonsterRace : IDataObject , IIndexedData
    {        public const string MODULE = "MonsterRaces";

        public int Id => (int)id;

        public int id;
        public int superRaceId;
        public uint nameId;
        public List<uint> monsters;
        public int aggressiveZoneSize;
        public int aggressiveLevelDiff;
        public string aggressiveImmunityCriterion;
        public int aggressiveAttackDelay;

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
        public int SuperRaceId
        {
            get
            {
                return superRaceId;
            }
            set
            {
                superRaceId = value;
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
        public List<uint> Monsters
        {
            get
            {
                return monsters;
            }
            set
            {
                monsters = value;
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

    }}
