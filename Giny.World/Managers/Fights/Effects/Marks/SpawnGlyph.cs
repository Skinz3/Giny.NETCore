using Giny.Protocol.Custom.Enums;
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
    [SpellEffectHandler(EffectsEnum.Effect_TurnEndGlyph)]
    [SpellEffectHandler(EffectsEnum.Effect_TurnBeginGlyph)]
    public class SpawnGlyph : SpellEffectHandler
    {
        public SpawnGlyph(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Zone zone = Effect.GetZone();

            Color color = MarksManager.Instance.GetMarkColor(CastHandler.Cast.Spell.GetSpellEnum());

            Glyph glyph = new Glyph(Source.Fight.PopNextMarkId(), Effect,
                 zone, GetTriggerType(), color, Source, TargetCell, CastHandler.Cast.Spell.Record, CastHandler.Cast.Spell.Level);

            Source.Fight.AddMark(glyph);
        }

        private MarkTriggerType GetTriggerType()
        {
            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_TurnBeginGlyph:
                    return MarkTriggerType.OnTurnBegin;
                case EffectsEnum.Effect_TurnEndGlyph:
                    return MarkTriggerType.OnTurnEnd;
            }

            return MarkTriggerType.None;
        }
    }
}
