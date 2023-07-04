using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TreasureHuntStepDig : TreasureHuntStep
    {
        public new const ushort Id = 961;
        public override ushort TypeId => Id;


        public TreasureHuntStepDig()
        {
        }
        public override void Serialize(IDataWriter writer)
        {
        }
        public override void Deserialize(IDataReader reader)
        {
        }


    }
}


