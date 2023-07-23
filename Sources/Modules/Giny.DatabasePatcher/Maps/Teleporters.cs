using Giny.Core;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Records.Maps;
using Org.BouncyCastle.Asn1.Pkcs;
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
            304418, // new brackmar
            70520, // new bonta1
            70521, // new bonta2
            70914, // frigost
        };



        private static int[] ZaapBones = new int[]
        {
            5247, // Regular

        };

        private static int[] ZaapGfxIds = new int[]
        {
           38003,
           41939,
           41724,
           19804,
        };

        /*
         * Some zaap and zaapis, needs to be added manually.
         */
        public static void Patch()
        {

            Logger.Write("Spawning teleporters ...");

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

                    if ((ZaapBones.Contains(element.BonesId) || ZaapGfxIds.Contains(element.GfxId)) && element.IsInMap())
                    {
                        if (map.Subarea.AreaId == 58) // habre sacs
                        {
                            continue;
                        }

                        if (!InteractiveSkillRecord.ExistAndHandled(element.Identifier))
                        {
                            TeleportersManager.Instance.AddDestination(TeleporterTypeEnum.TELEPORTER_ZAAP,
                            InteractiveTypeEnum.ZAAP16,
                            GenericActionEnum.Zaap,
                            map,
                            element,
                            map.Subarea.Area.SuperAreaId);
                            count++;

                        }
                    }
                }
            }


            Logger.Write(count + " teleporters added.");
        }
    }
}
