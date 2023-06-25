using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SpouseStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 2627;
        public override ushort MessageId => Id;

        public bool hasSpouse;

        public SpouseStatusMessage()
        {
        }
        public SpouseStatusMessage(bool hasSpouse)
        {
            this.hasSpouse = hasSpouse;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)hasSpouse);
        }
        public override void Deserialize(IDataReader reader)
        {
            hasSpouse = (bool)reader.ReadBoolean();
        }


    }
}








