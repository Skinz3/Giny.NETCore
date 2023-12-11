using Giny.World.Api;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Modules;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    [Module("Pokéfus")]
    public class Module : IModule
    {
        public void Initialize()
        {
            PokefusWishManager.Initialize();
            PokefusManager.Instance.Initialize();
        }

        public void CreateHooks()
        {
            FightEventApi.OnPlayerResultApplied += PokefusManager.Instance.OnPlayerResultApplied;
           // CharacterEventApi.OnHumanOptionsCreated += PokefusManager.Instance.OnHumanOptionsCreated;
            FightEventApi.OnFighterJoined += PokefusManager.Instance.OnFighterJoined;
            InventoryEventApi.CanEquipItem += PokefusManager.Instance.CanEquipItem;
            FightEventApi.OnSpellCasting += PokefusManager.Instance.OnSpellCasting;
            InventoryEventApi.OnItemEquipped += PokefusManager.Instance.OnItemEquipped;
            InventoryEventApi.OnItemUnequipped += PokefusManager.Instance.OnItemUnequipped;
        }

        public void Dispose()
        {

        }
    }
}
