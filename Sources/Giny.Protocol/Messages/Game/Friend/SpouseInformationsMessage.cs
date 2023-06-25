using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SpouseInformationsMessage : NetworkMessage  
    { 
        public  const ushort Id = 9621;
        public override ushort MessageId => Id;

        public FriendSpouseInformations spouse;

        public SpouseInformationsMessage()
        {
        }
        public SpouseInformationsMessage(FriendSpouseInformations spouse)
        {
            this.spouse = spouse;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)spouse.TypeId);
            spouse.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            spouse = ProtocolTypeManager.GetInstance<FriendSpouseInformations>((short)_id1);
            spouse.Deserialize(reader);
        }


    }
}








