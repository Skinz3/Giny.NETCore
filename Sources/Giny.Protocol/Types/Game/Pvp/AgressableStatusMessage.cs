using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AgressableStatusMessage
    {
        public const ushort Id = 5775;
        public virtual ushort TypeId => Id;

        public long playerId;
        public byte enable;
        public int roleAvAId;
        public int pictoScore;

        public AgressableStatusMessage()
        {
        }
        public AgressableStatusMessage(long playerId, byte enable, int roleAvAId, int pictoScore)
        {
            this.playerId = playerId;
            this.enable = enable;
            this.roleAvAId = roleAvAId;
            this.pictoScore = pictoScore;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteByte((byte)enable);
            writer.WriteInt((int)roleAvAId);
            writer.WriteInt((int)pictoScore);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AgressableStatusMessage.playerId.");
            }

            enable = (byte)reader.ReadByte();
            if (enable < 0)
            {
                throw new System.Exception("Forbidden value (" + enable + ") on element of AgressableStatusMessage.enable.");
            }

            roleAvAId = (int)reader.ReadInt();
            pictoScore = (int)reader.ReadInt();
        }


    }
}


