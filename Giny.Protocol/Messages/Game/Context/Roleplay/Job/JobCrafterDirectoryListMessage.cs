using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectoryListMessage : NetworkMessage  
    { 
        public  const ushort Id = 2905;
        public override ushort MessageId => Id;

        public JobCrafterDirectoryListEntry[] listEntries;

        public JobCrafterDirectoryListMessage()
        {
        }
        public JobCrafterDirectoryListMessage(JobCrafterDirectoryListEntry[] listEntries)
        {
            this.listEntries = listEntries;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)listEntries.Length);
            for (uint _i1 = 0;_i1 < listEntries.Length;_i1++)
            {
                (listEntries[_i1] as JobCrafterDirectoryListEntry).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            JobCrafterDirectoryListEntry _item1 = null;
            uint _listEntriesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _listEntriesLen;_i1++)
            {
                _item1 = new JobCrafterDirectoryListEntry();
                _item1.Deserialize(reader);
                listEntries[_i1] = _item1;
            }

        }


    }
}








