using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO;
using Giny.Core.Network;
using Giny.IO.MA3;
using Giny.ORM;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Records.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Items
{
    public class ItemAppearances
    {
        private static ItemTypeEnum[] ItemTypes = new ItemTypeEnum[]
      {
            ItemTypeEnum.HAT,
            ItemTypeEnum.CLOAK,
            ItemTypeEnum.MISCELLANEOUS,
            ItemTypeEnum.SHIELD,
      };

        public const string ItemsUrl = "http://www.dofus.tools/myAvatar3/assets/data/Items.ma3";

        public const string MountsUrl = "http://www.dofus.tools/myAvatar3/assets/data/Mounts.ma3";

        public const string DofusBookUrl = "https://www.dofusbook.net/items/dofus/skinator/category/{0}";

        public static void Patch()
        {
            Logger.Write("Fixing item appeareances ...");

            Logger.Write("Downloading Items.ma3 from " + ItemsUrl, Channels.Info);

            try
            {
                ProcessMA3();
            }
            catch
            {
                Logger.Write("Unable to download Items.ma3 from " + ItemsUrl, Channels.Warning);
            }

            Logger.Write("Downloading appearences from Dofus Book", Channels.Info);

            try
            {
                ProcessFromDofusBook();
            }
            catch
            {
                Logger.Write("Unable to download appearance from Dofus Book", Channels.Warning);
            }

        }
        private static bool UseLook(ItemRecord item)
        {
            if (item.TypeEnum == ItemTypeEnum.PETSMOUNT ||
                item.TypeEnum == ItemTypeEnum.PET)
            {
                return true;
            }

            if (item.IsCeremonialItem())
            {
                var effect = item.Effects.GetFirst<EffectDice>(EffectsEnum.Effect_Compatible);

                if (effect != null)
                {
                    var type = (ItemTypeEnum)effect.Value;
                    return type == ItemTypeEnum.PETSMOUNT || type == ItemTypeEnum.PET;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        private static void ProcessFromDofusBook()
        {
            foreach (var itemType in ItemTypes)
            {
                string url = string.Format(DofusBookUrl, (int)itemType);
                var jsonStr = Http.Get(url);
                dynamic json = JsonConvert.DeserializeObject(jsonStr);

                Logger.Write("Patching : " + itemType);

                foreach (var jsonItem in json.data)
                {
                    if (jsonItem.official != null && jsonItem.swf != null)
                    {
                        int itemId = jsonItem.official;
                        int appearenceId = jsonItem.swf;

                        if (ItemRecord.ItemExists(itemId))
                        {
                            ItemRecord itemRecord = ItemRecord.GetItem(itemId);

                            if (!UseLook(itemRecord))
                            {
                                ModifyItemAppearence(itemRecord, appearenceId);
                            }
                            else
                            {
                                string look = "{" + appearenceId + "}";

                                if (look != itemRecord.Look)
                                {
                                    if (itemRecord.Look == string.Empty || itemRecord.Look == null)
                                    {
                                        itemRecord.Look = look;
                                        itemRecord.UpdateNow();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void ProcessMA3()
        {
            MA3ItemFile itemFile = new MA3ItemFile(Giny.Core.IO.Web.DownloadData(ItemsUrl));

            foreach (var item in itemFile.Items)
            {
                if (ItemRecord.ItemExists(item.Id))
                {
                    ItemRecord itemRecord = ItemRecord.GetItem(item.Id);

                    ModifyItemAppearence(itemRecord, item.Skin);

                    if (item.Look != string.Empty && item.Look != itemRecord.Look)
                    {
                        itemRecord.Look = item.Look;
                        itemRecord.UpdateNow();
                    }
                }
            }
        }


        private static void ModifyItemAppearence(ItemRecord itemRecord, int appearenceId)
        {
            if (appearenceId != 0 && itemRecord.AppearenceId != appearenceId)
            {
                WeaponRecord weapon = WeaponRecord.GetWeapon(itemRecord.Id);

                if (weapon != null)
                {
                    weapon.AppearenceId = (short)appearenceId;
                    weapon.UpdateNow();
                }
                else
                {
                    itemRecord.AppearenceId = (short)appearenceId;
                    itemRecord.UpdateNow();
                }

            }
        }
    }
}
