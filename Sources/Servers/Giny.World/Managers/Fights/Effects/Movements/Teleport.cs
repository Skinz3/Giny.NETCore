using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
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


        private CellRecord? GetTeleportCell()
        {
            var targetCells = base.GetAffectedCells();

            if (targetCells.Count == 0)
            {
                return null;
            }
            if (targetCells.Count == 1)
            {
                return targetCells.First();
            }


           
            for (int i = targetCells.Count - 1; i >= 0; i--)
            {
                var cellFree = Source.Fight.IsCellFree(targetCells[i]);

                if (cellFree)
                {
                    return targetCells[i];
                }
            }

            return null;
        }
        protected override void Apply(IEnumerable<Fighter> targets)
        {
            CellRecord? targetCell = GetTeleportCell();

            if (targetCell == null)
            {
                Source.Fight.Warn("Teleport cell could not be computed from RawZone...");
                return;
            }


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
