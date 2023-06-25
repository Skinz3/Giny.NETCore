using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Marks
{
    [SpellEffectHandler(EffectsEnum.Effect_Trap)]
    public class SpawnTrap : SpellEffectHandler
    {
        public SpawnTrap(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (!Source.Fight.MarkExist<Trap>(x => x.CenterCell == TargetCell))
            {
                Zone zone = Effect.GetZone();

                Color color = MarksManager.Instance.GetMarkColor(CastHandler.Cast.Spell.GetSpellEnum());

                Trap trap = new Trap(Source.Fight.PopNextMarkId(), Effect,
                    MarkTriggerType.OnMove, zone, color, Source, TargetCell, CastHandler.Cast.Spell.Record, CastHandler.Cast.Spell.Level);

                Source.Fight.AddMark(trap);
            }
        }
    }
}
