using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AbstractGameActionMessage : NetworkMessage  
    { 
        public  const ushort Id = 9188;
        public override ushort MessageId => Id;

        public short actionId;
        public double sourceId;

        public AbstractGameActionMessage()
        {
        }
        public AbstractGameActionMessage(short actionId,double sourceId)
        {
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteVarShort((short)actionId);
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element sourceId.");
            }

            writer.WriteDouble((double)sourceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            actionId = (short)reader.ReadVarUhShort();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of AbstractGameActionMessage.actionId.");
            }

            sourceId = (double)reader.ReadDouble();
            if (sourceId < -9.00719925474099E+15 || sourceId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sourceId + ") on element of AbstractGameActionMessage.sourceId.");
            }

        }


    }
}








