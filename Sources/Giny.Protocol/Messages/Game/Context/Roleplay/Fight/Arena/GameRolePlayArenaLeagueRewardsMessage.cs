using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaLeagueRewardsMessage : NetworkMessage  
    { 
        public  const ushort Id = 4959;
        public override ushort MessageId => Id;

        public short seasonId;
        public short leagueId;
        public int ladderPosition;
        public bool endSeasonReward;

        public GameRolePlayArenaLeagueRewardsMessage()
        {
        }
        public GameRolePlayArenaLeagueRewardsMessage(short seasonId,short leagueId,int ladderPosition,bool endSeasonReward)
        {
            this.seasonId = seasonId;
            this.leagueId = leagueId;
            this.ladderPosition = ladderPosition;
            this.endSeasonReward = endSeasonReward;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (seasonId < 0)
            {
                throw new System.Exception("Forbidden value (" + seasonId + ") on element seasonId.");
            }

            writer.WriteVarShort((short)seasonId);
            if (leagueId < 0)
            {
                throw new System.Exception("Forbidden value (" + leagueId + ") on element leagueId.");
            }

            writer.WriteVarShort((short)leagueId);
            writer.WriteInt((int)ladderPosition);
            writer.WriteBoolean((bool)endSeasonReward);
        }
        public override void Deserialize(IDataReader reader)
        {
            seasonId = (short)reader.ReadVarUhShort();
            if (seasonId < 0)
            {
                throw new System.Exception("Forbidden value (" + seasonId + ") on element of GameRolePlayArenaLeagueRewardsMessage.seasonId.");
            }

            leagueId = (short)reader.ReadVarUhShort();
            if (leagueId < 0)
            {
                throw new System.Exception("Forbidden value (" + leagueId + ") on element of GameRolePlayArenaLeagueRewardsMessage.leagueId.");
            }

            ladderPosition = (int)reader.ReadInt();
            endSeasonReward = (bool)reader.ReadBoolean();
        }


    }
}








