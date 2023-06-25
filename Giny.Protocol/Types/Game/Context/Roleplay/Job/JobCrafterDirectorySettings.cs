using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobCrafterDirectorySettings  
    { 
        public const ushort Id = 2478;
        public virtual ushort TypeId => Id;

        public byte jobId;
        public byte minLevel;
        public bool free;

        public JobCrafterDirectorySettings()
        {
        }
        public JobCrafterDirectorySettings(byte jobId,byte minLevel,bool free)
        {
            this.jobId = jobId;
            this.minLevel = minLevel;
            this.free = free;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element jobId.");
            }

            writer.WriteByte((byte)jobId);
            if (minLevel < 0 || minLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element minLevel.");
            }

            writer.WriteByte((byte)minLevel);
            writer.WriteBoolean((bool)free);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            jobId = (byte)reader.ReadByte();
            if (jobId < 0)
            {
                throw new System.Exception("Forbidden value (" + jobId + ") on element of JobCrafterDirectorySettings.jobId.");
            }

            minLevel = (byte)reader.ReadSByte();
            if (minLevel < 0 || minLevel > 255)
            {
                throw new System.Exception("Forbidden value (" + minLevel + ") on element of JobCrafterDirectorySettings.minLevel.");
            }

            free = (bool)reader.ReadBoolean();
        }


    }
}








