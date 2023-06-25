using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Chat;
using Giny.World.Network;
using Giny.World.Records.Items;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    class ChatCommands
    {
        [ChatCommand("pokefus", ServerRoleEnum.GamemasterPadawan)]
        public static void PokefusCommand(WorldClient client, short monsterId)
        {
            MonsterRecord monsterRecord = MonsterRecord.GetMonsterRecord(monsterId);
            CharacterItemRecord item = PokefusManager.Instance.CreatePokefusItem(client.Character.Id, monsterRecord, 1);
            client.Character.Inventory.AddItem(item);
            client.Character.Reply("Pokefus " + monsterRecord.Name + " added.");
        }
    }
}
