using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Breeds;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Movements
{
    [SpellEffectHandler(EffectsEnum.Effect_Teleport)]
    public class Teleport : SpellEffectHandler
    {
        public Teleport(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            var targetCells = base.GetAffectedCells();

            var targetCell = targetCells.Last();

            Telefrag telefrag = Source.Teleport(Source, targetCell);

            if (telefrag != null)
            {
                CastHandler.AddTelefrag(telefrag);
            }
        }
        public override bool CanApply()
        {
            return true;// Source.Fight.IsCellFree(TargetCell); // and state criteria
        }
    }
}
