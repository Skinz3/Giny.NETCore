using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class ResistanceBuff : Buff
    {
        private short Delta
        {
            get;
            set;
        }
        public ResistanceBuff(int id, short delta,   Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id,   target,   effectHandler, dispellable, customActionId)
        {
            this.Delta = delta;
        }

        public override void Execute()
        {
            
            Target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT].Context += Delta;
            Target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT].Context += Delta;
            Target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT].Context += Delta;
            Target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT].Context += Delta;
        }

        public override void Dispell()
        {
            Target.Stats[CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT].Context -= Delta;
            Target.Stats[CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT].Context -= Delta;
            Target.Stats[CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT].Context -= Delta;
            Target.Stats[CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT].Context -= Delta;
        }

        public override short GetDelta()
        {
            return Delta;
        }
    }
}
