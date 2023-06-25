using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdolsPresetSaveRequestMessage : IconPresetSaveRequestMessage  
    { 
        public new const ushort Id = 1959;
        public override ushort MessageId => Id;


        public IdolsPresetSaveRequestMessage()
        {
        }
        public IdolsPresetSaveRequestMessage(short presetId,byte symbolId,bool updateData)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.updateData = updateData;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








