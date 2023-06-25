using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Shortcuts;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Shortcuts
{
    public class GeneralShortcutBar : ShortcutBar
    {
        public override ShortcutBarEnum BarEnum => ShortcutBarEnum.GENERAL_SHORTCUT_BAR;

        public GeneralShortcutBar(Character character) : base(character)
        {

        }
        public override List<CharacterShortcut> InitializeShortcuts()
        {
            return Character.Record.Shortcuts.FindAll(x => !(x is CharacterSpellShortcut));
        }

        public void OnItemRemoved(CharacterItemRecord obj)
        {
            var shortcut = GetItemShortcut(obj.UId);

            if (shortcut != null)
            {
                RemoveShortcut(shortcut.SlotId);
            }
        }

        private CharacterItemShortcut GetItemShortcut(int itemUId)
        {
            return Shortcuts.OfType<CharacterItemShortcut>().FirstOrDefault(x => x.ItemUId == itemUId);
        }
    }
}
