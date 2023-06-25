using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ItemForPresetUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 5813;
        public override ushort MessageId => Id;

        public short presetId;
        public ItemForPreset presetItem;

        public ItemForPresetUpdateMessage()
        {
        }
        public ItemForPresetUpdateMessage(short presetId,ItemForPreset presetItem)
        {
            this.presetId = presetId;
            this.presetItem = presetItem;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presetId);
            presetItem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = (short)reader.ReadShort();
            presetItem = new ItemForPreset();
            presetItem.Deserialize(reader);
        }


    }
}








