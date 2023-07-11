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
        public override SpellModifierTypeEnum Type => SpellModifierTypeEnum.LOS;

        public override SpellModifierActionTypeEnum Action => SpellModifierActionTypeEnum.ACTION_SET;

        public SpellModifierLOS(short spellId) : base(spellId)
        {
        }

      
        public override bool RequiresDeletion()
        {
            return Value == 1;
        }

        public override void Update(short value)
        {
            Value = value;
        }
    }
}
