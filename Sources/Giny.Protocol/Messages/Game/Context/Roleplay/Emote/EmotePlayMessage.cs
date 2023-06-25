using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EmotePlayMessage : EmotePlayAbstractMessage  
    { 
        public new const ushort Id = 792;
        public override ushort MessageId => Id;

        public double actorId;
        public int accountId;

        public EmotePlayMessage()
        {
        }
        public EmotePlayMessage(double actorId,int accountId,short emoteId,double emoteStartTime)
        {
            this.actorId = actorId;
            this.accountId = accountId;
            this.emoteId = emoteId;
            this.emoteStartTime = emoteStartTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            actorId = (double)reader.ReadDouble();
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of EmotePlayMessage.actorId.");
            }

            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of EmotePlayMessage.accountId.");
            }

        }


    }
}








