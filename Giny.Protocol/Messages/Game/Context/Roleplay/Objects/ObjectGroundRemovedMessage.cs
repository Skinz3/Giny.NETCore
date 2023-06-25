using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectGroundRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 5373;
        public override ushort MessageId => Id;

        public short cell;

        public ObjectGroundRemovedMessage()
        {
        }
        public ObjectGroundRemovedMessage(short cell)
        {
            this.cell = cell;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (cell < 0 || cell > 559)
            {
                throw new System.Exception("Forbidden value (" + cell + ") on element cell.");
            }

            writer.WriteVarShort((short)cell);
        }
        public override void Deserialize(IDataReader reader)
        {
            cell = (short)reader.ReadVarUhShort();
            if (cell < 0 || cell > 559)
            {
                throw new System.Exception("Forbidden value (" + cell + ") on element of ObjectGroundRemovedMessage.cell.");
            }

        }


    }
}








