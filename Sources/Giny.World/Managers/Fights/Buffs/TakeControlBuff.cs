using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class TakeControlBuff : Buff
    {
        public TakeControlBuff(int id, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
        }

        public override void Execute()
        {
            ((SummonedFighter)Target).SetController((CharacterFighter)Cast.Source);
        }

        public override void Dispell()
        {
            ((SummonedFighter)Target).RemoveController(); 
        }

        public override short GetDelta()
        {
            return 0;
        }
    }
}
