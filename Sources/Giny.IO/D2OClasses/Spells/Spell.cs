using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Spell", "")]
    public class Spell : IDataObject , IIndexedData
    {        public const string MODULE = "Spells";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint descriptionId;
        public uint typeId;
        public uint order;
        public string scriptParams;
        public string scriptParamsCritical;
        public int scriptId;
        public int scriptIdCritical;
        public uint iconId;
        public List<uint> spellLevels;
        public bool useParamCache;
        public bool verbose_cast;
        public string default_zone;
        public bool bypassSummoningLimit;
        public bool canAlwaysTriggerSpells;
        public string adminName;

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
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
            }
        }
        [D2OIgnore]
        public uint Order
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
        public string ScriptParams
        {
            get
            {
                return scriptParams;
            }
            set
            {
                scriptParams = value;
            }
        }
        [D2OIgnore]
        public string ScriptParamsCritical
        {
            get
            {
                return scriptParamsCritical;
            }
            set
            {
                scriptParamsCritical = value;
            }
        }
        [D2OIgnore]
        public int ScriptId
        {
            get
            {
                return scriptId;
            }
            set
            {
                scriptId = value;
            }
        }
        [D2OIgnore]
        public int ScriptIdCritical
        {
            get
            {
                return scriptIdCritical;
            }
            set
            {
                scriptIdCritical = value;
            }
        }
        [D2OIgnore]
        public uint IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public List<uint> SpellLevels
        {
            get
            {
                return spellLevels;
            }
            set
            {
                spellLevels = value;
            }
        }
        [D2OIgnore]
        public bool UseParamCache
        {
            get
            {
                return useParamCache;
            }
            set
            {
                useParamCache = value;
            }
        }
        [D2OIgnore]
        public bool Verbose_cast
        {
            get
            {
                return verbose_cast;
            }
            set
            {
                verbose_cast = value;
            }
        }
        [D2OIgnore]
        public string Default_zone
        {
            get
            {
                return default_zone;
            }
            set
            {
                default_zone = value;
            }
        }
        [D2OIgnore]
        public bool BypassSummoningLimit
        {
            get
            {
                return bypassSummoningLimit;
            }
            set
            {
                bypassSummoningLimit = value;
            }
        }
        [D2OIgnore]
        public bool CanAlwaysTriggerSpells
        {
            get
            {
                return canAlwaysTriggerSpells;
            }
            set
            {
                canAlwaysTriggerSpells = value;
            }
        }
        [D2OIgnore]
        public string AdminName
        {
            get
            {
                return adminName;
            }
            set
            {
                adminName = value;
            }
        }

    }}
