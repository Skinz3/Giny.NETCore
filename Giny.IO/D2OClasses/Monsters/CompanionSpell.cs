using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CompanionSpell", "")]
    public class CompanionSpell : IDataObject , IIndexedData
    {        public const string MODULE = "CompanionSpells";

        public int Id => (int)id;

        public int id;
        public int spellId;
        public int companionId;
        public string gradeByLevel;

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
        public int CompanionId
        {
            get
            {
                return companionId;
            }
            set
            {
                companionId = value;
            }
        }
        [D2OIgnore]
        public string GradeByLevel
        {
            get
            {
                return gradeByLevel;
            }
            set
            {
                gradeByLevel = value;
            }
        }

    }}
