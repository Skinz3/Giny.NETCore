using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightInfoMessage : NetworkMessage
    {
        public const ushort Id = 5259;
        public override ushort MessageId => Id;

        public SocialFight[] allianceFights;

        public AllianceFightInfoMessage()
        {
        }
        public AllianceFightInfoMessage(SocialFight[] allianceFights)
        {
            this.allianceFights = allianceFights;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)allianceFights.Length);
            for (uint _i1 = 0; _i1 < allianceFights.Length; _i1++)
            {
                (allianceFights[_i1] as SocialFight).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            SocialFight _item1 = null;
            uint _allianceFightsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _allianceFightsLen; _i1++)
            {
                _item1 = new SocialFight();
                _item1.Deserialize(reader);
                allianceFights[_i1] = _item1;
            }

        }

    }
}


