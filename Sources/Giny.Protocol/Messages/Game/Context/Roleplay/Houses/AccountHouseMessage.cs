using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AccountHouseMessage : NetworkMessage  
    { 
        public  const ushort Id = 6940;
        public override ushort MessageId => Id;

        public AccountHouseInformations[] houses;

        public AccountHouseMessage()
        {
        }
        public AccountHouseMessage(AccountHouseInformations[] houses)
        {
            this.houses = houses;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)houses.Length);
            for (uint _i1 = 0;_i1 < houses.Length;_i1++)
            {
                (houses[_i1] as AccountHouseInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AccountHouseInformations _item1 = null;
            uint _housesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _housesLen;_i1++)
            {
                _item1 = new AccountHouseInformations();
                _item1.Deserialize(reader);
                houses[_i1] = _item1;
            }

        }


    }
}








