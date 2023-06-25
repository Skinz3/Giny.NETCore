using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpellState", "")]
    public class SpellState : IDataObject , IIndexedData
    {        public const string MODULE = "SpellStates";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public bool preventsSpellCast;
        public bool preventsFight;
        public bool isSilent;
        public bool cantDealDamage;
        public bool invulnerable;
        public bool incurable;
        public bool cantBeMoved;
        public bool cantBePushed;
        public bool cantSwitchPosition;
        public List<int> effectsIds;
        public string icon;
        public int iconVisibilityMask;
        public bool invulnerableMelee;
        public bool invulnerableRange;
        public bool cantTackle;
        public bool cantBeTackled;
        public bool displayTurnRemaining;

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
        public bool PreventsSpellCast
        {
            get
            {
                return preventsSpellCast;
            }
            set
            {
                preventsSpellCast = value;
            }
        }
        [D2OIgnore]
        public bool PreventsFight
        {
            get
            {
                return preventsFight;
            }
            set
            {
                preventsFight = value;
            }
        }
        [D2OIgnore]
        public bool IsSilent
        {
            get
            {
                return isSilent;
            }
            set
            {
                isSilent = value;
            }
        }
        [D2OIgnore]
        public bool CantDealDamage
        {
            get
            {
                return cantDealDamage;
            }
            set
            {
                cantDealDamage = value;
            }
        }
        [D2OIgnore]
        public bool Invulnerable
        {
            get
            {
                return invulnerable;
            }
            set
            {
                invulnerable = value;
            }
        }
        [D2OIgnore]
        public bool Incurable
        {
            get
            {
                return incurable;
            }
            set
            {
                incurable = value;
            }
        }
        [D2OIgnore]
        public bool CantBeMoved
        {
            get
            {
                return cantBeMoved;
            }
            set
            {
                cantBeMoved = value;
            }
        }
        [D2OIgnore]
        public bool CantBePushed
        {
            get
            {
                return cantBePushed;
            }
            set
            {
                cantBePushed = value;
            }
        }
        [D2OIgnore]
        public bool CantSwitchPosition
        {
            get
            {
                return cantSwitchPosition;
            }
            set
            {
                cantSwitchPosition = value;
            }
        }
        [D2OIgnore]
        public List<int> EffectsIds
        {
            get
            {
                return effectsIds;
            }
            set
            {
                effectsIds = value;
            }
        }
        [D2OIgnore]
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
            }
        }
        [D2OIgnore]
        public int IconVisibilityMask
        {
            get
            {
                return iconVisibilityMask;
            }
            set
            {
                iconVisibilityMask = value;
            }
        }
        [D2OIgnore]
        public bool InvulnerableMelee
        {
            get
            {
                return invulnerableMelee;
            }
            set
            {
                invulnerableMelee = value;
            }
        }
        [D2OIgnore]
        public bool InvulnerableRange
        {
            get
            {
                return invulnerableRange;
            }
            set
            {
                invulnerableRange = value;
            }
        }
        [D2OIgnore]
        public bool CantTackle
        {
            get
            {
                return cantTackle;
            }
            set
            {
                cantTackle = value;
            }
        }
        [D2OIgnore]
        public bool CantBeTackled
        {
            get
            {
                return cantBeTackled;
            }
            set
            {
                cantBeTackled = value;
            }
        }
        [D2OIgnore]
        public bool DisplayTurnRemaining
        {
            get
            {
                return displayTurnRemaining;
            }
            set
            {
                displayTurnRemaining = value;
            }
        }

    }}
