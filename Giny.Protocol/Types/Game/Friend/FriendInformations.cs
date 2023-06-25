using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FriendInformations : AbstractContactInformations  
    { 
        public new const ushort Id = 9350;
        public override ushort TypeId => Id;

        public byte playerState;
        public short lastConnection;
        public int achievementPoints;
        public short leagueId;
        public int ladderPosition;

        public FriendInformations()
        {
        }
        public FriendInformations(byte playerState,short lastConnection,int achievementPoints,short leagueId,int ladderPosition,int accountId,AccountTagInformation accountTag)
        {
            this.playerState = playerState;
            this.lastConnection = lastConnection;
            this.achievementPoints = achievementPoints;
            this.leagueId = leagueId;
            this.ladderPosition = ladderPosition;
            this.accountId = accountId;
            this.accountTag = accountTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)playerState);
            if (lastConnection < 0)
            {
                throw new System.Exception("Forbidden value (" + lastConnection + ") on element lastConnection.");
            }

            writer.WriteVarShort((short)lastConnection);
            writer.WriteInt((int)achievementPoints);
            writer.WriteVarShort((short)leagueId);
            writer.WriteInt((int)ladderPosition);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerState = (byte)reader.ReadByte();
            if (playerState < 0)
            {
                throw new System.Exception("Forbidden value (" + playerState + ") on element of FriendInformations.playerState.");
            }

            lastConnection = (short)reader.ReadVarUhShort();
            if (lastConnection < 0)
            {
                throw new System.Exception("Forbidden value (" + lastConnection + ") on element of FriendInformations.lastConnection.");
            }

            achievementPoints = (int)reader.ReadInt();
            leagueId = (short)reader.ReadVarShort();
            ladderPosition = (int)reader.ReadInt();
        }


    }
}








