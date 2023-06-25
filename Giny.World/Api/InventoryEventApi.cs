using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Api
{
    public class InventoryEventApi
    {
        /*
         * bool = can equip ?
         * This return value is not a good option .. (multiple event binding)
         */
        public delegate bool OnEquipItemDelegate(Character character, CharacterItemRecord item);

        public static event OnEquipItemDelegate CanEquipItem;

        public delegate void ItemMovedDelegate(Character owner, CharacterItemRecord item);

        public static event ItemMovedDelegate OnItemEquipped;

        public static event ItemMovedDelegate OnItemUnequipped;

        internal static void ItemEquipped(Character owner, CharacterItemRecord item)
        {
            OnItemEquipped?.Invoke(owner, item);
        }
        internal static void ItemUnequipped(Character owner, CharacterItemRecord item)
        {
            OnItemUnequipped?.Invoke(owner, item);
        }
        internal static bool OnItemEquipping(Character character, CharacterItemRecord item)
        {
            if (CanEquipItem != null)
            {
                return CanEquipItem.Invoke(character, item);
            }
            else
            {
                return true;
            }
        }
    }
}
