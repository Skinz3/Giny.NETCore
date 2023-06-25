using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildHousesInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 5298;
        public override ushort MessageId => Id;

        public HouseInformationsForGuild[] housesInformations;

        public GuildHousesInformationMessage()
        {
        }
        public GuildHousesInformationMessage(HouseInformationsForGuild[] housesInformations)
        {
            this.housesInformations = housesInformations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)housesInformations.Length);
            for (uint _i1 = 0;_i1 < housesInformations.Length;_i1++)
            {
                (housesInformations[_i1] as HouseInformationsForGuild).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            HouseInformationsForGuild _item1 = null;
            uint _housesInformationsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _housesInformationsLen;_i1++)
            {
                _item1 = new HouseInformationsForGuild();
                _item1.Deserialize(reader);
                housesInformations[_i1] = _item1;
            }

        }


    }
}








