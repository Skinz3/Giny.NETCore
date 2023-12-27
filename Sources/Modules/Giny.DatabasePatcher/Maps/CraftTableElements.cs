using Giny.Core;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Maps
{
    public class CraftTableElements
    {
        public static void Patch()
        {
            Logger.Write("Spawning craft tables ...");

            AddCraftTable(217056260, 523886, SkillTypeEnum.SEW63);

        }

        private static void AddCraftTable(int mapId, int elementId, SkillTypeEnum skillType)
        {
            MapsManager.Instance.AddInteractiveSkill(MapRecord.GetMap(mapId), elementId, GenericActionEnum.Craft,
                InteractiveTypeEnum.CRAFTING_TABLE11, skillType);
        }
    }
}
