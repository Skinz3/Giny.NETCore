using System.Collections.Concurrent;
using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.Core.Extensions;
using Giny.World.Records.Items;

namespace Giny.AdditionalDrop
{
    public class DropManager : Singleton<DropManager>
    {
        private const double DropChance = 18d;

        private ConcurrentBag<ItemRecord> DroppableItems = new ConcurrentBag<ItemRecord>();

        private static ItemTypeEnum[] DropTypes = new ItemTypeEnum[]
        {
                ItemTypeEnum.HAT,
                ItemTypeEnum.CLOAK,
                ItemTypeEnum.TROPHY,
                ItemTypeEnum.AMULET,
                ItemTypeEnum.RING,
                ItemTypeEnum.BOOTS,
                ItemTypeEnum.BELT,
                ItemTypeEnum.SHIELD,
                ItemTypeEnum.SHOVEL,
                ItemTypeEnum.SWORD,
                ItemTypeEnum.BOW,
                ItemTypeEnum.PET,
                ItemTypeEnum.DAGGER,
                ItemTypeEnum.HAMMER,
                ItemTypeEnum.AXE,
                ItemTypeEnum.STAFF,
                ItemTypeEnum.WAND,
        };

        public void Initialize()
        {
            DroppableItems = new ConcurrentBag<ItemRecord>(ItemRecord.GetItems().Where(x => IsDroppable(x) && DropTypes.Contains(x.TypeEnum)));

            Logger.Write("Additional loot chance : " + DropChance + "%. (" + DroppableItems.Count + " items)");
        }
        private static bool IsDroppable(ItemRecord item)
        {
            return item.Exchangeable && !item.Etheral;
        }

        public void OnPlayerResultApplied(FightPlayerResult result)
        {
            if (result.Fight.Winners != result.Fighter.Team)
            {
                return;
            }
            if (!(result.Fight is FightPvM))
            {
                return;
            }
            AsyncRandom random = new AsyncRandom();

            var chance = random.Next(0, 100) + random.NextDouble();

            var dropRate = DropChance;

            if (!(dropRate >= chance))
                return;

            var maxLevel = result.Fighter.EnemyTeam.GetFighters<MonsterFighter>(false).Max(x => x.Level);

            if (maxLevel > 200)
            {
                maxLevel = 200;
            }
            var minLevel = 1;

            if (maxLevel > 100)
            {
                minLevel = 40;
            }
            if (maxLevel > 150)
            {
                minLevel = 70;
            }
            if (maxLevel == 200)
            {
                minLevel = 100;
            }

            var item = DroppableItems.Where(x => x.Level <= maxLevel && x.Level >= minLevel).Random();

            var quantity = 1;

            result.Character.Inventory.AddItem((short)item.Id, quantity);

            result.Loot.AddItem((short)item.Id, quantity);
        }
    }
}