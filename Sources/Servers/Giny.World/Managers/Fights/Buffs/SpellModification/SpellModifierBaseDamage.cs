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
        public override SpellModifierTypeEnum Type => SpellModifierTypeEnum.BASE_DAMAGE;

        public override SpellModifierActionTypeEnum Action => SpellModifierActionTypeEnum.ACTION_BOOST;

        public SpellModifierBaseDamage(short spellId) : base(spellId)
        {

        }

        public override bool RequiresDeletion()
        {
            return this.Value == 0;
        }

        public override void Update(short value)
        {
            Value += value;
        }
    }
}
