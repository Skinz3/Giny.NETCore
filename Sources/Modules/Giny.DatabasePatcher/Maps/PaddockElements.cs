using Giny.Core;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Generic;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Maps.Teleporters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Maps
{
    public class PaddockElements
    {
        private static int[] PaddockGfxId = new int[]
    {
            304508, // new barkmar, (public)
            58491, // astrub (public)
            7793, // cania (private)
            24180, // koalak (private)
            260106, // bonta (public)
            58492, // brakmar (private)
            22192, // Frigost (private)
            17971, // Otomai (private)
    };


        public static void Patch()
        {
            Logger.Write("Spawning paddocks ...");

            int count = 0;

            foreach (var map in MapRecord.GetMaps())
            {

                foreach (var element in map.Elements)
                {

                    if (PaddockGfxId.Contains(element.GfxId) && element.IsInMap())
                    {
                        if (!InteractiveSkillRecord.ExistAndHandled(element.Identifier))
                        {
                            MapsManager.Instance.AddInteractiveSkill(map, element.Identifier, GenericActionEnum.Paddock,
                                InteractiveTypeEnum.PADDOCK120, SkillTypeEnum.ACCESS175);
                            count++;
                        }
                    }

                }
            }


            Logger.Write(count + " paddocks added.");
        }
    }
}
