using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.SpellTree
{
    internal class PopupHelper
    {
        public static bool IsDisplayed(EffectDice effect)
        {
            if (effect.IsSpellCastEffect())
            {
                return false;
            }

            if (effect.EffectEnum == EffectsEnum.Effect_NoMoreEffects)
            {
                return false;
            }

            return true;
        }

        public static string GetDescription(EffectDice effect)
        {
            var content = $"({effect.Min},{effect.Max},{effect.Value})";

            switch (effect.EffectEnum)
            {
                case EffectsEnum.Effect_Summon:
                case EffectsEnum.Effect_SummonSlave:
                case EffectsEnum.Effect_KillAndSummon:
                case EffectsEnum.Effect_KillAndSummonSlave:
                    MonsterRecord monster = MonsterRecord.GetMonsterRecord((short)effect.Min);
                    content = $"{monster.ToString()} ({effect.Max})";
                    break;

                case EffectsEnum.Effect_AddState:
                case EffectsEnum.Effect_DisableState:
                case EffectsEnum.Effect_DispelState:
                    SpellStateRecord state = SpellStateRecord.GetSpellStateRecord((short)effect.Value);
                    content = $"{state}";
                    break;
            }
           
            return effect.EffectEnum + " - " + content;


        }
    }
}