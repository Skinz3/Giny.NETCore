using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Buffs.SpellModifiers;
using Giny.World.Managers.Fights.Fighters;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellModification
{
    public class SpellModifiers
    {
        public Dictionary<short, Dictionary<CharacterSpellModificationTypeEnum, SpellModifier>> Modifications
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
            this.Modifications = new Dictionary<short, Dictionary<CharacterSpellModificationTypeEnum, SpellModifier>>();
        }


        public short GetModifier(short spellId, CharacterSpellModificationTypeEnum type)
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

        public void ApplySpellModification(short spellId, CharacterSpellModificationTypeEnum type, short value)
        {
            if (!Modifications.ContainsKey(spellId))
            {
                Modifications.Add(spellId, new Dictionary<CharacterSpellModificationTypeEnum, SpellModifier>());
            }

            var modifiers = Modifications[spellId];

            if (modifiers.ContainsKey(type))
            {
                modifiers[type].UpdateValue(value);
            }
            else
            {
                modifiers[type] = new SpellModifier(type, spellId, value);
            }

            Fighter.Fight.Send(new UpdateSpellModifierMessage(Fighter.Id, modifiers[type].GetCharacterSpellModification()));

        }
    }
}
