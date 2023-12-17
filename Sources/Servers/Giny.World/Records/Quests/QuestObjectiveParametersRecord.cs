using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Quests
{
    [ProtoContract]
    public class QuestObjectiveParametersRecord
    {
        [ProtoMember(1)]
        public uint NumParams
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public int Param0
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public int Param1
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public int Param2
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public int Param3
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public int Param4
        {
            get;
            set;
        }
        [ProtoMember(7)]
        public bool DungeonOnly
        {
            get;
            set;
        }
    }
}
