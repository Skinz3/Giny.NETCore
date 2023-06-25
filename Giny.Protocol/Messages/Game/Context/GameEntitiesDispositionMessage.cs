using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameEntitiesDispositionMessage : NetworkMessage  
    { 
        public  const ushort Id = 3000;
        public override ushort MessageId => Id;

        public IdentifiedEntityDispositionInformations[] dispositions;

        public GameEntitiesDispositionMessage()
        {
        }
        public GameEntitiesDispositionMessage(IdentifiedEntityDispositionInformations[] dispositions)
        {
            this.dispositions = dispositions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)dispositions.Length);
            for (uint _i1 = 0;_i1 < dispositions.Length;_i1++)
            {
                (dispositions[_i1] as IdentifiedEntityDispositionInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            IdentifiedEntityDispositionInformations _item1 = null;
            uint _dispositionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _dispositionsLen;_i1++)
            {
                _item1 = new IdentifiedEntityDispositionInformations();
                _item1.Deserialize(reader);
                dispositions[_i1] = _item1;
            }

        }


    }
}








