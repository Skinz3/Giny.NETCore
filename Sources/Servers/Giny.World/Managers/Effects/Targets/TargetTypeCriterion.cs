using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class TargetTypeCriterion : TargetCriterion
    {
        public TargetTypeCriterion(SpellTargetType type, bool caster)
        {
            TargetType = type;
            Caster = caster;
        }

        public SpellTargetType TargetType
        {
            get;
            set;
        }

        public bool Caster
        {
            get;
            set;
        }

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            if (Caster)
                actor = handler.Source;

            if (TargetType == SpellTargetType.NONE)
                // return false; note : wtf, why is there spells with TargetType = NONE ?
                return true;

            if (handler.Source == actor && (TargetType.HasFlag(SpellTargetType.SELF)
                || TargetType.HasFlag(SpellTargetType.SELF_ONLY)
                || TargetType.HasFlag(SpellTargetType.ALLY_ALL)))
                return true;

            if (TargetType.HasFlag(SpellTargetType.SELF_ONLY) && actor != handler.Source)
                return false;

            if (handler.Source.IsFriendlyWith(actor) && (handler.Source != actor || Caster))
            {
                if (TargetType == SpellTargetType.ALLY_ALL_EXCEPT_SELF || TargetType == SpellTargetType.ALLY_ALL)
                    return true;

                if ((TargetType.HasFlag(SpellTargetType.ALLY_PLAYER))
                    && (actor is CharacterFighter))
                    return true;

                if ((TargetType.HasFlag(SpellTargetType.ALLY_MONSTER)) && (actor is MonsterFighter))
                    return true;

                if (TargetType.HasFlag(SpellTargetType.ALLY_SUMMON) && (actor is SummonedFighter))
                    return true;

                if (TargetType.HasFlag(SpellTargetType.ALLY_SUMMONER) && (handler.Source is SummonedFighter) && ((SummonedFighter)handler.Source).Summoner == actor)
                    return true;

                if ((TargetType.HasFlag(SpellTargetType.ALLY_MONSTER_SUMMON) || TargetType.HasFlag(SpellTargetType.ALLY_NON_MONSTER_SUMMON))
                    && (actor is SummonedFighter))
                    return true;
            }

            if (!handler.Source.IsEnnemyWith(actor))
                return false;

            if (TargetType == SpellTargetType.ENEMY_ALL)
                return true;

            if ((TargetType.HasFlag(SpellTargetType.ENEMY_PLAYER) || TargetType.HasFlag(SpellTargetType.ENEMY_HUMAN))
                && (actor is CharacterFighter))
                return true;

            if ((TargetType.HasFlag(SpellTargetType.ENEMY_MONSTER)) && (actor is MonsterFighter))
                return true;

            if (TargetType.HasFlag(SpellTargetType.ENEMY_SUMMON) && (actor is SummonedFighter))
                return true;

            if ((TargetType.HasFlag(SpellTargetType.ENEMY_MONSTER_SUMMON) || TargetType.HasFlag(SpellTargetType.ENEMY_NON_MONSTER_SUMMON))
                && (actor is SummonedFighter))
                return true;

            return false;
        }
        public override string ToString()
        {
            return TargetType.ToString();
        }
    }
}
