using Giny.Protocol.Types;
using Giny.World.Managers.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Results
{
    public class FightPvpData : ResultAdditionalData
    {
        public byte Grade
        {
            get;
            set;
        }
        public short MinHonorForGrade
        {
            get;
            set;
        }
        public short MaxHonorForGrade
        {
            get;
            set;
        }
        public short Honor
        {
            get;
            set;
        }
        public short HonorDelta
        {
            get;
            set;
        }
        public FightPvpData(Character character)
            : base(character)
        {
        }
        public override FightResultAdditionalData GetFightResultAdditionalData()
        {
            return new FightResultPvpData(this.Grade, this.MinHonorForGrade, this.MaxHonorForGrade, this.Honor, this.HonorDelta);
        }
        public override void Apply()
        {
         /*   if (this.HonorDelta > 0)
            {
                base.Character.AddHonor((ushort)HonorDelta);
            }
            else
            {
                if (this.HonorDelta < 0)
                {
                    base.Character.RemoveHonor((ushort)-HonorDelta);
                }
            } */
        }
    }
}
