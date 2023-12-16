using Giny.Core.Extensions;
using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class SummonAction : AIAction
    {
        public SummonAction(AIFighter fighter) : base(fighter)
        {

        }


        protected override void Apply()
        {
            if (!Fighter.CanSummon())
            {
                return;
            }

            foreach (var spellRecord in GetSpells().Where(x => x.Category == SpellCategoryEnum.Summon).Shuffle())
            {
                MapPoint targetPoint = GetTargetPoint(spellRecord.Id, x => Fighter.Fight.IsCellFree(x.CellId));

                if (targetPoint != null)
                {
                    var fighterSpell = Fighter.GetSpell(spellRecord.Id);

                    var isReviveEffect = fighterSpell.Level.Effects.Any(x => x.EffectEnum == EffectsEnum.Effect_ReviveAlly ||
                    x.EffectEnum == EffectsEnum.Effect_ReviveAlly_1034 || x.EffectEnum == EffectsEnum.Effect_ReviveAndGiveHPToLastDiedAlly);

                    if (isReviveEffect && Fighter.Team.GetLastDeadFighter() == null)
                    {
                        continue;
                    }

                    Fighter.CastSpell(spellRecord.Id, targetPoint.CellId);
                }
            }
        }
    }
}