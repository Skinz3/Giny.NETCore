using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstance", "")]
    public class EffectInstance : IDataObject , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint effectUid;
        public uint baseEffectId;
        public uint effectId;
        public int order;
        public int targetId;
        public string targetMask;
        public int duration;
        public int delay;
        public double random;
        public int group;
        public int modificator;
        public bool trigger;
        public string triggers;
        public bool visibleInTooltip;
        public bool visibleInBuffUi;
        public bool visibleInFightLog;
        public bool visibleOnTerrain;
        public bool forClientOnly;
        public int dispellable;
        public object zoneSize;
        public uint zoneShape;
        public object zoneMinSize;
        public object zoneEfficiencyPercent;
        public object zoneMaxEfficiency;
        public object zoneStopAtTarget;
        public int effectElement;
        public int spellId;
        public string rawZone;

        [D2OIgnore]
        public uint EffectUid
        {
            get
            {
                return effectUid;
            }
            set
            {
                effectUid = value;
            }
        }
        [D2OIgnore]
        public uint BaseEffectId
        {
            get
            {
                return baseEffectId;
            }
            set
            {
                baseEffectId = value;
            }
        }
        [D2OIgnore]
        public uint EffectId
        {
            get
            {
                return effectId;
            }
            set
            {
                effectId = value;
            }
        }
        [D2OIgnore]
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
        [D2OIgnore]
        public int TargetId
        {
            get
            {
                return targetId;
            }
            set
            {
                targetId = value;
            }
        }
        [D2OIgnore]
        public string TargetMask
        {
            get
            {
                return targetMask;
            }
            set
            {
                targetMask = value;
            }
        }
        [D2OIgnore]
        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        [D2OIgnore]
        public int Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        [D2OIgnore]
        public double Random
        {
            get
            {
                return random;
            }
            set
            {
                random = value;
            }
        }
        [D2OIgnore]
        public int Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }
        [D2OIgnore]
        public int Modificator
        {
            get
            {
                return modificator;
            }
            set
            {
                modificator = value;
            }
        }
        [D2OIgnore]
        public bool Trigger
        {
            get
            {
                return trigger;
            }
            set
            {
                trigger = value;
            }
        }
        [D2OIgnore]
        public string Triggers
        {
            get
            {
                return triggers;
            }
            set
            {
                triggers = value;
            }
        }
        [D2OIgnore]
        public bool VisibleInTooltip
        {
            get
            {
                return visibleInTooltip;
            }
            set
            {
                visibleInTooltip = value;
            }
        }
        [D2OIgnore]
        public bool VisibleInBuffUi
        {
            get
            {
                return visibleInBuffUi;
            }
            set
            {
                visibleInBuffUi = value;
            }
        }
        [D2OIgnore]
        public bool VisibleInFightLog
        {
            get
            {
                return visibleInFightLog;
            }
            set
            {
                visibleInFightLog = value;
            }
        }
        [D2OIgnore]
        public bool VisibleOnTerrain
        {
            get
            {
                return visibleOnTerrain;
            }
            set
            {
                visibleOnTerrain = value;
            }
        }
        [D2OIgnore]
        public bool ForClientOnly
        {
            get
            {
                return forClientOnly;
            }
            set
            {
                forClientOnly = value;
            }
        }
        [D2OIgnore]
        public int Dispellable
        {
            get
            {
                return dispellable;
            }
            set
            {
                dispellable = value;
            }
        }
        [D2OIgnore]
        public object ZoneSize
        {
            get
            {
                return zoneSize;
            }
            set
            {
                zoneSize = value;
            }
        }
        [D2OIgnore]
        public uint ZoneShape
        {
            get
            {
                return zoneShape;
            }
            set
            {
                zoneShape = value;
            }
        }
        [D2OIgnore]
        public object ZoneMinSize
        {
            get
            {
                return zoneMinSize;
            }
            set
            {
                zoneMinSize = value;
            }
        }
        [D2OIgnore]
        public object ZoneEfficiencyPercent
        {
            get
            {
                return zoneEfficiencyPercent;
            }
            set
            {
                zoneEfficiencyPercent = value;
            }
        }
        [D2OIgnore]
        public object ZoneMaxEfficiency
        {
            get
            {
                return zoneMaxEfficiency;
            }
            set
            {
                zoneMaxEfficiency = value;
            }
        }
        [D2OIgnore]
        public object ZoneStopAtTarget
        {
            get
            {
                return zoneStopAtTarget;
            }
            set
            {
                zoneStopAtTarget = value;
            }
        }
        [D2OIgnore]
        public int EffectElement
        {
            get
            {
                return effectElement;
            }
            set
            {
                effectElement = value;
            }
        }
        [D2OIgnore]
        public int SpellId
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
        public string RawZone
        {
            get
            {
                return rawZone;
            }
            set
            {
                rawZone = value;
            }
        }

    }}
