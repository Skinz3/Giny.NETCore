using Giny.Core.Pool;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Shortcuts;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Shortcuts
{
    public abstract class ShortcutBar
    {
        protected List<CharacterShortcut> Shortcuts
        {
            get;
            private set;
        }
        public abstract ShortcutBarEnum BarEnum
        {
            get;
        }
        protected Character Character
        {
            get;
            private set;
        }
        protected UniqueIdProvider IdProvider
        {
            get;
            private set;
        }
        public ShortcutBar(Character character)
        {
            this.Character = character;
            this.Shortcuts = InitializeShortcuts();
        }
        private CharacterShortcut GetShortcut(byte slotId)
        {
            return Shortcuts.FirstOrDefault(x => x.SlotId == slotId);
        }
        public void RemoveShortcut(byte slotId)
        {
            var shortcut = GetShortcut(slotId);

            if (shortcut != null)
            {
                Shortcuts.Remove(shortcut);
                Character.Record.Shortcuts.Remove(shortcut);
                Character.Client.Send(new ShortcutBarRemovedMessage((byte)BarEnum, slotId));
            }
        }

        public void Add(CharacterShortcut scut)
        {
            if (!CanAdd())
                return;
            var shortcut = GetShortcut(scut.SlotId);

            if (shortcut != null)
                RemoveShortcut(shortcut.SlotId);

            Character.Record.Shortcuts.Add(scut);
            Shortcuts.Add(scut);
            RefreshShortcut(scut);
        }
        public void Swap(byte firstSlot, byte secondSlot)
        {
            var shortcut1 = GetShortcut(firstSlot);
            var shortcut2 = GetShortcut(secondSlot);

            if (shortcut1 != null && shortcut2 != null)
            {
                shortcut1.SlotId = shortcut1.SlotId == firstSlot ? secondSlot : firstSlot;
                shortcut2.SlotId = shortcut2.SlotId == firstSlot ? secondSlot : firstSlot;
            }
            else if (shortcut1 != null)
            {
                shortcut1.SlotId = secondSlot;
            }

            Refresh();
        }

        protected void RefreshShortcut(CharacterShortcut shortcut)
        {
            Character.Client.Send(new ShortcutBarRefreshMessage((byte)BarEnum, shortcut.GetShortcut()));
        }

        public void Refresh()
        {
            Character.Client.Send(new ShortcutBarContentMessage((byte)BarEnum, Shortcuts.Select(x => x.GetShortcut()).ToArray()));
        }

        public abstract List<CharacterShortcut> InitializeShortcuts();


        public static CharacterShortcut GetCharacterShortcut(Character character, Shortcut shortcut)
        {
            if (shortcut is ShortcutObjectItem)
            {
                var shortcutObjectItem = shortcut as ShortcutObjectItem;

                var item = character.Inventory.GetItem(shortcutObjectItem.itemUID);
                if (item != null)
                {
                    return new CharacterItemShortcut(shortcut.slot, shortcutObjectItem.itemUID, (short)item.GId);
                }
                else
                {
                    return null;
                }
            }
            else if (shortcut is ShortcutSpell)
            {
                var shortcutSpell = shortcut as ShortcutSpell;

                return new CharacterSpellShortcut(shortcutSpell.slot, shortcutSpell.spellId);
            }
            else if (shortcut is ShortcutEmote)
            {
                var shortcutEmote = shortcut as ShortcutEmote;

                return new CharacterEmoteShortcut(shortcutEmote.slot, shortcutEmote.emoteId);
            }
            else if (shortcut is ShortcutSmiley)
            {
                return new CharacterSmileyShortcut(((ShortcutSmiley)shortcut).smileyId, shortcut.slot);
            }
            return null;
        }

        public byte? GetFreeSlotId()
        {
            for (byte i = 0; i < 100; i++)
            {
                if (Shortcuts.Any(x=>x.SlotId == i))
                {
                    continue;
                }
                else
                {
                    return i;
                }
            }
            return null;
        }
        public bool CanAdd()
        {
            return GetFreeSlotId() != null;
        }
    }
}
