using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LivingObjectMessageMessage : NetworkMessage  
    { 
        public  const ushort Id = 9837;
        public override ushort MessageId => Id;

        public short msgId;
        public int timeStamp;
        public string owner;
        public short objectGenericId;

        public LivingObjectMessageMessage()
        {
        }
        public LivingObjectMessageMessage(short msgId,int timeStamp,string owner,short objectGenericId)
        {
            this.msgId = msgId;
            this.timeStamp = timeStamp;
            this.owner = owner;
            this.objectGenericId = objectGenericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element msgId.");
            }

            writer.WriteVarShort((short)msgId);
            if (timeStamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timeStamp + ") on element timeStamp.");
            }

            writer.WriteInt((int)timeStamp);
            writer.WriteUTF((string)owner);
            if (objectGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGenericId + ") on element objectGenericId.");
            }

            writer.WriteVarShort((short)objectGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            msgId = (short)reader.ReadVarUhShort();
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element of LivingObjectMessageMessage.msgId.");
            }

            timeStamp = (int)reader.ReadInt();
            if (timeStamp < 0)
            {
                throw new System.Exception("Forbidden value (" + timeStamp + ") on element of LivingObjectMessageMessage.timeStamp.");
            }

            owner = (string)reader.ReadUTF();
            objectGenericId = (short)reader.ReadVarUhShort();
            if (objectGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGenericId + ") on element of LivingObjectMessageMessage.objectGenericId.");
            }

        }


    }
}








