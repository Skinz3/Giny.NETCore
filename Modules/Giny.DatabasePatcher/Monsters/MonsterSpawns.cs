using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Monsters
{
    public class MonsterSpawns 
    {
        private static long[] NotSpawned = new long[]
        {
            1247, // Leprechaun
            793, // Bouftou d'Hallouine
            494, // Poutch Ingball
            3592, // Poutch
            3590, // Poutch
            3591, // Poutch
            3589, // Poutch d'Ombre
            3588, // Poutch Vil smisse
            3244, // Hyperscampe, Sufokia
        };

        public static void Patch()
        {
            Logger.Write("Spawning Monsters ...");

            long id = 1;

            MonsterSpawnRecord.GetMonsterSpawnRecords().ToArray().RemoveInstantElements();

            foreach (var subArea in SubareaRecord.GetSubareas())
            {
                if (subArea.MonsterIds.Length > 0)
                {
                    foreach (var monsterId in subArea.MonsterIds)
                    {
                        MonsterRecord monsterRecord = MonsterRecord.GetMonsterRecord(monsterId);

                        var spawnProbability = ComputeMonsterSpawnProbability(monsterRecord);

                        if (spawnProbability > 0)
                        {
                            MonsterSpawnRecord record = new MonsterSpawnRecord()
                            {
                                Id = id,
                                MonsterId = monsterId,
                                Probability = spawnProbability,
                                SubareaId = (short)subArea.Id,
                            };
                            id++;
                            record.AddInstantElement();
                        }
                    }
                   
                }
            }

        }

        private static double ComputeMonsterSpawnProbability(MonsterRecord record)
        {
            if (NotSpawned.Contains(record.Id))
            {
                return 0d;
            }

            if (record.IsBoss)
            {
                return 0d;
            }
            switch (record.Race)
            {
                case MonsterRacesEnum.UNCLASSIFIED:
                    return 0.5d;
                case MonsterRacesEnum.CLASS_SUMMONS:
                    break;
                case MonsterRacesEnum.BANDITS:
                    break;
                case MonsterRacesEnum.WABBITS:
                    break;
                case MonsterRacesEnum.IMMATURE_DREGGONS:
                    break;
                case MonsterRacesEnum.BWORKS:
                    break;
                case MonsterRacesEnum.GOBLINS:
                    break;
                case MonsterRacesEnum.JELLIES:
                    break;
                case MonsterRacesEnum.NIGHT_MONSTERS:
                    break;
                case MonsterRacesEnum.GOBBALLS:
                    break;
                case MonsterRacesEnum.FIELD_PLANTS:
                    break;
                case MonsterRacesEnum.LARVAE:
                    break;
                case MonsterRacesEnum.KWAKS:
                    break;
                case MonsterRacesEnum.CRACKLERS:
                    break;
                case MonsterRacesEnum.PORCOS:
                    break;
                case MonsterRacesEnum.CHAFERS:
                    break;
                case MonsterRacesEnum.TEMPLE_DOPPLES:
                    break;
                case MonsterRacesEnum.NPCS:
                    break;
                case MonsterRacesEnum.MOON_KANNIBALLS:
                    break;
                case MonsterRacesEnum.DRAGOTURKEYS:
                    break;
                case MonsterRacesEnum.TREECHNIDIANS:
                    break;
                case MonsterRacesEnum.BLOPS:
                    break;
                case MonsterRacesEnum.FIELD_ANIMALS:
                    break;
                case MonsterRacesEnum.SIDIMONSTERS:
                    break;
                case MonsterRacesEnum.GUARDS:
                    break;
                case MonsterRacesEnum.DOPPLES:
                    break;
                case MonsterRacesEnum.IMPS:
                    break;
                case MonsterRacesEnum.SEWER_MONSTERS:
                    break;
                case MonsterRacesEnum.WANTED_MONSTERS:
                    return 0.05d;
                case MonsterRacesEnum.PIWIS:
                    break;
                case MonsterRacesEnum.SCARALEAVES:
                    break;
                case MonsterRacesEnum.ARACHNEES:
                    break;
                case MonsterRacesEnum.BOOWOLVES:
                    break;
                case MonsterRacesEnum.MOON_BEACH_MONSTERS:
                    break;
                case MonsterRacesEnum.MOON_PIRATES:
                    break;
                case MonsterRacesEnum.FORBIDDEN_JUNGLE_MONSTERS:
                    break;
                case MonsterRacesEnum.CROCODYLS:
                    break;
                case MonsterRacesEnum.MUSHES:
                    break;
                case MonsterRacesEnum.TOFUS:
                    break;
                case MonsterRacesEnum.FIELD_VERMIN:
                    break;
                case MonsterRacesEnum.MUSHDS:
                    break;
                case MonsterRacesEnum.FOREST_ANIMALS:
                    break;
                case MonsterRacesEnum.QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.CROBAKS:
                    break;
                case MonsterRacesEnum.GHOSTS:
                    break;
                case MonsterRacesEnum.KOALAKS:
                    break;
                case MonsterRacesEnum.CAVE_MONSTERS:
                    break;
                case MonsterRacesEnum.CROP_PROTECTORS:
                    break;
                case MonsterRacesEnum.ORE_PROTECTORS:
                    break;
                case MonsterRacesEnum.TREE_PROTECTORS:
                    break;
                case MonsterRacesEnum.FISH_PROTECTORS:
                    break;
                case MonsterRacesEnum.PLANT_PROTECTORS:
                    break;
                case MonsterRacesEnum.MINOS:
                    break;
                case MonsterRacesEnum.KWISMAS_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.SNAPPERS:
                    break;
                case MonsterRacesEnum.GRASSY_PLAINS_MONSTERS:
                    break;
                case MonsterRacesEnum.CORAL_BEACH_MONSTERS:
                    break;
                case MonsterRacesEnum.PEAT_BOG_MONSTERS:
                    break;
                case MonsterRacesEnum.DARK_JUNGLE_MONSTERS:
                    break;
                case MonsterRacesEnum.TREE_KEEHOLO_MONSTERS:
                    break;
                case MonsterRacesEnum.OTOMAIS_ARK_MONSTERS:
                    break;
                case MonsterRacesEnum.ZOTH_VILLAGE_MONSTERS:
                    break;
                case MonsterRacesEnum.ARCHMONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.MASTOGOBS:
                    break;
                case MonsterRacesEnum.FRIGOST_VILLAGE_MONSTERS:
                    break;
                case MonsterRacesEnum.LONESOME_PINE_TRAIL_MONSTERS:
                    break;
                case MonsterRacesEnum.PINGWINS:
                    break;
                case MonsterRacesEnum.BEARBARIANS:
                    break;
                case MonsterRacesEnum.TEARS_OF_OURONIGRIDE_MONSTERS:
                    break;
                case MonsterRacesEnum.ALMAS_CRADLE_MONSTERS:
                    break;
                case MonsterRacesEnum.FANGS_OF_GLASS_MONSTERS:
                    break;
                case MonsterRacesEnum.SNOWFOUX:
                    break;
                case MonsterRacesEnum.PETRIFIED_FOREST_MONSTERS:
                    break;
                case MonsterRacesEnum.FRIGOST_WANTED_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.FRIGOST_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.SAKAI_GOBLINS:
                    break;
                case MonsterRacesEnum.BOMBS:
                    break;
                case MonsterRacesEnum.VULKANIA_MONSTERS:
                    break;
                case MonsterRacesEnum.AL_HOWIN_MONSTERS:
                    break;
                case MonsterRacesEnum.DOPPLE_SUMMONS:
                    break;
                case MonsterRacesEnum.FUNGI:
                    break;
                case MonsterRacesEnum.LEATHERBODS:
                    break;
                case MonsterRacesEnum.VULKANIA_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.KWISMAS_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.ARMARINES:
                    break;
                case MonsterRacesEnum.ALCHIMERAS:
                    break;
                case MonsterRacesEnum.MECHANIACS:
                    break;
                case MonsterRacesEnum.SINISTROS:
                    break;
                case MonsterRacesEnum.MONSTER_SUMMONS:
                    break;
                case MonsterRacesEnum.INCARNATION_SUMMONS:
                    break;
                case MonsterRacesEnum.ALLIANCE_PRISMS:
                    break;
                case MonsterRacesEnum.KRISMAHLO_ISLAND_MONSTERS:
                    break;
                case MonsterRacesEnum.ARCHBISHOPS_PALACE_MONSTER:
                    break;
                case MonsterRacesEnum.ALIGNMENT_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.MERKAPTANS:
                    break;
                case MonsterRacesEnum.EVENT_MONSTERS:
                    break;
                case MonsterRacesEnum.STONTUSK_DESERT_KANIGS:
                    break;
                case MonsterRacesEnum.OBSCURATI:
                    break;
                case MonsterRacesEnum.STRICHES:
                    break;
                case MonsterRacesEnum.LOUSY_PIGS:
                    break;
                case MonsterRacesEnum.DRHELLERS:
                    break;
                case MonsterRacesEnum.ENUTOUGHS:
                    break;
                case MonsterRacesEnum.STRONGBOXERS:
                    break;
                case MonsterRacesEnum.WANTED_DIMENSIONAL_MONSTERS:
                    return 0.05d;
                case MonsterRacesEnum.DARK_COURT:
                    break;
                case MonsterRacesEnum.MALITIAMEN:
                    break;
                case MonsterRacesEnum.NECROTICKS:
                    break;
                case MonsterRacesEnum.KROBES:
                    break;
                case MonsterRacesEnum.FUGITIVES:
                    break;
                case MonsterRacesEnum.ALTDEMONS:
                    break;
                case MonsterRacesEnum.XELOMORPHS:
                    break;
                case MonsterRacesEnum.VILINSEKTS:
                    break;
                case MonsterRacesEnum.ARAK_HAI:
                    break;
                case MonsterRacesEnum.INCARNAM_CHAFERS:
                    break;
                case MonsterRacesEnum.CELESTIAL_TEMPLE_MONSTERS:
                    break;
                case MonsterRacesEnum.INCARNAM_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.SPIRIT_FIRES:
                    break;
                case MonsterRacesEnum.INCARNAM_GOBBALLS:
                    break;
                case MonsterRacesEnum.INCARNAM_FIELD_MONSTERS:
                    break;
                case MonsterRacesEnum.SPIRITABBYS:
                    break;
                case MonsterRacesEnum.GLOOTS:
                    break;
                case MonsterRacesEnum.INCARNAM_MUSHROOMS:
                    break;
                case MonsterRacesEnum.WANTED_ALIGNMENT_MONSTERS:
                    return 0.05d;
                case MonsterRacesEnum.GREAT_GAME:
                    break;
                case MonsterRacesEnum.ECAFLEES:
                    break;
                case MonsterRacesEnum.WEREMOGGIES:
                    break;
                case MonsterRacesEnum.DEEP_SEA_RUINS_MONSTERS:
                    break;
                case MonsterRacesEnum.TRITUNS:
                    break;
                case MonsterRacesEnum.SERVANTS_OF_THE_UNSPEAKABLE:
                    break;
                case MonsterRacesEnum.WILD_SEEMYOOLS:
                    break;
                case MonsterRacesEnum.SUFOKIA_WANTED_MONSTERS:
                    return 0.05d;
                case MonsterRacesEnum.BLIBLIS:
                    break;
                case MonsterRacesEnum.IMP_AUTOMATONS:
                    break;
                case MonsterRacesEnum.ELTNEG_TROOLS:
                    break;
                case MonsterRacesEnum.CANIA_RUFFIANS:
                    break;
                case MonsterRacesEnum.CANIA_BWORKS:
                    break;
                case MonsterRacesEnum.DESERT_ANIMALS:
                    break;
                case MonsterRacesEnum.CASTUCS:
                    break;
                case MonsterRacesEnum.SANDWORMS:
                    break;
                case MonsterRacesEnum.CURSED:
                    break;
                case MonsterRacesEnum.CHASSULLIERS:
                    break;
                case MonsterRacesEnum.MAGIK_RIKTUS:
                    break;
                case MonsterRacesEnum.GHOULS:
                    break;
                case MonsterRacesEnum.ILLNIMALS:
                    break;
                case MonsterRacesEnum.SOUL_BLAZES:
                    break;
                case MonsterRacesEnum.CHARRED_ONES:
                    break;
                case MonsterRacesEnum.SUBMERGED_ONES:
                    break;
                case MonsterRacesEnum.BEACH_MONSTERS:
                    break;
                case MonsterRacesEnum.ROTCERES:
                    break;
                case MonsterRacesEnum.RHINEETLES:
                    break;
                case MonsterRacesEnum.FREEZAMMER_CLAN:
                    break;
                case MonsterRacesEnum.CHOCRUNCHERS:
                    break;
                case MonsterRacesEnum.WADDICTS:
                    break;
                case MonsterRacesEnum.ANOMALY_GUARDIANS:
                    break;
                case MonsterRacesEnum.CHOCOMANCER:
                    break;
                case MonsterRacesEnum.FLEASTER_ISLAND_QUEST_MONSTERS:
                    return 0.01d;
                case MonsterRacesEnum.CLASS_SUMMONS_OSAMODAS:
                    break;
                case MonsterRacesEnum.DRAGOSS:
                    break;
                case MonsterRacesEnum.PROTECTIVE_DREGGONS:
                    break;
                case MonsterRacesEnum.CROCUZKO_CROCODYLS:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_ENUTROF:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_PANDAWA:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_ECAFLIP:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_OUGINAK:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_SADIDA:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_FECA:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_ENIRIPSA:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_CRA:
                    break;

            }
            return 1d;
        }
    }
}
