using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameMapChangeOrientationsMessage : NetworkMessage  
    { 
        public  const ushort Id = 3831;
        public override ushort MessageId => Id;

        public ActorOrientation[] orientations;

        public GameMapChangeOrientationsMessage()
        {
        }
        public GameMapChangeOrientationsMessage(ActorOrientation[] orientations)
        {
            this.orientations = orientations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)orientations.Length);
            for (uint _i1 = 0;_i1 < orientations.Length;_i1++)
            {
                (orientations[_i1] as ActorOrientation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ActorOrientation _item1 = null;
            uint _orientationsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _orientationsLen;_i1++)
            {
                _item1 = new ActorOrientation();
                _item1.Deserialize(reader);
                orientations[_i1] = _item1;
            }

        }


    }
}








