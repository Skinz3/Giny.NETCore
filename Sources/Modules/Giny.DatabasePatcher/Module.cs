using Giny.DatabasePatcher.Experience;
using Giny.DatabasePatcher.Items;
using Giny.DatabasePatcher.Maps;
using Giny.DatabasePatcher.Monsters;
using Giny.DatabasePatcher.Skills;
using Giny.DatabasePatcher.Spells;
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
            Experiences.Patch();
            ItemAppearances.Patch();
            LivingObjects.Patch();
            MapPlacements.Patch();
            SkillBones.Patch();
            InteractiveElements.Patch();
            MonsterKamas.Patch();
            MonsterSpawns.Patch();
            SpellCategories.Patch();
            Teleporters.Patch();
        }

    }
}