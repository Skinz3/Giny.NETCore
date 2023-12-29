using Giny.Core;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
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
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AdditionalDrop
{
    [Module("Craftable Drop")]
    public class CraftableDropModule : IModule
    {
        private Dictionary<MonsterRecord, List<ItemRecord>> Drops = new Dictionary<MonsterRecord, List<ItemRecord>>();

        /// <summary>
        /// Pondération (unitée relative)
        /// </summary>
        const double UpperBoundDropWeightItem = 10d;

        const double LowerBoundDropWeightItem = 0.2d;

        /// <summary>
        /// Pourcentage (unitée fixe)
        /// </summary>
        const double UpperBoundDropRateMonster = 0.10d;

        const double LowerBoundsDropRateMonster = 0.01d;

        public List<ItemRecord> GetDrops(MonsterRecord monster)
        {
            return Drops.ContainsKey(monster) ? Drops[monster] : new List<ItemRecord>();
        }
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

            var monsterTeam = result.Fight.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

            foreach (var monster in monsterTeam.GetFighters<MonsterFighter>(false))
            {
                // Monster has craftable drop

                if (Drops.ContainsKey(monster.Record))
                {
                    // first pass
                    double monsterDropProbability = ComputeMonsterDropProbability(monster.Level);

                    if (result.Fighter.Random.NextDouble() > monsterDropProbability)
                    {
                        continue;
                    }

                    else
                    {
                        PerformDrop(result, monster);
                    }

                }
            }
        }

        private void PerformDrop(FightPlayerResult result, MonsterFighter monster)
        {

            // Liste des items droppable sur un monstre
            List<ItemRecord> drops = Drops[monster.Record];

            Dictionary<ItemRecord, double> dropRates = new Dictionary<ItemRecord, double>();


            // Calcul des taux de pondération pour chaque item 

            foreach (var item in drops)
            {
                dropRates.Add(item, ComputeItemDropWeight(item));
            }

            // Calcul de la somme des taux

            double weightTotal = dropRates.Sum(x => x.Value);

            // Normalization des taux 

            foreach (var item in dropRates.Keys)
            {
                dropRates[item] = dropRates[item] / weightTotal;
            }


            double num = result.Fighter.Random.NextDouble();


            double dropRangeMax = 0;

            foreach (var pair in dropRates)
            {
                dropRangeMax += pair.Value;

                if (num <= dropRangeMax)
                {
                    var droppedItem = pair.Key;
                    result.Character.Inventory.AddItem((short)droppedItem.Id, 1);
                    result.Loot.AddItem((short)droppedItem.Id, 1);
                    break;
                }

            }
        }

        public double ComputeItemDropWeight(ItemRecord item)
        {
            double a = (LowerBoundDropWeightItem - UpperBoundDropWeightItem) / 199d;
            double b = UpperBoundDropWeightItem - a;

            double level = Math.Min(200d, item.Level);

            return (a * level) + b;
        }

        public double ComputeMonsterDropProbability(short monsterLevel)
        {
            double a = (LowerBoundsDropRateMonster - UpperBoundDropRateMonster) / 199d;
            double b = UpperBoundDropRateMonster - a;

            double level = Math.Min(200d, monsterLevel);

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

                        monster.Drops.RemoveAll(x => x.ItemGId == recipe.ResultId);

                        var item = ItemRecord.GetItem(recipe.ResultId);

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
