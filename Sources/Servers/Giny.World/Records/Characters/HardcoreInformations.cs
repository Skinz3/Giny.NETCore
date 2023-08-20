using Giny.ORM.Attributes;
using Giny.Protocol.Custom.Enums;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Characters
{
    [ProtoContract]
    public class HardcoreInformations
    {

        [ProtoMember(1)]
        public HardcoreOrEpicDeathStateEnum DeathState
        {
            get;
            set;
        }


        [Update]
        [ProtoMember(2)]
        public short DeathCount
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public short DeathMaxLevel
        {
            get;
            set;
        }

        public bool IsDead()
        {
            return DeathState != HardcoreOrEpicDeathStateEnum.DEATH_STATE_ALIVE;
        }
    }
}
