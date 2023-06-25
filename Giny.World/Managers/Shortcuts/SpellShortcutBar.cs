using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Shortcuts
{
    public class SpellShortcutBar : ShortcutBar
    {
        public SpellShortcutBar(Character character) : base(character)
        {
        }

        public override ShortcutBarEnum BarEnum
        {
            get
            {
                return ShortcutBarEnum.SPELL_SHORTCUT_BAR;
            }
        }

        public override List<CharacterShortcut> InitializeShortcuts()
        {
            return Character.Record.Shortcuts.FindAll(x => x is CharacterSpellShortcut);
        }

        public void Remove(short spellId)
        {
            var shortcuts = GetShortcuts(spellId);

            foreach (var shortcut in shortcuts)
            {
                Shortcuts.Remove(shortcut);
                Character.Record.Shortcuts.Remove(shortcut);
                Character.Client.Send(new ShortcutBarRemovedMessage((byte)BarEnum, shortcut.SlotId));
            }
        }
        public void Add(short spellId)
        {
            byte? slotId = GetFreeSlotId();

            if (slotId != null)
            {
                base.Add(new CharacterSpellShortcut(slotId.Value, spellId));
            }
        }
        public void UpdateVariantShortcut(short spellId, short variantSpellId)
        {
            var shortcuts = GetShortcuts(spellId);

            foreach (var shortcut in shortcuts)
            {
                shortcut.SpellId = variantSpellId;
                base.RefreshShortcut(shortcut);
            }
        }
        public IEnumerable<CharacterSpellShortcut> GetShortcuts(short spellId)
        {
            return Shortcuts.OfType<CharacterSpellShortcut>().Where(x => x.SpellId == spellId);
        }


    }
}
