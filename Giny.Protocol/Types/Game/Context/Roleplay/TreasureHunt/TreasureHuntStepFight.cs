using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TreasureHuntStepFight : TreasureHuntStep  
    { 
        public new const ushort Id = 7230;
        public override ushort TypeId => Id;


        public TreasureHuntStepFight()
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








