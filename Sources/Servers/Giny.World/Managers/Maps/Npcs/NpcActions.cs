using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Records.Items;
using Giny.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Maps.Npcs
{
    public class NpcActions
    {
        /// <summary>
        /// not sure about this parsing performances
        /// </summary>
        [NpcActionHandler(NpcActionsEnum.BUYSELL)]
        public static void HandleBuySellAction(Character character, Npc npc, NpcActionRecord record)
        {
            List<ItemRecord> itemsToSell = new List<ItemRecord>();

            foreach (var itemId in record.Param1.ToString().Split(','))
            {
                itemsToSell.Add(ItemRecord.GetItem(int.Parse(itemId)));
            }

            short tokenId = 0;

            short.TryParse(record.Param2, out tokenId);
            character.OpenBuySellExchange(npc, itemsToSell.ToArray(), tokenId);
        }

        [NpcActionHandler(NpcActionsEnum.TALK)]
        public static void HandleTalkAction(Character character, Npc npc, NpcActionRecord record)
        {
            character.TalkToNpc(npc, record);
        }
        
    }
}
