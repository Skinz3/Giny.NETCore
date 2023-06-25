using Giny.Protocol.Types;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Monsters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Monsters
{
    public class ModularMonsterGroup : MonsterGroup
    {
        /*
         * { Max Player, Monsters }
         */
        private readonly Dictionary<short, short> Rule = new Dictionary<short, short>()
        {
           { 4,  4 },
           { 8,  8 },
        };

        public ModularMonsterGroup(MapRecord map, short cellId) : base(map, cellId)
        {

        }
        public override GroupMonsterStaticInformations GetGroupMonsterStaticInformations()
        {
            IEnumerable<MonsterInGroupLightInformations> monsterInformations = Monsters.Select(x => new MonsterInGroupLightInformations((int)x.Record.Id, x.GradeId, x.Grade.Level));

            List<AlternativeMonstersInGroupLightInformations> alternatives = new List<AlternativeMonstersInGroupLightInformations>();

            foreach (var count in Rule)
            {
                alternatives.Add(new AlternativeMonstersInGroupLightInformations(count.Key, monsterInformations.Take(count.Value).ToArray()));
            }

            return new GroupMonsterStaticInformationsWithAlternatives()
            {
                underlings = GetMonsterInGroupInformations(),
                mainCreatureLightInfos = Leader.GetMonsterInGroupLightInformations(),
                alternatives = alternatives.ToArray()
            };
        }
        public int GetMonsterCount(int enemiesCount)
        {
            int value = Rule.Last().Value;

            foreach (var count in Rule)
            {
                if (enemiesCount <= count.Key)
                {
                    value = count.Value;
                    break;
                }
            }

            if (value > Monsters.Count)
            {
                return Monsters.Count;
            }
            else
            {
                return value;
            }
        }
        public IEnumerable<Fighter> CreateFighters(FightTeam team, int numberOfMonsters, int numbersOfPlayers)
        {
            var monsters = Monsters.Skip(numberOfMonsters);

            int count = GetMonsterCount(numbersOfPlayers) - numberOfMonsters;

            foreach (var monster in monsters.Take(count))
            {
                yield return monster.CreateFighter(team);
            }

        }
        public override IEnumerable<Fighter> CreateFighters(FightTeam team)
        {
            int enemiesCount = team.EnemyTeam.GetFightersCount();

            int count = GetMonsterCount(enemiesCount);

            foreach (var monster in Monsters.Take(count))
            {
                yield return monster.CreateFighter(team);
            }
        }
    }
}
