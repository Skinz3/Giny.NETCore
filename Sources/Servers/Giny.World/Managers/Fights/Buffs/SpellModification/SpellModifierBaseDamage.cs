using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public class SpellModifierBaseDamage : SpellModifier
    {
        public SpellModifierBaseDamage(short spellId) : base(spellId)
        {
        }

        public override SpellModifierTypeEnum Type => SpellModifierTypeEnum.BASE_DAMAGE;

        public override bool RequiresDeletion()
        {
            return this.Value == 0;
        }

        public override SpellModifierActionTypeEnum Update(short value)
        {
            Value += value;

            if (value > 0)
            {
                return SpellModifierActionTypeEnum.ACTION_BOOST;
            }
            else
            {
                return SpellModifierActionTypeEnum.ACTION_DEBOOST;
            }
        }
    }
}
