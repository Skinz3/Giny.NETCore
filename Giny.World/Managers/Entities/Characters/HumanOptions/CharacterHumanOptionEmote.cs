using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters.HumanOptions
{
    public class CharacterHumanOptionEmote : CharacterHumanOption
    {
        public byte EmoteId
        {
            get;
            set;
        }
        public double EmoteStartTime
        {
            get;
            set;
        }

        public CharacterHumanOptionEmote(byte emoteId, double emoteStartTime)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = EmoteStartTime;
        }
        public CharacterHumanOptionEmote()
        {

        }
        public override HumanOption GetHumanOption(Character character)
        {
            return new HumanOptionEmote(EmoteId, EmoteStartTime);
        }
    }
}
