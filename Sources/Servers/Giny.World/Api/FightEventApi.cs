using Giny.Protocol.Enums;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Api
{
    public class FightEventApi
    {
        public static event Action<FightPlayerResult> OnPlayerResultApplied;
        public static event Action<Fight> OnPlacementStarted;
        public static event Action<Fighter> OnFighterJoined;
        public static event Func<SpellCast, bool> OnSpellCasting;

        internal static void PlayerResultApplied(FightPlayerResult fightPlayerResult)
        {
            OnPlayerResultApplied?.Invoke(fightPlayerResult);
        }

        internal static void PlacementStarted(Fight fight)
        {
            OnPlacementStarted?.Invoke(fight);
        }

        internal static void FighterJoined(Fighter fighter)
        {
            OnFighterJoined?.Invoke(fighter);
        }

        internal static bool CanCastSpell(SpellCast cast)
        {
            bool? result = OnSpellCasting?.Invoke(cast);

            if (!result.HasValue)
                return true;
            else
                return result.Value;
        }

    }
}
