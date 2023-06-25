using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Pokefus.Effects
{
    [ProtoContract]
    public class EffectPokefus : EffectCustom
    {
        private const string Description = "Contrôle du monstre {0} Grade : {1}";

        [ProtoMember(18)]
        public short MonsterId
        {
            get;
            set;
        }
        [ProtoMember(19)]
        public byte GradeId
        {
            get;
            set;
        }
        [ProtoMember(20)]
        public string MonsterName
        {
            get;
            set;
        }
        public EffectPokefus()
        {
            EffectId = TextEffectId;
        }
        public EffectPokefus(short monsterId, string monsterName, byte gradeId)
        {
            this.MonsterId = monsterId;
            this.MonsterName = monsterName;
            this.GradeId = gradeId;
            EffectId = TextEffectId;
        }

        public override object Clone()
        {
            return new EffectPokefus()
            {
                EffectId = EffectId,
                MonsterId = MonsterId,
                GradeId = GradeId,
                MonsterName = MonsterName,
            };
        }
    
        public override bool Equals(object obj)
        {
            return obj is EffectPokefus ? Equals((EffectPokefus)obj) : false;
        }
        private bool Equals(EffectPokefus effect)
        {
            return MonsterId == effect.MonsterId && GradeId == effect.GradeId && MonsterName == effect.MonsterName;
        }
        protected override string GetEffectDescription()
        {
            return string.Format(Description, MonsterName, GradeId);
        }

    }
}
