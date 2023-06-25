using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayDelayedObjectUseMessage : GameRolePlayDelayedActionMessage  
    { 
        public new const ushort Id = 1610;
        public override ushort MessageId => Id;

        public short objectGID;

        public GameRolePlayDelayedObjectUseMessage()
        {
        }
        public GameRolePlayDelayedObjectUseMessage(short objectGID,double delayedCharacterId,byte delayTypeId,double delayEndTime)
        {
            this.objectGID = objectGID;
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of GameRolePlayDelayedObjectUseMessage.objectGID.");
            }

        }


    }
}








