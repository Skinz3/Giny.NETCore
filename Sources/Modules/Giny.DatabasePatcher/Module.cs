using Giny.Core;
using Giny.Core.Commands;
using Giny.DatabasePatcher.Achievements;
using Giny.DatabasePatcher.Experience;
using Giny.DatabasePatcher.Items;
using Giny.DatabasePatcher.Maps;
using Giny.DatabasePatcher.Monsters;
using Giny.DatabasePatcher.Skills;
using Giny.DatabasePatcher.Spells;
using Giny.IO.D2OClasses;
using Giny.World.Modules;
using System.Reflection;

namespace Giny.DatabasePatcher
{
    [Module("Database patcher")]
    public class Module : IModule
    {
        public void CreateHooks()
        {

        }

        public void Initialize()
        {
            // Patch here
        }

        [ConsoleCommand("patch")]
        public static void PatchCommand()
        {
            Logger.Write("Patching world database ...", Channels.Info);

            LevelAchievements.Patch();
            SubareaAchievements.Patch();
            Dungeons.Patch();
            Experiences.Patch();
            ItemAppearances.Patch();
            LivingObjects.Patch();
            SkillBones.Patch();
            InteractiveElements.Patch();
            MonsterSpawns.Patch();
            Teleporters.Patch();
            MapPlacements.Patch();
            SpellCategories.Patch();
            MonsterKamas.Patch();
            Logger.Write("World database patched.", Channels.Info);
        }

    }
}