using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("LuaFormula", "")]
    public class LuaFormula : IDataObject , IIndexedData
    {        public const string MODULE = "LuaFormulas";

        public int Id => (int)id;

        public int id;
        public string formulaName;
        public string luaFormula;

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
        public string FormulaName
        {
            get
            {
                return formulaName;
            }
            set
            {
                formulaName = value;
            }
        }
        [D2OIgnore]
        public string LuaFormula_
        {
            get
            {
                return luaFormula;
            }
            set
            {
                luaFormula = value;
            }
        }

    }}
