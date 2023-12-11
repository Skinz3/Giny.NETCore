using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_SummonsBomb)]
    public class SummonBomb : SpellEffectHandler
    {
        public SummonBomb(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
            
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (Source.Fight.IsCellFree(CastHandler.Cast.BaseTargetCell))
            {
                MonsterRecord record = MonsterRecord.GetMonsterRecord((short)Effect.Min);
                SummonedMonster bombFighter = new SummonedBomb(Source, record, this, CastHandler.Cast.Spell.Level.Grade, CastHandler.Cast.BaseTargetCell);
                Source.Fight.AddSummon(Source, bombFighter);
            }
            else
            {

                SpellBombRecord spellBomb = SpellBombRecord.GetSpellBomb(Effect.Min);
                Source.ExecuteSpell(spellBomb.InstantSpellId, this.CastHandler.Cast.Spell.Level.Grade, CastHandler.Cast.BaseTargetCell);
            }
        }
    }
}
