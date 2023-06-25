using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PresetsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6378;
        public override ushort MessageId => Id;

        public Preset[] presets;

        public PresetsMessage()
        {
        }
        public PresetsMessage(Preset[] presets)
        {
            this.presets = presets;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presets.Length);
            for (uint _i1 = 0;_i1 < presets.Length;_i1++)
            {
                writer.WriteShort((short)(presets[_i1] as Preset).TypeId);
                (presets[_i1] as Preset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            Preset _item1 = null;
            uint _presetsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _presetsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<Preset>((short)_id1);
                _item1.Deserialize(reader);
                presets[_i1] = _item1;
            }

        }


    }
}








