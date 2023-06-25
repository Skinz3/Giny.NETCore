using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobBookSubscription  
    { 
        public const ushort Id = 2471;
        public virtual ushort TypeId => Id;

        public byte jobId;
        public bool subscribed;

        public JobBookSubscription()
        {
        }
        public JobBookSubscription(byte jobId,bool subscribed)
        {
            this.jobId = jobId;
            this.subscribed = subscribed;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            writer.WriteBoolean((bool)subscribed);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobBookSubscription.jobId.");
            }

            subscribed = (bool)reader.ReadBoolean();
        }


    }
}








