using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceInsiderInfoMessage : NetworkMessage
    {
        public const ushort Id = 5093;
        public override ushort MessageId => Id;

        public AllianceFactSheetInformation allianceInfos;
        public AllianceMemberInfo[] members;
        public PrismGeolocalizedInformation[] prisms;
        public TaxCollectorInformations[] taxCollectors;

        public AllianceInsiderInfoMessage()
        {
        }
        public AllianceInsiderInfoMessage(AllianceFactSheetInformation allianceInfos, AllianceMemberInfo[] members, PrismGeolocalizedInformation[] prisms, TaxCollectorInformations[] taxCollectors)
        {
            this.allianceInfos = allianceInfos;
            this.members = members;
            this.prisms = prisms;
            this.taxCollectors = taxCollectors;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceInfos.Serialize(writer);
            writer.WriteShort((short)members.Length);
            for (uint _i2 = 0; _i2 < members.Length; _i2++)
            {
                (members[_i2] as AllianceMemberInfo).Serialize(writer);
            }

            writer.WriteShort((short)prisms.Length);
            for (uint _i3 = 0; _i3 < prisms.Length; _i3++)
            {
                writer.WriteShort((short)(prisms[_i3] as PrismGeolocalizedInformation).TypeId);
                (prisms[_i3] as PrismGeolocalizedInformation).Serialize(writer);
            }

            writer.WriteShort((short)taxCollectors.Length);
            for (uint _i4 = 0; _i4 < taxCollectors.Length; _i4++)
            {
                (taxCollectors[_i4] as TaxCollectorInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AllianceMemberInfo _item2 = null;
            uint _id3 = 0;
            PrismGeolocalizedInformation _item3 = null;
            TaxCollectorInformations _item4 = null;
            allianceInfos = new AllianceFactSheetInformation();
            allianceInfos.Deserialize(reader);
            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _membersLen; _i2++)
            {
                _item2 = new AllianceMemberInfo();
                _item2.Deserialize(reader);
                members[_i2] = _item2;
            }

            uint _prismsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0; _i3 < _prismsLen; _i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<PrismGeolocalizedInformation>((short)_id3);
                _item3.Deserialize(reader);
                prisms[_i3] = _item3;
            }

            uint _taxCollectorsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0; _i4 < _taxCollectorsLen; _i4++)
            {
                _item4 = new TaxCollectorInformations();
                _item4.Deserialize(reader);
                taxCollectors[_i4] = _item4;
            }

        }

    }
}


