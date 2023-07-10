using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public class SpellModifierLOS : SpellModifier
    {
        public SpellModifierLOS(short spellId) : base(spellId)
        {
        }

        public override SpellModifierTypeEnum Type => SpellModifierTypeEnum.LOS;

        public override bool RequiresDeletion()
        {
            return Value == 1;
        }

        public override SpellModifierActionTypeEnum Update(short value)
        {
            Value = value;
            return SpellModifierActionTypeEnum.ACTION_SET;
        }
    }
}
