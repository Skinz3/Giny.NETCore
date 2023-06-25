using Giny.World.Managers.Monsters;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Fighters
{
    public interface IMonster
    {
        public MonsterGrade Grade
        {
            get;
        }
        public MonsterRecord Record
        {
            get;
        }
    }
}
