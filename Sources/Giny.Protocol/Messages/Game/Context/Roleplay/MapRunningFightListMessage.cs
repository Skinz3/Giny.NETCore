using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapRunningFightListMessage : NetworkMessage  
    { 
        public  const ushort Id = 9279;
        public override ushort MessageId => Id;

        public FightExternalInformations[] fights;

        public MapRunningFightListMessage()
        {
        }
        public MapRunningFightListMessage(FightExternalInformations[] fights)
        {
            this.fights = fights;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)fights.Length);
            for (uint _i1 = 0;_i1 < fights.Length;_i1++)
            {
                (fights[_i1] as FightExternalInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            FightExternalInformations _item1 = null;
            uint _fightsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _fightsLen;_i1++)
            {
                _item1 = new FightExternalInformations();
                _item1.Deserialize(reader);
                fights[_i1] = _item1;
            }

        }


    }
}








