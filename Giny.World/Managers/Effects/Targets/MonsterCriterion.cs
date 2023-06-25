using Giny.Core.DesignPattern;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    public class MonsterCriterion : TargetCriterion
    {
        public MonsterCriterion(int monsterId, bool caster, bool required)
        {
            MonsterId = monsterId;
            Required = required;
            Caster = caster;
        }

        public int MonsterId
        {
            get;
            set;
        }

        public bool Required
        {
            get;
            set;
        }

        public bool Caster
        {
            get;
            set;
        }

        public override bool IsDisjonction => Required;

        public override bool IsTargetValid(Fighter actor, SpellEffectHandler handler)
        {
            var target = actor;

            if (Caster)
            {
                target = handler.Source;
            }

            bool result = Required ? ((target is IMonster) && (target as IMonster).Record.Id == MonsterId) :
                 (!(target is IMonster) || (target as IMonster).Record.Id != MonsterId);

            return result;
        }

        public override string ToString()
        {
            MonsterRecord record = MonsterRecord.GetMonsterRecord((short)MonsterId);

            if (record == null)
            {
                return "IsNotMonster (" + MonsterId + ")";
            }
            if (Required)
            {
                return "IsMonster (" + record.Name + ")";
            }
            else
            {
                return "IsNotMonster (" + record.Name + ")";
            }
        }
    }
}
