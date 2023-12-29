using Giny.Core;
using Giny.Core.Extensions;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.Protocol.Enums;
using Giny.World.Api;
using Giny.World.Managers.Experiences;
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
        const double UpperBoundDropRateMonster = 0.15d; // 0.12

        const double LowerBoundsDropRateMonster = 0.07d; // 0.04d

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

        /* public double ComputeMonsterDropProbability(short level)
         {
                 level = (short)Math.Min(200, level);

             const double Base = 0.99d;

             return UpperBoundDropRateMonster +
                 (Math.Pow(Base, level - 1) - 1) *
                 ((LowerBoundsDropRateMonster - UpperBoundDropRateMonster) /
                 (Math.Pow(Base, 199d) - 1d));
         } */

        public double ComputeMonsterDropProbability(short level)
        {
            const double LevelMax = (double)ExperienceManager.MaxLevel;

            level = (short)Math.Min(LevelMax, level);

            const double FalloffCenter = 70;

            // plus la valeur est grande, plus la chute courbe est adoucie
            const double FalloffIntensity = 30;

            var a = UpperBoundDropRateMonster;

            var b = (LowerBoundsDropRateMonster - UpperBoundDropRateMonster);

            var c = Math.Atan(-(level - FalloffCenter) / FalloffIntensity) - Math.Atan(FalloffCenter / FalloffIntensity);

            var d = Math.Atan((FalloffCenter - LevelMax) / FalloffIntensity) - Math.Atan(FalloffCenter / FalloffIntensity);

            var res = a + b * (c / d);

            return res;
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

            Logger.Write($"{Drops.Count} monsters has drop(s). (Bounds : {Math.Round(LowerBoundsDropRateMonster * 100d, 2)}% - {Math.Round(UpperBoundDropRateMonster * 100d, 2)}%)");
        }
    }
}
