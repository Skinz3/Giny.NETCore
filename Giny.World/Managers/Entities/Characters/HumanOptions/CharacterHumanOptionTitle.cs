using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public class CharacterHumanOptionTitle : CharacterHumanOption
    {
        public short TitleId
        {
            get;
            set;
        }
        public string TitleParam
        {
            get;
            set;
        }
        public CharacterHumanOptionTitle(short titleId, string titleParam)
        {
            this.TitleId = titleId;
            this.TitleParam = titleParam;
        }
        public CharacterHumanOptionTitle()
        {

        }
        public override HumanOption GetHumanOption(Character character)
        {
            return new HumanOptionTitle()
            {
                titleId = TitleId,
                titleParam = TitleParam,
            };
        }
    }
}
