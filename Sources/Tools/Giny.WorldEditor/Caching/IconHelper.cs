using Giny.IO.D2OClasses;
using Giny.World.Records.Items;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor.Caching
{
    internal class IconHelper
    {
        public static string GetItemIcon(ItemRecord item)
        {
            Item d2oItem = D2OCache.GetItem((int)item.Id);

            var base64image = ExternalResources.GetItemIcon((int)d2oItem.IconId);
            var source = string.Format("data:image/png;base64,{0}", base64image);

            return source;
        }
        public static string GetMonsterIcon(short id)
        {
            var base64image = ExternalResources.GetMonsterIcon((short)id);
            var source = string.Format("data:image/png;base64,{0}", base64image);

            return source;
        }
    }
}
