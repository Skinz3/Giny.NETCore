using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Characters
{
    public class CharacterCreationResult
    {
        public CharacterCreationResultEnum Result
        {
            get;
            set;
        }
        /// <summary>
        /// Couldnt find this in client
        /// </summary>
        public byte Reason
        {
            get;
            set;
        }

        public CharacterCreationResult(CharacterCreationResultEnum result, byte reason = 0)
        {
            this.Result = result;
            this.Reason = reason;
        }
    }
}
