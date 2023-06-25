using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Emoticon", "")]
    public class Emoticon : IDataObject , IIndexedData
    {        public const string MODULE = "Emoticons";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint shortcutId;
        public uint order;
        public string defaultAnim;
        public bool persistancy;
        public bool eight_directions;
        public bool aura;
        public List<string> anims;
        public uint cooldown;
        public uint duration;
        public uint weight;
        public uint spellLevelId;

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
        public uint ShortcutId
        {
            get
            {
                return shortcutId;
            }
            set
            {
                shortcutId = value;
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
        public string DefaultAnim
        {
            get
            {
                return defaultAnim;
            }
            set
            {
                defaultAnim = value;
            }
        }
        [D2OIgnore]
        public bool Persistancy
        {
            get
            {
                return persistancy;
            }
            set
            {
                persistancy = value;
            }
        }
        [D2OIgnore]
        public bool Eight_directions
        {
            get
            {
                return eight_directions;
            }
            set
            {
                eight_directions = value;
            }
        }
        [D2OIgnore]
        public bool Aura
        {
            get
            {
                return aura;
            }
            set
            {
                aura = value;
            }
        }
        [D2OIgnore]
        public List<string> Anims
        {
            get
            {
                return anims;
            }
            set
            {
                anims = value;
            }
        }
        [D2OIgnore]
        public uint Cooldown
        {
            get
            {
                return cooldown;
            }
            set
            {
                cooldown = value;
            }
        }
        [D2OIgnore]
        public uint Duration
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
        public uint Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }
        [D2OIgnore]
        public uint SpellLevelId
        {
            get
            {
                return spellLevelId;
            }
            set
            {
                spellLevelId = value;
            }
        }

    }}
