using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChatSmileyMessage : NetworkMessage  
    { 
        public  const ushort Id = 1197;
        public override ushort MessageId => Id;

        public double entityId;
        public short smileyId;
        public int accountId;

        public ChatSmileyMessage()
        {
        }
        public ChatSmileyMessage(double entityId,short smileyId,int accountId)
        {
            this.entityId = entityId;
            this.smileyId = smileyId;
            this.accountId = accountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (entityId < -9.00719925474099E+15 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element entityId.");
            }

            writer.WriteDouble((double)entityId);
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element smileyId.");
            }

            writer.WriteVarShort((short)smileyId);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            entityId = (double)reader.ReadDouble();
            if (entityId < -9.00719925474099E+15 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element of ChatSmileyMessage.entityId.");
            }

            smileyId = (short)reader.ReadVarUhShort();
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element of ChatSmileyMessage.smileyId.");
            }

            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of ChatSmileyMessage.accountId.");
            }

        }


    }
}








