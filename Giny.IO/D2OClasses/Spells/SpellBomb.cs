using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpellBomb", "")]
    public class SpellBomb : IDataObject , IIndexedData
    {        public const string MODULE = "SpellBombs";

        public int Id => (int)id;

        public int id;
        public int chainReactionSpellId;
        public int explodSpellId;
        public int wallId;
        public int instantSpellId;
        public int comboCoeff;

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
        public int ChainReactionSpellId
        {
            get
            {
                return chainReactionSpellId;
            }
            set
            {
                chainReactionSpellId = value;
            }
        }
        [D2OIgnore]
        public int ExplodSpellId
        {
            get
            {
                return explodSpellId;
            }
            set
            {
                explodSpellId = value;
            }
        }
        [D2OIgnore]
        public int WallId
        {
            get
            {
                return wallId;
            }
            set
            {
                wallId = value;
            }
        }
        [D2OIgnore]
        public int InstantSpellId
        {
            get
            {
                return instantSpellId;
            }
            set
            {
                instantSpellId = value;
            }
        }
        [D2OIgnore]
        public int ComboCoeff
        {
            get
            {
                return comboCoeff;
            }
            set
            {
                comboCoeff = value;
            }
        }

    }}
