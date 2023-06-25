using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Logging;
using Giny.ORM;
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
    public class InteractiveElements
    {
        public static void Patch()
        {
            Logger.Write("Synchronize interactives elements ...");

            int count = 0;
            foreach (SkillRecord skillRecord in SkillRecord.GetSkills())
            {
                if (skillRecord.ParentBonesIds.Count > 0)
                {
                    InteractiveElementRecord[] elements = MapRecord.GetElementsByBonesId(skillRecord.ParentBonesIds);

                    foreach (var element in elements)
                    {
                        if (!InteractiveSkillRecord.ExistAndHandled(element.Identifier))
                        {
                            count++;

                            MapRecord map = MapRecord.GetMap(element.MapId);

                            MapsManager.Instance.AddInteractiveSkill(map, element.Identifier, GenericActionEnum.Collect,
                                (InteractiveTypeEnum)skillRecord.InteractiveTypeId, (SkillTypeEnum)skillRecord.Id);

                        }
                    }
                }
            }

            return;

            PercentageLogger logger = new PercentageLogger();

            var maps = MapRecord.GetMaps();

            foreach (var map in maps)
            {
                foreach (var element in map.Elements.Where(x => x.Identifier != 0))
                {
                    if (!InteractiveSkillRecord.Exist(element.Identifier))
                    {
                        MapsManager.Instance.AddInteractiveSkill(map, element.Identifier, GenericActionEnum.Unhandled,
                            (InteractiveTypeEnum)0, SkillTypeEnum.USE114);
                    }
                }
                count++;

                logger.Update((int)((count / (double)maps.Count()) * 100) + "%");
            }

            if (count > 0)
            {
                Logger.Write(count + " collect stated elements added on maps.");

                foreach (var map in MapRecord.GetMaps())
                {
                    map.Instance.Reload();
                }
            }
        }
    }
}
