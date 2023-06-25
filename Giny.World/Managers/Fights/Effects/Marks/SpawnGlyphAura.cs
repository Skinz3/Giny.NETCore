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

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_GlyphAura)]
    public class SpawnGlyphAura : SpellEffectHandler
    {
        public SpawnGlyphAura(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (!Source.Fight.MarkExist<GlyphAura>(x => x.CenterCell == TargetCell))
            {
                Zone zone = Effect.GetZone();
                Color color = MarksManager.Instance.GetMarkColor(CastHandler.Cast.Spell.GetSpellEnum());

                GlyphAura glyph = new GlyphAura(Source.Fight.PopNextMarkId(), Effect,
                   zone, MarkTriggerType.OnMove, color, Source, TargetCell, CastHandler.Cast.Spell.Record, CastHandler.Cast.Spell.Level);

                Source.Fight.AddMark(glyph);
            }
        }
    }
}
