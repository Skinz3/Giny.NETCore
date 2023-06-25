using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("FinishMove", "")]
    public class FinishMove : IDataObject , IIndexedData
    {        public const string MODULE = "FinishMoves";

        public int Id => (int)id;

        public int id;
        public int duration;
        public bool free;
        public uint nameId;
        public int category;
        public int spellLevel;

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
        public bool Free
        {
            get
            {
                return free;
            }
            set
            {
                free = value;
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
        public int Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        [D2OIgnore]
        public int SpellLevel
        {
            get
            {
                return spellLevel;
            }
            set
            {
                spellLevel = value;
            }
        }

    }}
