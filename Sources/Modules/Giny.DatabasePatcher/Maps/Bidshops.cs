using Giny.Core;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Bidshops;
using Giny.World.Managers.Generic;
using Giny.World.Records.Bidshops;
using Giny.World.Records.Items;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Maps
{
    public class Bidshops
    {
        public static void Patch()
        {
            Logger.Write("Patching bidshops...");

            AddBidshopRecord(new List<int>() { 16, 17, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 19, 20, 82, 177, 151, 169, 217 },
                new List<int>() { 1, 10, 100 }, 522691); // Hotel de vente des équipements bonta.
        }

        private static void AddBidshopRecord(List<int> itemTypes, List<int> quantities, int elementIdentifier, int maxItemPerAccount = 550)
        {
            if (InteractiveSkillRecord.Exist(elementIdentifier))
            {
                Logger.Write("Unable to create bidshop. " + elementIdentifier + " already exists.", Channels.Warning);
                return;
            }

            MapRecord? targetMap = null;


            foreach (var map in MapRecord.GetMaps())
            {
                foreach (var element in map.Elements.Where(x => x.IsInMap()))
                {
                    if (element.Identifier == elementIdentifier)
                    {
                        targetMap = map;
                        break;
                    }
                }
            }


            if (targetMap == null)
            {
                Logger.Write("Unable to find map for element " + elementIdentifier + ". Cannot create bidshop", Channels.Warning);
                return;
            }
            var id = TableManager.Instance.PopId<BidShopRecord>();

            BidShopRecord record = new BidShopRecord()
            {
                MaxItemPerAccount = maxItemPerAccount,
                Id = id,
                ItemTypes = itemTypes,
                Quantities = quantities,
            };


            InteractiveSkillRecord interactiveSkill = new InteractiveSkillRecord()
            {
                ActionIdentifier = GenericActionEnum.Bidshop,
                Criteria = string.Empty,
                Id = TableManager.Instance.PopId<InteractiveSkillRecord>(),
                Identifier = elementIdentifier,
                MapId = targetMap.Id,
                Param1 = id.ToString(),
                SkillId = SkillTypeEnum.CONSULT355,
                Type = InteractiveTypeEnum.EQUIPMENT_MARKETPLACE314,
            };

            interactiveSkill.AddInstantElement();

            record.AddInstantElement();

            targetMap.ReloadMembers();
            targetMap.Instance.Reload();

            BidshopsManager.Instance.Initialize();

        }
    }
}
