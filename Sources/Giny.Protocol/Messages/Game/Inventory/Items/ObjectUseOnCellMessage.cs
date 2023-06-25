using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectUseOnCellMessage : ObjectUseMessage  
    { 
        public new const ushort Id = 1222;
        public override ushort MessageId => Id;

        public short cells;

        public ObjectUseOnCellMessage()
        {
        }
        public ObjectUseOnCellMessage(short cells,int objectUID)
        {
            this.cells = cells;
            this.objectUID = objectUID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (cells < 0 || cells > 559)
            {
                throw new System.Exception("Forbidden value (" + cells + ") on element cells.");
            }

            writer.WriteVarShort((short)cells);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            cells = (short)reader.ReadVarUhShort();
            if (cells < 0 || cells > 559)
            {
                throw new System.Exception("Forbidden value (" + cells + ") on element of ObjectUseOnCellMessage.cells.");
            }

        }


    }
}








