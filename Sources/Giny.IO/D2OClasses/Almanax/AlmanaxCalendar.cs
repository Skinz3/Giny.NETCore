using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AlmanaxCalendar", "")]
    public class AlmanaxCalendar : IDataObject , IIndexedData
    {        public const string MODULE = "AlmanaxCalendars";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint descId;
        public int npcId;
        public List<int> bonusesIds;

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
        public uint DescId
        {
            get
            {
                return descId;
            }
            set
            {
                descId = value;
            }
        }
        [D2OIgnore]
        public int NpcId
        {
            get
            {
                return npcId;
            }
            set
            {
                npcId = value;
            }
        }
        [D2OIgnore]
        public List<int> BonusesIds
        {
            get
            {
                return bonusesIds;
            }
            set
            {
                bonusesIds = value;
            }
        }

    }}
