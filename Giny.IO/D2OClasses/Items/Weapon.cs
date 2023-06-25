using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Weapon", "")]
    public class Weapon : Item , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public int apCost;
        public int minRange;
        public int range;
        public uint maxCastPerTurn;
        public bool castInLine;
        public bool castInDiagonal;
        public bool castTestLos;
        public int criticalHitProbability;
        public int criticalHitBonus;
        public int criticalFailureProbability;

        [D2OIgnore]
        public int ApCost
        {
            get
            {
                return apCost;
            }
            set
            {
                apCost = value;
            }
        }
        [D2OIgnore]
        public int MinRange
        {
            get
            {
                return minRange;
            }
            set
            {
                minRange = value;
            }
        }
        [D2OIgnore]
        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }
        [D2OIgnore]
        public uint MaxCastPerTurn
        {
            get
            {
                return maxCastPerTurn;
            }
            set
            {
                maxCastPerTurn = value;
            }
        }
        [D2OIgnore]
        public bool CastInLine
        {
            get
            {
                return castInLine;
            }
            set
            {
                castInLine = value;
            }
        }
        [D2OIgnore]
        public bool CastInDiagonal
        {
            get
            {
                return castInDiagonal;
            }
            set
            {
                castInDiagonal = value;
            }
        }
        [D2OIgnore]
        public bool CastTestLos
        {
            get
            {
                return castTestLos;
            }
            set
            {
                castTestLos = value;
            }
        }
        [D2OIgnore]
        public int CriticalHitProbability
        {
            get
            {
                return criticalHitProbability;
            }
            set
            {
                criticalHitProbability = value;
            }
        }
        [D2OIgnore]
        public int CriticalHitBonus
        {
            get
            {
                return criticalHitBonus;
            }
            set
            {
                criticalHitBonus = value;
            }
        }
        [D2OIgnore]
        public int CriticalFailureProbability
        {
            get
            {
                return criticalFailureProbability;
            }
            set
            {
                criticalFailureProbability = value;
            }
        }

    }}
