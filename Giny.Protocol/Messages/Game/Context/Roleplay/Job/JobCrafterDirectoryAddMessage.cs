using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectoryAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 4101;
        public override ushort MessageId => Id;

        public JobCrafterDirectoryListEntry listEntry;

        public JobCrafterDirectoryAddMessage()
        {
        }
        public JobCrafterDirectoryAddMessage(JobCrafterDirectoryListEntry listEntry)
        {
            this.listEntry = listEntry;
        }
        public override void Serialize(IDataWriter writer)
        {
            listEntry.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            listEntry = new JobCrafterDirectoryListEntry();
            listEntry.Deserialize(reader);
        }


    }
}








