using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    public class PokefusShowcase
    {
        public const long MapId = 222565380;

        public const short CellId = 463;

        public const int Delay = 1000;

        private static MonsterGroup Group
        {
            get;
            set;
        }

        public static void CreateMonsterGroup()
        {
            if (Group != null)
            {
                Group.Map.Instance.RemoveEntity(Group.Id);
            }

            var mapRecord = MapRecord.GetMap(MapId);
          
            var data = PokefusWishManager.GetCurrentWishData(false);

            MonsterGroup group = new MonsterGroup(mapRecord, CellId);

            group.CanBeAggressed = false;

            foreach (var monsterRecord in data.MonsterRecords.Keys)
            {
                var monster = new Monster(monsterRecord, mapRecord.GetCell(CellId));

                group.AddMonster(monster);
            }

            mapRecord.Instance.AddEntity(group);

            Group = group;
        }
       
    }
}
