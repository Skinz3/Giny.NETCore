using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightSpectatorJoinMessage : GameFightJoinMessage  
    { 
        public new const ushort Id = 4519;
        public override ushort MessageId => Id;

        public NamedPartyTeam[] namedPartyTeams;

        public GameFightSpectatorJoinMessage()
        {
        }
        public GameFightSpectatorJoinMessage(NamedPartyTeam[] namedPartyTeams,bool isTeamPhase,bool canBeCancelled,bool canSayReady,bool isFightStarted,short timeMaxBeforeFightStart,byte fightType)
        {
            this.namedPartyTeams = namedPartyTeams;
            this.isTeamPhase = isTeamPhase;
            this.canBeCancelled = canBeCancelled;
            this.canSayReady = canSayReady;
            this.isFightStarted = isFightStarted;
            this.timeMaxBeforeFightStart = timeMaxBeforeFightStart;
            this.fightType = fightType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)namedPartyTeams.Length);
            for (uint _i1 = 0;_i1 < namedPartyTeams.Length;_i1++)
            {
                (namedPartyTeams[_i1] as NamedPartyTeam).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            NamedPartyTeam _item1 = null;
            base.Deserialize(reader);
            uint _namedPartyTeamsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _namedPartyTeamsLen;_i1++)
            {
                _item1 = new NamedPartyTeam();
                _item1.Deserialize(reader);
                namedPartyTeams[_i1] = _item1;
            }

        }


    }
}








