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
    [SpellEffectHandler(EffectsEnum.Effect_SpawnPortal)]
    public class SpawnPortal : SpellEffectHandler
    {
        public SpawnPortal(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (!Source.Fight.MarkExist<Portal>(x => x.CenterCell == TargetCell))
            {

                IEnumerable<Portal> teamPortals = Source.Fight.GetMarks<Portal>().Where(x => x.Source.IsFriendlyWith(Source));

                if (teamPortals.Count() >= PortalManager.MaxPortalPerTeam)
                {
                    var removedPortal = teamPortals.First();
                    Source.Fight.RemoveMark(removedPortal);
                }

                Zone zone = Effect.GetZone();

                Color color = PortalManager.Instance.GetPortalColor(Source.Team.TeamId);

                Portal portal = new Portal(Source.Fight.PopNextMarkId(), (short)Effect.Value, Effect,
                     zone, MarkTriggerType.OnMove, color, Source, TargetCell, CastHandler.Cast.Spell, Effect.GetSpell());

                Source.Fight.AddMark(portal);
            }
        }



    }
}
