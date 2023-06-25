using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectGroundAddedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8096;
        public override ushort MessageId => Id;

        public short cellId;
        public short objectGID;

        public ObjectGroundAddedMessage()
        {
        }
        public ObjectGroundAddedMessage(short cellId,short objectGID)
        {
            this.cellId = cellId;
            this.objectGID = objectGID;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
        }
        public override void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of ObjectGroundAddedMessage.cellId.");
            }

            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectGroundAddedMessage.objectGID.");
            }

        }


    }
}








