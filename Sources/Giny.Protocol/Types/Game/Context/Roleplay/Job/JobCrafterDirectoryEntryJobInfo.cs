using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobCrafterDirectoryEntryJobInfo  
    { 
        public const ushort Id = 491;
        public virtual ushort TypeId => Id;

        public byte jobId;
        public byte jobLevel;
        public bool free;
        public byte minLevel;

        public JobCrafterDirectoryEntryJobInfo()
        {
        }
        public JobCrafterDirectoryEntryJobInfo(byte jobId,byte jobLevel,bool free,byte minLevel)
        {
            this.jobId = jobId;
            this.jobLevel = jobLevel;
            this.free = free;
            this.minLevel = minLevel;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            if (jobLevel < 1 || jobLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + jobLevel + ") on element jobLevel.");
            }

            writer.WriteByte((byte)jobLevel);
            writer.WriteBoolean((bool)free);
            if (minLevel < 0 || minLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element minLevel.");
            }

            writer.WriteByte((byte)minLevel);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobCrafterDirectoryEntryJobInfo.jobId.");
            }

            jobLevel = (byte)reader.ReadSByte();
            if (jobLevel < 1 || jobLevel > 200)
            {
                throw new System.Exception("Forbidden value (" + jobLevel + ") on element of JobCrafterDirectoryEntryJobInfo.jobLevel.");
            }

            free = (bool)reader.ReadBoolean();
            minLevel = (byte)reader.ReadSByte();
            if (minLevel < 0 || minLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element of JobCrafterDirectoryEntryJobInfo.minLevel.");
            }

        }


    }
}








