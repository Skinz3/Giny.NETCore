using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightPlacementSwapPositionsMessage : NetworkMessage  
    { 
        public  const ushort Id = 8732;
        public override ushort MessageId => Id;

        public IdentifiedEntityDispositionInformations[] dispositions;

        public GameFightPlacementSwapPositionsMessage()
        {
        }
        public GameFightPlacementSwapPositionsMessage(IdentifiedEntityDispositionInformations[] dispositions)
        {
            this.dispositions = dispositions;
        }
        public override void Serialize(IDataWriter writer)
        {
            for (uint _i1 = 0;_i1 < 2;_i1++)
            {
                dispositions[_i1].Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            for (uint _i1 = 0;_i1 < 2;_i1++)
            {
                dispositions[_i1] = new IdentifiedEntityDispositionInformations();
                dispositions[_i1].Deserialize(reader);
            }

        }


    }
}








