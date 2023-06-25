using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public class CharacterHumanOptionOrnament : CharacterHumanOption
    {
        public short OrnamentId
        {
            get;
            set;
        }
        public CharacterHumanOptionOrnament(short ornamentId)
        {
            this.OrnamentId = ornamentId;
        }
        public CharacterHumanOptionOrnament()
        {

        }
        public override HumanOption GetHumanOption(Character character)
        {
            return new HumanOptionOrnament()
            {
                ornamentId = OrnamentId,
                ladderPosition = 2,
                leagueId = 0,
                level = character.Level,
            };
        }
    }
}
