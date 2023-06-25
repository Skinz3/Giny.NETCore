using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AcquaintanceOnlineInformation : AcquaintanceInformation  
    { 
        public new const ushort Id = 4753;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public short moodSmileyId;
        public PlayerStatus status;

        public AcquaintanceOnlineInformation()
        {
        }
        public AcquaintanceOnlineInformation(long playerId,string playerName,short moodSmileyId,PlayerStatus status,int accountId,AccountTagInformation accountTag,byte playerState)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.moodSmileyId = moodSmileyId;
            this.status = status;
            this.accountId = accountId;
            this.accountTag = accountTag;
            this.playerState = playerState;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element moodSmileyId.");
            }

            writer.WriteVarShort((short)moodSmileyId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AcquaintanceOnlineInformation.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            moodSmileyId = (short)reader.ReadVarUhShort();
            if (moodSmileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + moodSmileyId + ") on element of AcquaintanceOnlineInformation.moodSmileyId.");
            }

            uint _id4 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id4);
            status.Deserialize(reader);
        }


    }
}








