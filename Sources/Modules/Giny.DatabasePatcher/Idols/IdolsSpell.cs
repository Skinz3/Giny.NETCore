using Giny.Core;
using Giny.ORM;
using Giny.World.Records.Idols;
using Giny.World.Records.Items;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Idols
{
    class IdolsSpell
    {
        public static void Patch()
        {
            Logger.Write("Fixing Idols spell ...");

            foreach (var idol in IdolRecord.GetIdolRecords())
            {
                var itemRecord = ItemRecord.GetItem(idol.ItemId);

                var name = itemRecord.Name;

                var splitted = name.Split(null);

                var spellName = splitted[0];

                var spell = SpellRecord.GetSpellRecord(spellName);

                var items = ItemRecord.GetItems().Where(x => x.Name.StartsWith(spell.Name));

                if (items.Count() > 1)
                {
                    if (splitted.Count() == 1)
                    {
                        idol.SpellLevelId = spell.Levels.First().Id;
                    }
                    else
                    {
                        if (spell.Levels.Count == 4)
                        {
                            switch (splitted[1])
                            {
                                case "mineure":
                                    idol.SpellLevelId = spell.Levels[0].Id;
                                    break;
                                case "majeure":
                                    idol.SpellLevelId = spell.Levels[2].Id;
                                    break;
                                case "magistrale":
                                    idol.SpellLevelId = spell.Levels[3].Id;
                                    break;
                            }
                        }
                        else
                        {
                            Logger.Write("Unable to bind idol... (" + itemRecord.Name + ")", Channels.Warning);
                        }

                    }
                }
                else
                {
                    idol.SpellLevelId = spell.Levels[0].Id;
                }

                idol.UpdateInstantElement();
            }

            IdolRecord.Initialize();

        }
    }
}
