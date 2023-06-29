using Giny.Core;
using Giny.World.Api;
using Giny.World.Managers.Entities.Monsters;
using Giny.World.Modules;
using Giny.World.Records.Items;
using Giny.World.Records.Jobs;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AdditionalDrop
{
    [Module("Craftable Drop")]
    public class Module : IModule
    {
        public void CreateHooks()
        {

        }

        public void Initialize()
        {
            int count = 0;

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


                        MonsterDrop newDrop = new MonsterDrop();
                        newDrop.RollsCounter = 1;
                        newDrop.DropLimit = 1;
                        newDrop.ItemGId = (int)recipe.ResultId;
                        newDrop.ProspectingLock = 100;
                        newDrop.PercentDropForGrade1 = 0.5;
                        newDrop.PercentDropForGrade2 = 0.6;
                        newDrop.PercentDropForGrade3 = 1.2;
                        newDrop.PercentDropForGrade4 = 1.4;
                        newDrop.PercentDropForGrade5 = 1.5;
                            
                        monster.Drops.Add(newDrop);
                        count++;

                    }
                }
            }

            Logger.Write(count + " craftable drops added to monsters.");
        }
    }
}
