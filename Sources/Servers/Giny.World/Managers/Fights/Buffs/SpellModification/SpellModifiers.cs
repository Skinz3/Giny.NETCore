using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public class SpellModifiers
    {
        public List<SpellModifier> Modifiers
        {
            get;
            private set;
        }

        private Fighter Fighter
        {
            get;
            set;
        }
        public SpellModifiers(Fighter fighter)
        {
            this.Fighter = fighter;
            this.Modifiers = new List<SpellModifier>();
        }



        public short? GetModifierSet(short spellId, SpellModifierTypeEnum type)
        {
            var set = Modifiers.LastOrDefault(x => x.SpellId == spellId &&
            type == x.Type && x.Action == SpellModifierActionTypeEnum.ACTION_SET);

            if (set != null)
            {
                return set.Value;
            }

            return null;
        }
        public short GetModifierBoost(short spellId, SpellModifierTypeEnum type)
        {
            var modifiers = Modifiers.Where(x => x.SpellId == spellId && x.Type == type);

            if (modifiers.Count() == 0)
            {
                return 0;
            }


            int boosts = modifiers.Where(x => x.Action == SpellModifierActionTypeEnum.ACTION_BOOST).Sum(x => x.Value);

            int deboosts = modifiers.Where(x => x.Action == SpellModifierActionTypeEnum.ACTION_DEBOOST).Sum(x => x.Value);


            return (short)(boosts - deboosts);
        }

        private void RemoveSpellModification(SpellModifier modifier)
        {
            Modifiers.Remove(modifier);

            Fighter.Fight.Send(new RemoveSpellModifierMessage(Fighter.Id,
                  (byte)modifier.Action, (byte)modifier.Type, modifier.SpellId));
        }


        public void ApplySpellModification(short spellId, SpellModifierTypeEnum type, SpellModifierActionTypeEnum action, short value)
        {
            var previous = Modifiers.FirstOrDefault(x => x.SpellId == spellId && x.Type == type && x.Action == action);

            if (previous != null)
            {
                if (action == SpellModifierActionTypeEnum.ACTION_SET && Math.Abs(value) != Math.Abs(previous.Value))
                {
                    throw new NotImplementedException("Modification set overlap for spellId " + spellId);
                }


                var result = previous.Update(value);

                if (result == SpellModifierUpdateResult.RequiresDeletion)
                {
                    RemoveSpellModification(previous);
                }
                else if (result == SpellModifierUpdateResult.Ok)
                {
                    Fighter.Fight.Send(new ApplySpellModifierMessage(Fighter.Id, previous.GetSpellModifierMessage()));
                }

                return;
            }
            else
            {
                SpellModifier modifier = new SpellModifier(spellId, type, action);
                modifier.Update(value);
                Modifiers.Add(modifier);
                Fighter.Fight.Send(new ApplySpellModifierMessage(Fighter.Id, modifier.GetSpellModifierMessage()));
            }

        }
    }
}
