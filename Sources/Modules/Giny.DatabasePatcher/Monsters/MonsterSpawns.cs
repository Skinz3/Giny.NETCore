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

                        if (monsterRecord == null)
                        {
                            Logger.Write($"Unknown monster {monsterId} skipping spawn...");
                            continue;
                        }
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
                case MonsterRacesEnum.AMAKNA_BWORKS:
                    break;
                case MonsterRacesEnum.AMAKNA_GOBLINS:
                    break;
                case MonsterRacesEnum.JELLIES:
                    break;
                case MonsterRacesEnum.NIGHT_BEASTS:
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
                case MonsterRacesEnum.SPHINCTER_CELLS_GANG:
                    break;
                case MonsterRacesEnum.WANTED_MONSTERS:
                    break;
                case MonsterRacesEnum.PIWIS:
                    break;
                case MonsterRacesEnum.SCARALEAVES:
                    break;
                case MonsterRacesEnum.ARACHNEES:
                    break;
                case MonsterRacesEnum.BOOWOLVES:
                    break;
                case MonsterRacesEnum.MOON_TURTLES:
                    break;
                case MonsterRacesEnum.MOON_PIRATES:
                    break;
                case MonsterRacesEnum.FORBIDDEN_JUNGLE_MONSTERS:
                    break;
                case MonsterRacesEnum.SWAMPSTERS:
                    break;
                case MonsterRacesEnum.SPIMUSHES:
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
                    break;
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
                    break;
                case MonsterRacesEnum.SNAPPERS:
                    break;
                case MonsterRacesEnum.HERBOREALS:
                    break;
                case MonsterRacesEnum.CORALATOR_COLONY:
                    break;
                case MonsterRacesEnum.PEAT_BOG_MONSTERS:
                    break;
                case MonsterRacesEnum.DARK_FLORIFAUNA:
                    break;
                case MonsterRacesEnum.TREE_PEOPLE:
                    break;
                case MonsterRacesEnum.OTOMAIS_ARK_PIRATES:
                    break;
                case MonsterRacesEnum.ZOTHS:
                    break;
                case MonsterRacesEnum.ARCHMONSTERS:
                    return 0.1d;
                case MonsterRacesEnum.MASTOGOBS:
                    break;
                case MonsterRacesEnum.FRIGOST_VILLAGE_CRITTERS:
                    break;
                case MonsterRacesEnum.LONESOME_PINE_FAUNA:
                    break;
                case MonsterRacesEnum.PINGWINS:
                    break;
                case MonsterRacesEnum.BEARBARIANS:
                    break;
                case MonsterRacesEnum.SULFURIOUSES:
                    break;
                case MonsterRacesEnum.HESPERUS_CREW:
                    break;
                case MonsterRacesEnum.BROCKHARDS:
                    break;
                case MonsterRacesEnum.SNOWFOUX:
                    break;
                case MonsterRacesEnum.SYLVASPIRITS:
                    break;
                case MonsterRacesEnum.FRIGOST_WANTED_NOTICES:
                    break;
                case MonsterRacesEnum.FRIGOST_QUEST_MONSTERS:
                    break;
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
                    break;
                case MonsterRacesEnum.KWISMAS_QUEST_MONSTERS:
                    break;
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
                case MonsterRacesEnum.ALLIANCE_PRISMS:
                    break;
                case MonsterRacesEnum.KRISMAHLO_ISLAND_MONSTERS:
                    break;
                case MonsterRacesEnum.ARCHBISHOPS_PALACE_MONSTERS:
                    break;
                case MonsterRacesEnum.ALIGNMENT_QUEST_MONSTERS:
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                case MonsterRacesEnum.BIGBOMORPHS:
                    break;
                case MonsterRacesEnum.CHOCOMANCER:
                    break;
                case MonsterRacesEnum.BRUTOMORPHS:
                    break;
                case MonsterRacesEnum.BRIKOMORPHS:
                    break;
                case MonsterRacesEnum.FLEASTER_ISLAND_QUEST_MONSTERS:
                    break;
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
                case MonsterRacesEnum.THRALLS:
                    break;
                case MonsterRacesEnum.MISERITES:
                    break;
                case MonsterRacesEnum.WARMONGERS:
                    break;
                case MonsterRacesEnum.CORRUPTED_ONES:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_ROGUE:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_FOGGERNAUT:
                    break;
                case MonsterRacesEnum.TEMPORIS_SUMMONS:
                    break;
                case MonsterRacesEnum.KWAPA:
                    break;
                case MonsterRacesEnum.KOZARU:
                    break;
                case MonsterRacesEnum.TANUKIS:
                    break;
                case MonsterRacesEnum.PLANTALAS:
                    break;
                case MonsterRacesEnum.FIREFOUX:
                    break;
                case MonsterRacesEnum.MIST_ARMY:
                    break;
                case MonsterRacesEnum.TOMB_YOKAI:
                    break;
                case MonsterRacesEnum.PAPER_YOKIANZHI:
                    break;
                case MonsterRacesEnum.INK_YOKIANZHI:
                    break;
                case MonsterRacesEnum.ECAFLIP_CITY_MONSTERS:
                    break;
                case MonsterRacesEnum.THE_POSSESSED:
                    break;
                case MonsterRacesEnum.BRAKMARIAN_RATS:
                    break;
                case MonsterRacesEnum.BONTARIAN_RATS:
                    break;
                case MonsterRacesEnum.DESTROYERS:
                    break;
                case MonsterRacesEnum.GOBBOWL_SUMMONS:
                    break;
                case MonsterRacesEnum.MALTERS:
                    break;
                case MonsterRacesEnum.POUTCH:
                    break;
                case MonsterRacesEnum.WORLD_BOSS:
                    break;
                case MonsterRacesEnum.TIMBERLAND_REBELS:
                    break;
                case MonsterRacesEnum.STRUBIAN_RATS:
                    break;
                case MonsterRacesEnum.MAKNIAN_RATS:
                    break;
                case MonsterRacesEnum.GISGOUL_BWORKS:
                    break;
                case MonsterRacesEnum.BIG_LARVAE:
                    break;
                case MonsterRacesEnum.REST_BEASTS:
                    break;
                case MonsterRacesEnum.MORTAL_KOALAKS:
                    break;
                case MonsterRacesEnum.PRIMITIVE_KOALAKS:
                    break;
                case MonsterRacesEnum.THE_WA_GUAWD:
                    break;
                case MonsterRacesEnum.MUTANT_WABBITS:
                    break;
                case MonsterRacesEnum.DARK_TREECHNIDIANS:
                    break;
                case MonsterRacesEnum.SHORT_TEMPERED_TREECHNIDIANS:
                    break;
                case MonsterRacesEnum.BIG_TOFUS:
                    break;
                case MonsterRacesEnum.MUSICOPATHS:
                    break;
                case MonsterRacesEnum.WILD_KOALAKS:
                    break;
                case MonsterRacesEnum.PROTECTORS_OF_EPHEDRYA:
                    break;
                case MonsterRacesEnum.MINOTOT:
                    break;
                case MonsterRacesEnum.GROHLUM:
                    break;
                case MonsterRacesEnum.PALMIKOKOS:
                    break;
                case MonsterRacesEnum.BITTER_HAMMERS:
                    break;
                case MonsterRacesEnum.SIDEKICK_SUMMONS:
                    break;
                case MonsterRacesEnum.SOLITARY_GUARDIANS:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_FORGELANCE:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_SACRIER:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_IOP:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_HUPPERMAGE:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_XELOR:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_MASQUERAIDER:
                    break;
                case MonsterRacesEnum.CLASS_SUMMONS_ELIOTROPE:
                    break;
                case MonsterRacesEnum.COMMON_SUMMONS:
                    break;
            }
            return 1d;
        }
    }
}
