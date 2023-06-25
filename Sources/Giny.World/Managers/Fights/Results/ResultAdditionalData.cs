using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public abstract class ResultAdditionalData
    {
        public Character Character
        {
            get;
            private set;
        }
        protected ResultAdditionalData(Character character)
        {
            this.Character = character;
        }
        public abstract FightResultAdditionalData GetFightResultAdditionalData();
        public abstract void Apply();
    }
}
