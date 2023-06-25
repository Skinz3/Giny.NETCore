using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceDuration", "")]
    public class EffectInstanceDuration : EffectInstance , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint days;
        public uint hours;
        public uint minutes;

        [D2OIgnore]
        public uint Days
        {
            get
            {
                return days;
            }
            set
            {
                days = value;
            }
        }
        [D2OIgnore]
        public uint Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
            }
        }
        [D2OIgnore]
        public uint Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
            }
        }

    }}
