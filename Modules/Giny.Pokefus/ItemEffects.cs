using Giny.Protocol.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Items;
using Giny.World.Records.Items;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus
{
    public class ItemEffects
    {
        [ItemEffect(EffectsEnum.Effect_Followed)]
        public static void Followed(Character character, int delta)
        {
            var monsterTemplate = MonsterRecord.GetMonsterRecord((short)Math.Abs(delta));

            if (delta > 0)
            {
                character.AddFollower(monsterTemplate.Look);
            }
            else
            {
                character.RemoveFollower(monsterTemplate.Look);
            }
        }


        [ItemUsageHandler(10675)]
        public static bool AddPokefusExperience(Character character, CharacterItemRecord item)
        {
            const long delta = 3000000;

            bool result = PokefusManager.Instance.AddPokefusExperience(character, delta);

            if (result)
            {
                character.Reply("Votre pokéfus a gagné <b>" + delta + "</b> points d'experience.");
                return true;
            }
            else
            {
                character.Reply("Vous devez équiper un pokéfus pour utiliser cet objet.");
                return false;
            }
        }
    }
}
