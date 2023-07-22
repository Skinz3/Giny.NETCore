using Giny.Core;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.Protocol.Enums;
using Giny.World.Api;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Monsters;
using Giny.World.Modules;
using Giny.World.Records.Items;
using Giny.World.Records.Jobs;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AdditionalDrop
{
    [Module("Craftable Drop")]
    public class Module : IModule
    {
        private ConcurrentDictionary<MonsterRecord, ConcurrentBag<ItemRecord>> Drops = new ConcurrentDictionary<MonsterRecord, ConcurrentBag<ItemRecord>>();


        public void CreateHooks()
        {
            FightEventApi.OnPlayerResultApplied += OnPlayerResultApplied;
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

            Random random = new Random();

            var monsterTeam = result.Fight.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

            List<ItemRecord> droppedItems = new List<ItemRecord>();

            foreach (var monster in monsterTeam.GetFighters<MonsterFighter>(false))
            {
                if (Drops.ContainsKey(monster.Record))
                {
                    ItemRecord item = Drops[monster.Record].Where(x => !droppedItems.Contains(x)).Random(random);

                    if (item == null)
                    {
                        continue;
                    }

                    if (RollLoot(random,result.Fighter, monster))
                    {
                        result.Character.Inventory.AddItem((short)item.Id, 1);
                        result.Loot.AddItem((short)item.Id, 1);
                        droppedItems.Add(item);
                    }

                }
            }

        }

        public bool RollLoot(Random random, CharacterFighter fighter, MonsterFighter monster)
        {
            var chance = random.Next(0, 100) + random.NextDouble();

            var dropRate = 5d;

            if (!(dropRate >= chance))
                return false;

            return true;
        }


        public void Initialize()
        {
            foreach (var monster in MonsterRecord.GetMonsterRecords())
            {
                foreach (var drop in monster.Drops.ToArray())
                {
                    var gid = (short)drop.ItemGId;

                    var recipes = RecipeRecord.GetRecipesWithItem(gid);

                    foreach (var recipe in recipes)
                    {
                        if (monster.Drops.Any(x => x.ItemGId == recipe.ResultId))
                        {
                            continue;
                        }
                        var item = ItemRecord.GetItem(recipe.ResultId);

                        if (item.DropMonsterIds.Length > 0)
                        {
                            continue;
                        }
                        if (!item.Exchangeable)
                        {
                            continue;
                        }
                        if (item.Usable)
                        {
                            continue;
                        }

                        if (!Drops.ContainsKey(monster))
                        {
                            Drops.TryAdd(monster, new ConcurrentBag<ItemRecord>() { item });
                        }
                        else
                        {
                            Drops[monster].Add(item);
                        }

                    }
                }
            }

            Logger.Write(Drops.Count + " monsters has drop(s).");
        }
    }
}
