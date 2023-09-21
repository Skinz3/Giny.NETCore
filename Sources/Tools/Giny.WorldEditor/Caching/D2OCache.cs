using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor.Caching
{
    public class D2OCache
    {

        private static Dictionary<int, Npc> Npcs = new Dictionary<int, Npc>();

        private static Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public static void Initialize()
        {
            Npcs.Clear();
            Items.Clear();

            foreach (Item item in D2OManager.GetObjects("Items.d2o"))
            {
                Items.Add(item.Id, item);
            }

            foreach (Npc npc in D2OManager.GetObjects("Npcs.d2o"))
            {
                Npcs.Add(npc.Id, npc);
            }
        }

        public static Npc GetNpc(int id)
        {
            return Npcs[id];
        }
        public static Item GetItem(int id)
        {
            return Items[id];
        }
    }
}
