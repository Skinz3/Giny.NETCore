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
        private Dictionary<MonsterRecord, List<ItemRecord>> Drops = new Dictionary<MonsterRecord, List<ItemRecord>>();

        const double UpperBoundDropRateItem = 7d;

        const double LowerBoundsDropRateItem = 0.1d;

        const double UpperBoundDropRateMonster = 5d;

        const double LowerBoundsDropRateMonster = 0.5d;

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

            foreach (var monster in monsterTeam.GetFighters<MonsterFighter>(false))
            {
                if (Drops.ContainsKey(monster.Record))
                {

                    var monsterDropProbability = ComputeMonsterDropProbability(monster);

                    if (random.Next(0, 101) < monsterDropProbability)
                    {
                        continue;
                    }


                    var drops = Drops[monster.Record];

                    Dictionary<ItemRecord, double> dropRates = new Dictionary<ItemRecord, double>();

                    foreach (var item in drops)
                    {
                        dropRates.Add(item, ComputeItemDropProbability(item) / drops.Count);
                    }

                    Dictionary<ItemRecord, double> results = new Dictionary<ItemRecord, double>();

                    foreach (var pair in dropRates)
                    {
                        var chance = random.Next(0, 100 + 1);

                        if (chance < pair.Value)
                        {
                            results.Add(pair.Key, pair.Value);
                        }
                    }


                    if (results.Count > 0)
                    {
                        var droppedItem = results.OrderBy(x => x.Value).First().Key;
                        result.Character.Inventory.AddItem((short)droppedItem.Id, 1);
                        result.Loot.AddItem((short)droppedItem.Id, 1);
                    }


                }
            }

        }

        private double ComputeItemDropProbability(ItemRecord item)
        {
            double a = (LowerBoundsDropRateItem - UpperBoundDropRateItem) / 199d;
            double b = UpperBoundDropRateItem - a;

            double level = Math.Min(200d, item.Level);

            return (a * level) + b;
        }

        private double ComputeMonsterDropProbability(MonsterFighter monster)
        {
            double a = (LowerBoundsDropRateMonster - UpperBoundDropRateMonster) / 199d;
            double b = UpperBoundDropRateMonster - a;

            double level = Math.Min(200d, monster.Level);

            return (a * level) + b;
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
                        var ingredients = recipe.Ingredients.Select(x => ItemRecord.GetItem(x)).ToArray();

                        var max = ingredients.OrderByDescending(x => x.Level).FirstOrDefault();

                        if (max.Id != gid)
                        {
                            continue;
                        }

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
                            Drops.TryAdd(monster, new List<ItemRecord>() { item });
                        }
                        else
                        {
                            if (!Drops[monster].Contains(item))
                            {
                                Drops[monster].Add(item);
                            }
                        }

                    }
                }
            }

            Logger.Write(Drops.Count + " monsters has drop(s).");
        }
    }
}
