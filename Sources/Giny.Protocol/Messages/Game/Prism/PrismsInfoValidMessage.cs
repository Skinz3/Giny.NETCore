using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismsInfoValidMessage : NetworkMessage  
    { 
        public  const ushort Id = 4883;
        public override ushort MessageId => Id;

        public PrismFightersInformation[] fights;

        public PrismsInfoValidMessage()
        {
        }
        public PrismsInfoValidMessage(PrismFightersInformation[] fights)
        {
            this.fights = fights;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)fights.Length);
            for (uint _i1 = 0;_i1 < fights.Length;_i1++)
            {
                (fights[_i1] as PrismFightersInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            PrismFightersInformation _item1 = null;
            uint _fightsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _fightsLen;_i1++)
            {
                _item1 = new PrismFightersInformation();
                _item1.Deserialize(reader);
                fights[_i1] = _item1;
            }

        }


    }
}








