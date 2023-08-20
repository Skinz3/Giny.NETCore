using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Chat;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Items;
using Giny.World.Modules;
using Giny.World.Network;
using Giny.World.Records.Items;

namespace Giny.DungeonZaap
{
    [Module("Dungeon zaap")]
    public class Module : IModule
    {
        public void CreateHooks()
        {

        }

        public void Initialize()
        {

        }

        [ItemUsageHandler(14017)]
        public static bool OpenDungeonZaapDialog(Character character, CharacterItemRecord item)
        {
            character.OpenDialog(new DungeonZaapDialog(character));
            return false;
        }

    }
}