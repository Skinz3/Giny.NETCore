using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public class SpellModifierMaxRange : SpellModifier
    {
        public SpellModifierMaxRange(short spellId) : base(spellId)
        {
        }

        public override SpellModifierTypeEnum Type => SpellModifierTypeEnum.RANGE_MAX;

        public override SpellModifierActionTypeEnum Action => SpellModifierActionTypeEnum.ACTION_BOOST;

        public override bool RequiresDeletion()
        {
            return Value == 0;
        }

        public override void Update(short value)
        {
            Value += value;
        }
    }
}
