using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaInvitationCandidatesAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 5498;
        public override ushort MessageId => Id;

        public LeagueFriendInformations[] candidates;

        public GameRolePlayArenaInvitationCandidatesAnswerMessage()
        {
        }
        public GameRolePlayArenaInvitationCandidatesAnswerMessage(LeagueFriendInformations[] candidates)
        {
            this.candidates = candidates;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)candidates.Length);
            for (uint _i1 = 0;_i1 < candidates.Length;_i1++)
            {
                (candidates[_i1] as LeagueFriendInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            LeagueFriendInformations _item1 = null;
            uint _candidatesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _candidatesLen;_i1++)
            {
                _item1 = new LeagueFriendInformations();
                _item1.Deserialize(reader);
                candidates[_i1] = _item1;
            }

        }


    }
}








