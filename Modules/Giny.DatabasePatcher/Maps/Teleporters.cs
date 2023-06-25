using Giny.Core;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Maps
{
    class Teleporters
    {
        private static int[] ZaapiGfxIds = new int[]
        {
            9541,
            15004,
            34925
        };

        private const int ZaapBones = 5247;

        /*
         * Some zaap and zaapis, needs to be added manually.
         */
        public static void Patch()
        {
            Logger.Write("Add zaapis ...");

            int count = 0;
            foreach (var map in MapRecord.GetMaps())
            {
                foreach (var element in map.Elements)
                {
                    if (ZaapiGfxIds.Contains(element.GfxId) && element.IsInMap())
                    {
                        if (!InteractiveSkillRecord.ExistAndHandled(element.Identifier))
                        {
                            TeleportersManager.Instance.AddDestination(
                            TeleporterTypeEnum.TELEPORTER_SUBWAY,
                            InteractiveTypeEnum.ZAAPI106,
                            GenericActionEnum.Zaapi,
                            map,
                            element,
                            map.Subarea.AreaId);
                            count++;
                        }
                    }

                    if (element.BonesId == ZaapBones && element.IsInMap())
                    {
                        if (!InteractiveSkillRecord.ExistAndHandled(element.Identifier))
                        {
                            TeleportersManager.Instance.AddDestination(TeleporterTypeEnum.TELEPORTER_ZAAP,
                            InteractiveTypeEnum.ZAAP16,
                            GenericActionEnum.Zaap,
                            map,
                            element,
                            1);
                            count++;

                        }
                    }
                }
            }


            Logger.Write(count + " teleporters added.");
        }
    }
}
