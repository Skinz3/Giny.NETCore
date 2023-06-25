using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class JobCrafterDirectoryListEntry  
    { 
        public const ushort Id = 2849;
        public virtual ushort TypeId => Id;

        public JobCrafterDirectoryEntryPlayerInfo playerInfo;
        public JobCrafterDirectoryEntryJobInfo jobInfo;

        public JobCrafterDirectoryListEntry()
        {
        }
        public JobCrafterDirectoryListEntry(JobCrafterDirectoryEntryPlayerInfo playerInfo,JobCrafterDirectoryEntryJobInfo jobInfo)
        {
            this.playerInfo = playerInfo;
            this.jobInfo = jobInfo;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            playerInfo.Serialize(writer);
            jobInfo.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerInfo = new JobCrafterDirectoryEntryPlayerInfo();
            playerInfo.Deserialize(reader);
            jobInfo = new JobCrafterDirectoryEntryJobInfo();
            jobInfo.Deserialize(reader);
        }


    }
}








