using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceRanksMessage : NetworkMessage
    {
        public const ushort Id = 9275;
        public override ushort MessageId => Id;

        public RankInformation[] ranks;

        public AllianceRanksMessage()
        {
        }
        public AllianceRanksMessage(RankInformation[] ranks)
        {
            this.ranks = ranks;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ranks.Length);
            for (uint _i1 = 0; _i1 < ranks.Length; _i1++)
            {
                (ranks[_i1] as RankInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            RankInformation _item1 = null;
            uint _ranksLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _ranksLen; _i1++)
            {
                _item1 = new RankInformation();
                _item1.Deserialize(reader);
                ranks[_i1] = _item1;
            }

        }

    }
}


