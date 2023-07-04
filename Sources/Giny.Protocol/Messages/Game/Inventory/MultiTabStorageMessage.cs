using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MultiTabStorageMessage : NetworkMessage
    {
        public const ushort Id = 1723;
        public override ushort MessageId => Id;

        public StorageTabInformation[] tabs;

        public MultiTabStorageMessage()
        {
        }
        public MultiTabStorageMessage(StorageTabInformation[] tabs)
        {
            this.tabs = tabs;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)tabs.Length);
            for (uint _i1 = 0; _i1 < tabs.Length; _i1++)
            {
                (tabs[_i1] as StorageTabInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            StorageTabInformation _item1 = null;
            uint _tabsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _tabsLen; _i1++)
            {
                _item1 = new StorageTabInformation();
                _item1.Deserialize(reader);
                tabs[_i1] = _item1;
            }

        }

    }
}


