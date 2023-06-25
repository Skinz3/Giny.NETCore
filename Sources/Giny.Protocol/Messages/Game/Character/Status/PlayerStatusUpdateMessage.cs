using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PlayerStatusUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 6964;
        public override ushort MessageId => Id;

        public int accountId;
        public long playerId;
        public PlayerStatus status;

        public PlayerStatusUpdateMessage()
        {
        }
        public PlayerStatusUpdateMessage(int accountId,long playerId,PlayerStatus status)
        {
            this.accountId = accountId;
            this.playerId = playerId;
            this.status = status;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of PlayerStatusUpdateMessage.accountId.");
            }

            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of PlayerStatusUpdateMessage.playerId.");
            }

            uint _id3 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id3);
            status.Deserialize(reader);
        }


    }
}








