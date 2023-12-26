using Giny.World.Managers.Fights;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Monsters
{
    public class StaticMonsterGroup : MonsterGroup
    {
        private MonsterStaticSpawnRecord SpawnRecord
        {
            get;
            set;
        }
        public override bool RespawnOnVictory => false;

        public StaticMonsterGroup(MapRecord map, short cellId, MonsterStaticSpawnRecord spawnRecord) : base(map, cellId)
        {
            SpawnRecord = spawnRecord;
        }
        public override void OnFightStarted(FightPvM fight)
        {
            MonstersManager.Instance.SpawnStaticMonsters(SpawnRecord, Map);
        }
    }
}
