using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public class HumanOptionsManager : Singleton<HumanOptionsManager>
    {
        public CharacterHumanOptionOrnament CreateHumanOptionOrnament(Character character)
        {
            return new CharacterHumanOptionOrnament(character.Record.ActiveOrnamentId);
        }
        public CharacterHumanOptionTitle CreateHumanOptionTitle(Character character)
        {
            return new CharacterHumanOptionTitle(character.Record.ActiveTitleId, string.Empty);
        }
        public CharacterHumanOptionGuild CreateHumanOptionGuild()
        {
            return new CharacterHumanOptionGuild();
        }
    }
}
