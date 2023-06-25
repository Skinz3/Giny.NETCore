using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Zones;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Giny.World.Managers.Fights.Marks.Rune;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_Rune)]
    public class SpawnRune : SpellEffectHandler
    {
        public SpawnRune(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var mark = Source.Fight.GetMarks<Rune>().Where(x => x.CenterCell == TargetCell).FirstOrDefault();

            if (mark != null)
            {
                Source.Fight.RemoveMark(mark);
            }

            Zone zone = Effect.GetZone();

            Color color = MarksManager.Instance.GetMarkColor(CastHandler.Cast.Spell.GetSpellEnum());

            Rune rune = new Rune(Source.Fight.PopNextMarkId(), Effect,
                zone, MarkTriggerType.None, color,
                Source, TargetCell, CastHandler.Cast.Spell.Record,
                CastHandler.Cast.Spell.Level);

            Source.Fight.AddMark(rune);
        }
    }
}
