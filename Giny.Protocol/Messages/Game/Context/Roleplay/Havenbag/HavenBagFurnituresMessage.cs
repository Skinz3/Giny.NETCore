using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HavenBagFurnituresMessage : NetworkMessage  
    { 
        public  const ushort Id = 8483;
        public override ushort MessageId => Id;

        public HavenBagFurnitureInformation[] furnituresInfos;

        public HavenBagFurnituresMessage()
        {
        }
        public HavenBagFurnituresMessage(HavenBagFurnitureInformation[] furnituresInfos)
        {
            this.furnituresInfos = furnituresInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)furnituresInfos.Length);
            for (uint _i1 = 0;_i1 < furnituresInfos.Length;_i1++)
            {
                (furnituresInfos[_i1] as HavenBagFurnitureInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            HavenBagFurnitureInformation _item1 = null;
            uint _furnituresInfosLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _furnituresInfosLen;_i1++)
            {
                _item1 = new HavenBagFurnitureInformation();
                _item1.Deserialize(reader);
                furnituresInfos[_i1] = _item1;
            }

        }


    }
}








