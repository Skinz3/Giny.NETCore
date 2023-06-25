using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceInteger", "")]
    public class EffectInstanceInteger : EffectInstance , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public int value;

        [D2OIgnore]
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                value = value;
            }
        }

    }}
