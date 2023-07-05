using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class NuggetsDistributionMessage : NetworkMessage
    {
        public const ushort Id = 3737;
        public override ushort MessageId => Id;

        public NuggetsBeneficiary[] beneficiaries;

        public NuggetsDistributionMessage()
        {
        }
        public NuggetsDistributionMessage(NuggetsBeneficiary[] beneficiaries)
        {
            this.beneficiaries = beneficiaries;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)beneficiaries.Length);
            for (uint _i1 = 0; _i1 < beneficiaries.Length; _i1++)
            {
                (beneficiaries[_i1] as NuggetsBeneficiary).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            NuggetsBeneficiary _item1 = null;
            uint _beneficiariesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _beneficiariesLen; _i1++)
            {
                _item1 = new NuggetsBeneficiary();
                _item1.Deserialize(reader);
                beneficiaries[_i1] = _item1;
            }

        }

    }
}


