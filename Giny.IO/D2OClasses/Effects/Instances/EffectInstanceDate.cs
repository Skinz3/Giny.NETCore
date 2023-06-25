using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceDate", "")]
    public class EffectInstanceDate : EffectInstance , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint year;
        public uint month;
        public uint day;
        public uint hour;
        public uint minute;

        [D2OIgnore]
        public uint Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        [D2OIgnore]
        public uint Month
        {
            get
            {
                return month;
            }
            set
            {
                month = value;
            }
        }
        [D2OIgnore]
        public uint Day
        {
            get
            {
                return day;
            }
            set
            {
                day = value;
            }
        }
        [D2OIgnore]
        public uint Hour
        {
            get
            {
                return hour;
            }
            set
            {
                hour = value;
            }
        }
        [D2OIgnore]
        public uint Minute
        {
            get
            {
                return minute;
            }
            set
            {
                minute = value;
            }
        }

    }}
