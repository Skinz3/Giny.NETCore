using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectsAddedMessage : ExchangeObjectMessage  
    { 
        public new const ushort Id = 4786;
        public override ushort MessageId => Id;

        public ObjectItem[] @object;

        public ExchangeObjectsAddedMessage()
        {
        }
        public ExchangeObjectsAddedMessage(ObjectItem[] @object,bool remote)
        {
            this.@object = @object;
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)@object.Length);
            for (uint _i1 = 0;_i1 < @object.Length;_i1++)
            {
                (@object[_i1] as ObjectItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItem _item1 = null;
            base.Deserialize(reader);
            uint _objectLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _objectLen;_i1++)
            {
                _item1 = new ObjectItem();
                _item1.Deserialize(reader);
                @object[_i1] = _item1;
            }

        }


    }
}








