using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Api
{
    public class CharacterEventApi
    {
        public static event Action<Character> OnHumanOptionsCreated;

        public static void HumanOptionsCreated(Character character)
        {
            OnHumanOptionsCreated?.Invoke(character);
        }
    }
}
