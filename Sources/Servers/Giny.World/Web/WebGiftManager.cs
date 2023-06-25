using Giny.Core.DesignPattern;
using Giny.World.Network;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Web
{
    // TODO ALL WEB API IN A MODULE.

    public class WebGiftManager : Singleton<WebGiftManager>
    {
        public const string Message = "La conversion de vos Ogrines depuis le site a été effectuée avec succès.";

        public bool AddGift(int accountId, int tokenId, int tokenCount)
        {
            WorldClient client = WorldServer.Instance.GetOnlineClients().FirstOrDefault(x => x.Account.Id == accountId);

            if (client != null)
            {
                CharacterItemRecord item = client.Character.Inventory.AddItem((short)tokenId, tokenCount);
                client.Character.NotifyItemGained((short)tokenId, tokenCount);
                client.Character.DisplayNotification(string.Format(Message, tokenCount, item.Record.Name));
                return true;
            }
            return false;
        }
    }
}
