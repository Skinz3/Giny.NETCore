using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AreaFightModificatorUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 2587;
        public override ushort MessageId => Id;

        public int spellPairId;

        public AreaFightModificatorUpdateMessage()
        {
        }
        public AreaFightModificatorUpdateMessage(int spellPairId)
        {
            this.spellPairId = spellPairId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)spellPairId);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellPairId = (int)reader.ReadInt();
        }


    }
}








