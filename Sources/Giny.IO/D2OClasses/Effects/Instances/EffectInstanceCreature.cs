using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceCreature", "")]
    public class EffectInstanceCreature : EffectInstance , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint monsterFamilyId;

        [D2OIgnore]
        public uint MonsterFamilyId
        {
            get
            {
                return monsterFamilyId;
            }
            set
            {
                monsterFamilyId = value;
            }
        }

    }}
