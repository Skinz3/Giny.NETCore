using Giny.Core;
using Giny.Core.IO;
using Giny.IO;
using Giny.IO.D2I;
using Giny.IO.D2O;
using Giny.IO.D2OClasses;
using Giny.IO.D2P;
using Giny.ORM;
using Giny.ORM.Interfaces;
using Giny.ORM.IO;
using Giny.World.Managers.Entities.Look;
using Giny.World.Records;
using Giny.World.Records.Achievements;
using Giny.World.Records.Breeds;
using Giny.World.Records.Challenges;
using Giny.World.Records.Characters;
using Giny.World.Records.Effects;
using Giny.World.Records.Items;
using Giny.World.Records.Jobs;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Npcs;
using Giny.World.Records.Quests;
using Giny.World.Records.Spells;
using Giny.World.Records.Tinsel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Giny.DatabaseSynchronizer
{
    class Program
    {
        public static bool SYNC_D2O = true;
        public static bool SYNC_MAPS = true;

        static void Main(string[] args)
        {

            Logger.DrawLogo();

            Logger.Write("Starting synchronization...", Channels.Info);

            D2IManager.Initialize(Path.Combine(ClientConstants.ClientPath, ClientConstants.i18nPath));

            DatabaseManager.Instance.Initialize(Assembly.GetAssembly(typeof(BreedRecord)),
              "127.0.0.1", "giny_world", "root", "");

            if (SYNC_D2O)
            {

                DatabaseManager.Instance.DropTableIfExists<RecipeRecord>();
                DatabaseManager.Instance.DropTableIfExists<SubareaRecord>();
                DatabaseManager.Instance.DropTableIfExists<AreaRecord>();
                DatabaseManager.Instance.DropTableIfExists<ItemSetRecord>();
                DatabaseManager.Instance.DropTableIfExists<BreedRecord>();
                DatabaseManager.Instance.DropTableIfExists<ExperienceRecord>();
                DatabaseManager.Instance.DropTableIfExists<HeadRecord>();
                DatabaseManager.Instance.DropTableIfExists<EffectRecord>();
                DatabaseManager.Instance.DropTableIfExists<MapScrollActionRecord>();
                DatabaseManager.Instance.DropTableIfExists<SpellRecord>();
                DatabaseManager.Instance.DropTableIfExists<SpellVariantRecord>();
                DatabaseManager.Instance.DropTableIfExists<ItemRecord>();
                DatabaseManager.Instance.DropTableIfExists<QuestStepRecord>();
                DatabaseManager.Instance.DropTableIfExists<QuestStepRewardRecord>();
                DatabaseManager.Instance.DropTableIfExists<QuestRecord>();
                DatabaseManager.Instance.DropTableIfExists<SpellStateRecord>();
                DatabaseManager.Instance.DropTableIfExists<WeaponRecord>();
                DatabaseManager.Instance.DropTableIfExists<MapReferenceRecord>();
                DatabaseManager.Instance.DropTableIfExists<LivingObjectRecord>();
                DatabaseManager.Instance.DropTableIfExists<EmoteRecord>();
                DatabaseManager.Instance.DropTableIfExists<SpellLevelRecord>();
                DatabaseManager.Instance.DropTableIfExists<OrnamentRecord>();
                DatabaseManager.Instance.DropTableIfExists<TitleRecord>();
                DatabaseManager.Instance.DropTableIfExists<MonsterRecord>();
                DatabaseManager.Instance.DropTableIfExists<SkillRecord>();
                DatabaseManager.Instance.DropTableIfExists<DungeonRecord>();
                DatabaseManager.Instance.DropTableIfExists<MapPositionRecord>();
                DatabaseManager.Instance.DropTableIfExists<NpcRecord>();
                DatabaseManager.Instance.DropTableIfExists<SpellBombRecord>();
                DatabaseManager.Instance.DropTableIfExists<ChallengeRecord>();
                DatabaseManager.Instance.DropTableIfExists<AchievementRewardRecord>();
                DatabaseManager.Instance.DropTableIfExists<AchievementRecord>();
                DatabaseManager.Instance.DropTableIfExists<AchievementObjectiveRecord>();
            }


            if (SYNC_MAPS)
                DatabaseManager.Instance.DropTableIfExists<MapRecord>();

            DatabaseManager.Instance.CreateAllTablesIfNotExists();

            D2OSynchronizer.Synchronize();

            MapSynchronizer.Synchronize();

            Logger.WriteColor1("Build finished.");
            Console.Read();

        }

    }
}
