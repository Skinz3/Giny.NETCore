using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EmotePlayAbstractMessage : NetworkMessage  
    { 
        public  const ushort Id = 610;
        public override ushort MessageId => Id;

        public short emoteId;
        public double emoteStartTime;

        public EmotePlayAbstractMessage()
        {
        }
        public EmotePlayAbstractMessage(short emoteId,double emoteStartTime)
        {
            this.emoteId = emoteId;
            this.emoteStartTime = emoteStartTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element emoteId.");
            }

            writer.WriteShort((short)emoteId);
            if (emoteStartTime < -9.00719925474099E+15 || emoteStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + emoteStartTime + ") on element emoteStartTime.");
            }

            writer.WriteDouble((double)emoteStartTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            emoteId = (short)reader.ReadUShort();
            if (emoteId < 0 || emoteId > 65535)
            {
                throw new System.Exception("Forbidden value (" + emoteId + ") on element of EmotePlayAbstractMessage.emoteId.");
            }

            emoteStartTime = (double)reader.ReadDouble();
            if (emoteStartTime < -9.00719925474099E+15 || emoteStartTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + emoteStartTime + ") on element of EmotePlayAbstractMessage.emoteStartTime.");
            }

        }


    }
}








