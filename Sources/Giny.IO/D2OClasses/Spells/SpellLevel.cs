using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("SpellLevel", "")]
    public class SpellLevel : IDataObject, IIndexedData
    {
        public const string MODULE = "SpellLevels";

        public int Id => (int)id;

        public uint id;
        public uint spellId;
        public uint grade;
        public uint spellBreed;
        public uint apCost;
        public uint minRange;
        public uint range;
        public bool castInLine;
        public bool castInDiagonal;
        public bool castTestLos;
        public uint criticalHitProbability;
        public bool needFreeCell;
        public bool needTakenCell;
        public bool needFreeTrapCell;
        public bool needVisibleEntity;
        public bool needCellWithoutPortal;
        public bool portalProjectionForbidden;
        public bool rangeCanBeBoosted;
        public int maxStack;
        public uint maxCastPerTurn;
        public uint maxCastPerTarget;
        public uint minCastInterval;
        public uint initialCooldown;
        public int globalCooldown;
        public uint minPlayerLevel;
        public bool hideEffects;
        public bool hidden;
        public bool playAnimation;
        public string statesCriterion;
        public List<EffectInstanceDice> effects;
        public List<EffectInstanceDice> criticalEffect;
        public List<EffectZone> previewZones;

        [D2OIgnore]
        public uint Id_
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
        public uint SpellId
        {
            get
            {
                return spellId;
            }
            set
            {
                spellId = value;
            }
        }
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
        public uint SpellBreed
        {
            get
            {
                return spellBreed;
            }
            set
            {
                spellBreed = value;
            }
        }
        [D2OIgnore]
        public uint ApCost
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
        public uint MinRange
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
        public uint Range
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
        public uint CriticalHitProbability
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
        public bool NeedFreeCell
        {
            get
            {
                return needFreeCell;
            }
            set
            {
                needFreeCell = value;
            }
        }
        [D2OIgnore]
        public bool NeedTakenCell
        {
            get
            {
                return needTakenCell;
            }
            set
            {
                needTakenCell = value;
            }
        }
        [D2OIgnore]
        public bool NeedFreeTrapCell
        {
            get
            {
                return needFreeTrapCell;
            }
            set
            {
                needFreeTrapCell = value;
            }
        }
        [D2OIgnore]
        public bool NeedVisibleEntity
        {
            get
            {
                return needVisibleEntity;
            }
            set
            {
                needVisibleEntity = value;
            }
        }
        [D2OIgnore]
        public bool NeedCellWithoutPortal
        {
            get
            {
                return needCellWithoutPortal;
            }
            set
            {
                needCellWithoutPortal = value;
            }
        }
        [D2OIgnore]
        public bool PortalProjectionForbidden
        {
            get
            {
                return portalProjectionForbidden;
            }
            set
            {
                portalProjectionForbidden = value;
            }
        }
        [D2OIgnore]
        public bool RangeCanBeBoosted
        {
            get
            {
                return rangeCanBeBoosted;
            }
            set
            {
                rangeCanBeBoosted = value;
            }
        }
        [D2OIgnore]
        public int MaxStack
        {
            get
            {
                return maxStack;
            }
            set
            {
                maxStack = value;
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
        public uint MaxCastPerTarget
        {
            get
            {
                return maxCastPerTarget;
            }
            set
            {
                maxCastPerTarget = value;
            }
        }
        [D2OIgnore]
        public uint MinCastInterval
        {
            get
            {
                return minCastInterval;
            }
            set
            {
                minCastInterval = value;
            }
        }
        [D2OIgnore]
        public uint InitialCooldown
        {
            get
            {
                return initialCooldown;
            }
            set
            {
                initialCooldown = value;
            }
        }
        [D2OIgnore]
        public int GlobalCooldown
        {
            get
            {
                return globalCooldown;
            }
            set
            {
                globalCooldown = value;
            }
        }
        [D2OIgnore]
        public uint MinPlayerLevel
        {
            get
            {
                return minPlayerLevel;
            }
            set
            {
                minPlayerLevel = value;
            }
        }
        [D2OIgnore]
        public bool HideEffects
        {
            get
            {
                return hideEffects;
            }
            set
            {
                hideEffects = value;
            }
        }
        [D2OIgnore]
        public bool Hidden
        {
            get
            {
                return hidden;
            }
            set
            {
                hidden = value;
            }
        }
        [D2OIgnore]
        public bool PlayAnimation
        {
            get
            {
                return playAnimation;
            }
            set
            {
                playAnimation = value;
            }
        }
        [D2OIgnore]
        public string StatesCriterion
        {
            get
            {
                return statesCriterion;
            }
            set
            {
                statesCriterion = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstanceDice> Effects
        {
            get
            {
                return effects;
            }
            set
            {
                effects = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstanceDice> CriticalEffect
        {
            get
            {
                return criticalEffect;
            }
            set
            {
                criticalEffect = value;
            }
        }
        [D2OIgnore]
        public List<EffectZone> PreviewZones
        {
            get
            {
                return previewZones;
            }
            set
            {
                previewZones = value;
            }
        }

    }
}

