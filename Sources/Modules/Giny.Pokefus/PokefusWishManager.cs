using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Entities.Characters;
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

        [GenericActionHandler(GenericActionEnum.PokefusWish)]
        public static void HandlePokefusAction(Character character, IGenericActionParameter parameter)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            var weekNum = currentCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            var indice = (int)(WishData.Data.Count % (double)weekNum);

            var random = new Random();

            var npcSpawn = NpcSpawnRecord.GetNpcSpawnRecord((parameter as NpcReplyRecord).NpcSpawnId);

            var count = int.Parse(parameter.Param1);

            for (int i = 0; i < count; i++)
            {
                Wish(character, npcSpawn, random, indice);
            }



        }

        private static PokefusRarity GetRarity(string monsterName, double rate)
        {
            if (WishData.StaticMonsters.ContainsKey(monsterName))
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
        private static void Wish(Character character, NpcSpawnRecord npcSpawn, Random random, int indice)
        {
            var data = WishData.Data.Find(x => x.Indice == indice);

            string monsterName = data.RollMonster(random);

            double rate = data.Monsters[monsterName];

            PokefusRarity rarity = GetRarity(monsterName, rate);

            ItemRecord itemRecord = ItemRecords[rarity];

            bool isStatic = WishData.StaticMonsters.ContainsKey(monsterName);

            MonsterRecord monster = MonsterRecord.GetMonsterRecords().FirstOrDefault(x => x.Name == monsterName);

            var grade = isStatic ? monster.Grades.First() : monster.Grades.Random(random);

            var item = PokefusManager.Instance.CreatePokefusItem(character.Id, itemRecord, monster, grade.GradeId);

            character.Inventory.AddItem(item);

            string msg = $"Vous avez obtenu <b>{monsterName}</b>. Drop <b>: {Math.Round(rate * 100d, 2)}%</b>.";

            if (isStatic)
            {
                character.Reply(msg);
                return;
            }

            if (rate >= 0.1)
            {
                character.PlaySpellAnim(npcSpawn.CellId, 25492, 1, DirectionsEnum.DIRECTION_SOUTH_EAST);
                character.Reply(msg, Color.Orange);
            }
            else if (rate >= 0.03)
            {
                character.PlaySpellAnimOnMap(npcSpawn.CellId, 10126, 1, DirectionsEnum.DIRECTION_SOUTH_EAST); // 19066
                character.Reply(msg, Color.PaleVioletRed);
                character.DisplayNotification("Félicitation ! Il semble que la chance vous sourit , vous reportez un pokéfus mythique!");
            }
            else
            {
                character.PlaySpellAnimOnMap(npcSpawn.CellId, 19066, 1, DirectionsEnum.DIRECTION_SOUTH_EAST);
                character.Reply(msg, Color.Red);

                Thread.Sleep(1000);
                character.DisplayPopup(1, "Pokéfus", $"Félicitation ! Quelle chance, Vous obtenez un pokéfus absolument magnifique un {monsterName} sauvage !");
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


            foreach (var wishData in WishData.Data)
            {
                foreach (var staticMonster in WishData.StaticMonsters)
                {
                    wishData.Monsters.Add(staticMonster.Key, staticMonster.Value);
                }
            }

            ItemRecords.Clear();
            ItemRecords.Add(PokefusRarity.Common, ItemRecord.GetItem(27582));
            ItemRecords.Add(PokefusRarity.Banner, ItemRecord.GetItem(27581));
            ItemRecords.Add(PokefusRarity.Mythic, ItemRecord.GetItem(27583));
            ItemRecords.Add(PokefusRarity.Legendary, ItemRecord.GetItem(27608));



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


        public string RollMonster(Random random)
        {
            var value = random.NextDouble();

            var probabilities = Monsters.Values.Select(x => (int)(x * 100));
            var monsters = Monsters.Keys.ShuffleWithProbabilities(probabilities);

            return monsters.First();

        }
    }
}
