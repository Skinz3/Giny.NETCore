using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public abstract class CharacterHumanOption
    {
        public abstract HumanOption GetHumanOption(Character character);
    }
}
