using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Sequences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Giny.World.Managers.Fights.Marks.Rune;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_TriggerRune)]
    public class TriggerRune : SpellEffectHandler
    {
        public TriggerRune(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Rune rune = Source.GetMarks<Rune>().Where(x => x.CenterCell == TargetCell).FirstOrDefault();

            if (rune != null)
            {
                using (Source.Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP))
                {
                    Fighter target = Source.Fight.GetFighter(rune.CenterCell.Id);
                    rune.Trigger(target, MarkTriggerType.None);
                }
            }
        }
    }
}
