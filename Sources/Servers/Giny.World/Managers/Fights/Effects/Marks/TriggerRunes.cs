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
    [SpellEffectHandler(EffectsEnum.Effect_TriggerRunes)]
    public class TriggerRune : SpellEffectHandler
    {
        public TriggerRune(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var cells = GetAffectedCells();

            IEnumerable<Rune> runes = Source.GetMarks<Rune>().Where(x => x.ContainsCell(x.CenterCell.Id));

            using (Source.Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP))
            {
                foreach (var rune in runes.ToArray())
                {
                    Fighter target = Source.Fight.GetFighter(rune.CenterCell.Id);
                    rune.Trigger(target, MarkTriggerType.None);
                }
            }
        }
    }
}
