using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldView
{
    internal class SpellHelper
    {
        private const string UnknownDataText = "Aucune données.";
        public static string GetRequiredStatesNames(SpellLevelRecord level)
        {
            return string.Join(',', level.StatesRequired.Select(x => SpellStateRecord.GetSpellStateRecord(x)));
        }
        public static string GetForbiddenStatesNames(SpellLevelRecord level)
        {
            return string.Join(',', level.StatesForbidden.Select(x => SpellStateRecord.GetSpellStateRecord(x)));
        }
        public static string GetSpellStateName(EffectDice effect)
        {
            var state = SpellStateRecord.GetSpellStateRecord(effect.Value);

            if (state == null)
            {
                return UnknownDataText;
            }
            else
            {
                return state.ToString();
            }
        }
        public static string GetSummonedMonsterName(EffectDice effect)
        {
            MonsterRecord monster = MonsterRecord.GetMonsterRecord((short)effect.Min);

            if (monster == null)
            {
                return UnknownDataText;
            }
            else
            {
                return monster.ToString();
            }
        }
        public static bool IsSummonEffect(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_Summon:
                case EffectsEnum.Effect_SummonSlave:
                    return true;
            }

            return false;
        }
      
        public static string GetDebuffedSpellName(EffectDice effect)
        {
            SpellRecord spell = SpellRecord.GetSpellRecord((short)effect.Value);

            if (spell != null)
            {
                return spell.ToString();
            }
            else
            {
                return UnknownDataText;
            }
        }
        public static bool IsSpellCastEffect(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_TargetExecuteSpellWithAnimation:
                case EffectsEnum.Effect_TargetExecuteSpell:
                case EffectsEnum.Effect_TargetExecuteSpellOnCell:
                case EffectsEnum.Effect_CasterExecuteSpellGlobalLimitation:
                case EffectsEnum.Effect_CastSpell_1175:
                case EffectsEnum.Effect_CasterExecuteSpell:
                case EffectsEnum.Effect_SourceExecuteSpellOnSource:
                case EffectsEnum.Effect_SourceExecuteSpellOnTarget:
                case EffectsEnum.Effect_TargetExecuteSpellOnSource:
                case EffectsEnum.Effect_TargetExecuteSpellGlobalLimitation:
                case EffectsEnum.Effect_TargetExecuteSpellOnSourceGlobalLimitation:
                case EffectsEnum.Effect_Trap:
                case EffectsEnum.Effect_CasterExecuteSpellOnCell:
                    return true;
            }

            return false;
        }
        public static string TriggersToString(IEnumerable<World.Managers.Fights.Triggers.Trigger> triggers)
        {
            string result = string.Empty;

            foreach (var trigger in triggers)
            {
                result += trigger.Type;

                if (trigger.Value.HasValue)
                {
                    result += " (" + trigger.Value + ")";
                }

                if (trigger != triggers.Last())
                    result += ",";
            }

            return result;
        }
        public static string GetTargets(EffectDice effect)
        {
            string targets = string.Join(",", effect.GetTargets());

            if (targets == string.Empty)
            {
                targets = "ALL";
            }

            return targets;
        }
        public static string GetTargetSpellName(EffectDice effect)
        {
            SpellRecord spell = SpellRecord.GetSpellRecord((short)effect.Min);

            if (spell != null)
            {
                return spell.ToString();
            }
            else
            {
                return UnknownDataText;
            }
        }
    }
}
