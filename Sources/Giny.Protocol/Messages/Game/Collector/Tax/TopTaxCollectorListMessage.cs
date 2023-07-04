using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TopTaxCollectorListMessage : NetworkMessage
    {
        public const ushort Id = 4096;
        public override ushort MessageId => Id;

        public TaxCollectorInformations[] dungeonTaxCollectorsInformation;
        public TaxCollectorInformations[] worldTaxCollectorsInformation;

        public TopTaxCollectorListMessage()
        {
        }
        public TopTaxCollectorListMessage(TaxCollectorInformations[] dungeonTaxCollectorsInformation, TaxCollectorInformations[] worldTaxCollectorsInformation)
        {
            this.dungeonTaxCollectorsInformation = dungeonTaxCollectorsInformation;
            this.worldTaxCollectorsInformation = worldTaxCollectorsInformation;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)dungeonTaxCollectorsInformation.Length);
            for (uint _i1 = 0; _i1 < dungeonTaxCollectorsInformation.Length; _i1++)
            {
                writer.WriteShort((short)(dungeonTaxCollectorsInformation[_i1] as TaxCollectorInformations).TypeId);
                (dungeonTaxCollectorsInformation[_i1] as TaxCollectorInformations).Serialize(writer);
            }

            writer.WriteShort((short)worldTaxCollectorsInformation.Length);
            for (uint _i2 = 0; _i2 < worldTaxCollectorsInformation.Length; _i2++)
            {
                writer.WriteShort((short)(worldTaxCollectorsInformation[_i2] as TaxCollectorInformations).TypeId);
                (worldTaxCollectorsInformation[_i2] as TaxCollectorInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            TaxCollectorInformations _item1 = null;
            uint _id2 = 0;
            TaxCollectorInformations _item2 = null;
            uint _dungeonTaxCollectorsInformationLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _dungeonTaxCollectorsInformationLen; _i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<TaxCollectorInformations>((short)_id1);
                _item1.Deserialize(reader);
                dungeonTaxCollectorsInformation[_i1] = _item1;
            }

            uint _worldTaxCollectorsInformationLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _worldTaxCollectorsInformationLen; _i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<TaxCollectorInformations>((short)_id2);
                _item2.Deserialize(reader);
                worldTaxCollectorsInformation[_i2] = _item2;
            }

        }

    }
}


