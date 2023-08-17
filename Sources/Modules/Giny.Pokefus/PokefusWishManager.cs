using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps;
using Giny.World.Records.Items;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    public enum PokefusRarity
    {
        Common,
        Banner,
        Mythic,
        Legendary,
    }
    public class PokefusWishManager
    {
        private static Dictionary<PokefusRarity, ItemRecord> ItemRecords = new Dictionary<PokefusRarity, ItemRecord>();

        private const string WishFilepath = "pokefus.json";

        static PokefusWishConfiguration WishData;

        static object locker = new object();

        [GenericActionHandler(GenericActionEnum.PokefusWish)]
        public static void HandlePokefusAction(Character character, IGenericActionParameter parameter)
        {
            character.Inventory.Unequip(Protocol.Enums.CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);

            var random = new Random();

            var npcSpawn = NpcSpawnRecord.GetNpcSpawnRecord((parameter as NpcReplyRecord).NpcSpawnId);

            var count = int.Parse(parameter.Param1);

            for (int i = 0; i < count; i++)
            {
                Wish(character, npcSpawn, random, GetCurrentWishData());
            }

        }

        public static WishData GetCurrentWishData(bool withStatic = true)
        {
            lock (locker)
            {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;

                var weekNum = currentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday) / 4d;

                if (weekNum == 0)
                    weekNum = 1;

                var indice = (int)(WishData.Data.Count % weekNum);

                var result = WishData.Data[indice - 1];


                if (withStatic)
                {
                    return result;
                }
                else
                {
                    var monsters = new Dictionary<MonsterRecord, double>();

                    foreach (var monster in result.MonsterRecords)
                    {
                        if (!WishData.StaticMonsters.ContainsKey(monster.Key.Name))
                        {
                            monsters.Add(monster.Key, monster.Value);
                        }
                    }

                    return new WishData()
                    {
                        Indice = result.Indice,
                        MonsterRecords = monsters,
                        Monsters = monsters.ToDictionary(x => x.Key.Name, x => x.Value),
                    };
                }
            }
        }

        private static PokefusRarity GetRarity(MonsterRecord record, double rate)
        {
            lock (locker)
            {

                if (WishData.StaticMonsters.ContainsKey(record.Name))
                {
                    return PokefusRarity.Common;
                }

                if (rate >= 0.1)
                {
                    return PokefusRarity.Banner;
                }
                else if (rate >= 0.03)
                {
                    return PokefusRarity.Mythic;
                }
                else
                {
                    return PokefusRarity.Legendary;
                }
            }

        }
        private static void Wish(Character character, NpcSpawnRecord npcSpawn, Random random, WishData data)
        {
            lock (locker)
            {
                var monster = data.RollMonster(random);

                double rate = data.MonsterRecords[monster];

                PokefusRarity rarity = GetRarity(monster, rate);

                ItemRecord itemRecord = ItemRecords[rarity];

                bool isStatic = WishData.StaticMonsters.ContainsKey(monster.Name);

                var grade = isStatic ? monster.Grades.First() : monster.Grades.Random(random);

                var item = PokefusManager.Instance.CreatePokefusItem(character.Id, itemRecord, monster, grade.GradeId);

                character.Inventory.AddItem(item);

                string msg = $"Vous avez obtenu <b>{monster.Name}</b>. Drop <b>: {Math.Round(rate * 100d, 2)}%</b>.";

                if (isStatic)
                {
                    character.Reply(msg);
                    return;
                }

                var direction = character.GetCell().Point.OrientationTo(new MapPoint(PokefusShowcase.CellId));

                if (rate >= 0.1)
                {
                    character.PlaySpellAnim(PokefusShowcase.CellId, 25492, 1, direction);
                    character.Reply(msg, Color.Orange);
                }
                else if (rate >= 0.03)
                {
                    character.PlaySpellAnimOnMap(PokefusShowcase.CellId, 10126, 1, direction); // 19066
                    character.Reply(msg, Color.PaleVioletRed);
                    character.DisplayNotification("Félicitation ! Il semble que la chance vous sourit , vous reportez un pokéfus mythique!");
                }
                else
                {
                    character.PlaySpellAnimOnMap(PokefusShowcase.CellId, 19066, 1, direction);
                    character.Reply(msg, Color.Red);

                    Thread.Sleep(3000);
                    character.DisplayPopup(1, "Pokéfus", $"Félicitation ! Quelle chance, Vous obtenez un pokéfus absolument magnifique un {monster.Name} sauvage !");
                }
            }
        }


        public static void Initialize()
        {
            if (!File.Exists(WishFilepath))
            {
                WishData = new PokefusWishConfiguration();
                File.WriteAllText(WishFilepath, Json.Serialize(WishData));
            }
            else
            {
                WishData = Json.Deserialize<PokefusWishConfiguration>(File.ReadAllText(WishFilepath));
            }

            WishData.Data = WishData.Data.OrderBy(x => x.Indice).ToList();

            foreach (var wishData in WishData.Data)
            {
                foreach (var staticMonster in WishData.StaticMonsters)
                {
                    if (wishData.Monsters.ContainsKey(staticMonster.Key))
                    {
                        Logger.Write($"Unable to add monster '{staticMonster.Key}' to wish data. Already in static list.", Channels.Warning);
                        continue;
                    }
                    wishData.Monsters.Add(staticMonster.Key, staticMonster.Value);


                }


                foreach (var pair in wishData.Monsters)
                {
                    MonsterRecord record = MonsterRecord.GetMonsterRecords().FirstOrDefault(x => x.Name == pair.Key);

                    if (record == null)
                    {
                        Logger.Write($"Unable to add monster '{pair.Key}' to wish data. Not found.", Channels.Warning);
                        continue;
                    }
                    wishData.MonsterRecords.Add(record, pair.Value);
                }
            }

            ItemRecords.Clear();
            ItemRecords.Add(PokefusRarity.Common, ItemRecord.GetItem(27582));
            ItemRecords.Add(PokefusRarity.Banner, ItemRecord.GetItem(27581));
            ItemRecords.Add(PokefusRarity.Mythic, ItemRecord.GetItem(27583));
            ItemRecords.Add(PokefusRarity.Legendary, ItemRecord.GetItem(27600));

            PokefusShowcase.CreateMonsterGroup();

            Logger.Write($"{WishData.Data.Count} wish data found.");

        }


    }

    public class PokefusWishConfiguration
    {
        public List<WishData> Data = new List<WishData>();
        public Dictionary<string, double> StaticMonsters
        {
            get;
            set;
        } = new Dictionary<string, double>();

    }
    public class WishData
    {
        public int Indice
        {
            get;
            set;
        }
        public Dictionary<string, double> Monsters
        {
            get;
            set;
        } = new Dictionary<string, double>();

        [JsonIgnore]
        public Dictionary<MonsterRecord, double> MonsterRecords
        {
            get;
            set;
        } = new Dictionary<MonsterRecord, double>();


        public MonsterRecord RollMonster(Random random)
        {
            var monsters = MonsterRecords.Keys.Shuffle();

            foreach (var monster in monsters)
            {
                var value = random.NextDouble();

                if (value <= MonsterRecords[monster])
                {
                    return monster;
                }
            }


            var ordered = MonsterRecords.OrderByDescending(x => x.Value);

            return ordered.FirstOrDefault().Key;



        }
    }
}
