using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.ORM;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Monsters
{
    public class MonsterKamas 
    {
        public const int BossMultiplicator = 4;

        public const int DroppedKamasRatio = 3;

        public static void Patch()
        {
            Logger.Write("Computing monsters kamas ...");

            AsyncRandom random = new AsyncRandom();

            foreach (var monster in MonsterRecord.GetMonsterRecords())
            {
                int minDroppedKamas = 0;
                int maxDroppedKamas = 0;
                int level = monster.GetGrade(1).Level;

                minDroppedKamas = random.Next(level * DroppedKamasRatio, level * (DroppedKamasRatio * 2));

                maxDroppedKamas = minDroppedKamas + level * 2;

                if (maxDroppedKamas < 20)
                {
                    maxDroppedKamas = 20;
                }

                if (monster.IsBoss)
                {
                    minDroppedKamas *= BossMultiplicator;
                    maxDroppedKamas *= BossMultiplicator;
                }

                monster.MinDroppedKamas = minDroppedKamas / 2;
                monster.MaxDroppedKamas = maxDroppedKamas / 2;

                monster.UpdateInstantElement();
            }
        }
    }
}
