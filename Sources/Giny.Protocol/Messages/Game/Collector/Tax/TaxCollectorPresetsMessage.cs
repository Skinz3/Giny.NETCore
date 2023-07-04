using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorPresetsMessage : NetworkMessage
    {
        public const ushort Id = 6867;
        public override ushort MessageId => Id;

        public TaxCollectorPreset[] presets;

        public TaxCollectorPresetsMessage()
        {
        }
        public TaxCollectorPresetsMessage(TaxCollectorPreset[] presets)
        {
            this.presets = presets;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presets.Length);
            for (uint _i1 = 0; _i1 < presets.Length; _i1++)
            {
                (presets[_i1] as TaxCollectorPreset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorPreset _item1 = null;
            uint _presetsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _presetsLen; _i1++)
            {
                _item1 = new TaxCollectorPreset();
                _item1.Deserialize(reader);
                presets[_i1] = _item1;
            }

        }

    }
}


