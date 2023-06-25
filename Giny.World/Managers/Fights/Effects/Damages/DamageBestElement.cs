using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Stats;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [WIP("three effects... Same handler? weird")]
    [SpellEffectHandler(EffectsEnum.Effect_DamageBestElement_2829)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageBestElement_2828)] // Concentration de Chakra
    [SpellEffectHandler(EffectsEnum.Effect_DamageBestElement)] // Intimidation
    public class DamageBestElement : SpellEffectHandler
    {
        protected override bool Reveals => true;

        public DamageBestElement(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var fighter in targets)
            {
                fighter.InflictDamage(CreateDamage(fighter));
            }
        }
        private Damage CreateDamage(Fighter target)
        {
            return new Damage(Source, target, GetEffectSchool(), (short)Effect.Min, (short)Effect.Max, this);
        }

        public EffectSchoolEnum GetEffectSchool()
        {
            Dictionary<EffectSchoolEnum, Characteristic> values = new Dictionary<EffectSchoolEnum, Characteristic>
            {
                { EffectSchoolEnum.Earth,Source.Stats.Strength },
                { EffectSchoolEnum.Fire, Source.Stats.Intelligence },
                { EffectSchoolEnum.Air,  Source.Stats.Agility },
                { EffectSchoolEnum.Water,Source.Stats.Chance },
            };

            EffectSchoolEnum result = values.OrderByDescending(x => x.Value.TotalInContext()).First().Key; // context of context free ?

            return result;
        }
    }
}
