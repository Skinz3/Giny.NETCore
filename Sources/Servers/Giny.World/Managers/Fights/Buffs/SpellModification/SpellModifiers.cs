using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
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
        public Dictionary<short, Dictionary<SpellModifierTypeEnum, SpellModifier>> Modifications
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
            this.Modifications = new Dictionary<short, Dictionary<SpellModifierTypeEnum, SpellModifier>>();
        }


        public short GetModifier(short spellId, SpellModifierTypeEnum type)
        {
            if (!Modifications.ContainsKey(spellId))
            {
                return 0;
            }

            var modifiers = Modifications[spellId];

            if (modifiers.ContainsKey(type))
            {
                return modifiers[type].Value;
            }
            else
            {
                return 0;
            }
        }

        public void ApplySpellModification(short spellId, SpellModifierTypeEnum type, short value)
        {
            if (!Modifications.ContainsKey(spellId))
            {
                Modifications.Add(spellId, new Dictionary<SpellModifierTypeEnum, SpellModifier>());
            }

            var modifiers = Modifications[spellId];

            if (modifiers.ContainsKey(type))
            {
                UpdateModifier(modifiers[type], value);
            }
            else
            {
                modifiers[type] = CreateSpellModifier(type, spellId);
                UpdateModifier(modifiers[type], value);
            }
        }

        private void UpdateModifier(SpellModifier modifier, short value)
        {
            modifier.Update(value);

            if (modifier.RequiresDeletion())
            {
                Modifications[modifier.SpellId].Remove(modifier.Type);

                if (Modifications[modifier.SpellId].Count == 0)
                {
                    Modifications.Remove(modifier.SpellId);
                }

                Fighter.Fight.Send(new RemoveSpellModifierMessage(Fighter.Id,
                      (byte)modifier.Action, (byte)modifier.Type, modifier.SpellId));
            }
            else
            {
                Fighter.Fight.Send(new ApplySpellModifierMessage(Fighter.Id, modifier.GetSpellModifierMessage()));
            }
        }

        private SpellModifier CreateSpellModifier(SpellModifierTypeEnum type, short spellId)
        {
            switch (type)
            {
                case SpellModifierTypeEnum.BASE_DAMAGE:
                    return new SpellModifierBaseDamage(spellId);
                case SpellModifierTypeEnum.LOS:
                    return new SpellModifierLOS(spellId);
                case SpellModifierTypeEnum.AP_COST:
                    return new SpellModifierApCost(spellId);

            }


            throw new NotImplementedException($"Not implemented spell modifier {type}");
        }
    }
}
