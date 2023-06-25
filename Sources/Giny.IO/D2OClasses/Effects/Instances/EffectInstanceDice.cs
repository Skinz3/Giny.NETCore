using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceDice", "")]
    public class EffectInstanceDice : EffectInstanceInteger , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint diceNum;
        public uint diceSide;

        [D2OIgnore]
        public uint DiceNum
        {
            get
            {
                return diceNum;
            }
            set
            {
                diceNum = value;
            }
        }
        [D2OIgnore]
        public uint DiceSide
        {
            get
            {
                return diceSide;
            }
            set
            {
                diceSide = value;
            }
        }

    }}
