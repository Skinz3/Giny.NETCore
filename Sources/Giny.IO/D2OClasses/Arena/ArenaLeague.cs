using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ArenaLeague", "")]
    public class ArenaLeague : IDataObject , IIndexedData
    {        public const string MODULE = "ArenaLeagues";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint ornamentId;
        public string icon;
        public string illus;
        public bool isLastLeague;

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
        public uint OrnamentId
        {
            get
            {
                return ornamentId;
            }
            set
            {
                ornamentId = value;
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
        public string Illus
        {
            get
            {
                return illus;
            }
            set
            {
                illus = value;
            }
        }
        [D2OIgnore]
        public bool IsLastLeague
        {
            get
            {
                return isLastLeague;
            }
            set
            {
                isLastLeague = value;
            }
        }

    }}
