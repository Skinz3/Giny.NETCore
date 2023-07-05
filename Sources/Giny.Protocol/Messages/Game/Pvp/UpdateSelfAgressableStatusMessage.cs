using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class UpdateSelfAgressableStatusMessage : NetworkMessage
    {
        public const ushort Id = 8145;
        public override ushort MessageId => Id;

        public byte status;
        public double probationTime;
        public int roleAvAId;
        public int pictoScore;

        public UpdateSelfAgressableStatusMessage()
        {
        }
        public UpdateSelfAgressableStatusMessage(byte status, double probationTime, int roleAvAId, int pictoScore)
        {
            this.status = status;
            this.probationTime = probationTime;
            this.roleAvAId = roleAvAId;
            this.pictoScore = pictoScore;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)status);
            if (probationTime < 0 || probationTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element probationTime.");
            }

            writer.WriteDouble((double)probationTime);
            writer.WriteInt((int)roleAvAId);
            writer.WriteInt((int)pictoScore);
        }
        public override void Deserialize(IDataReader reader)
        {
            status = (byte)reader.ReadByte();
            if (status < 0)
            {
                throw new System.Exception("Forbidden value (" + status + ") on element of UpdateSelfAgressableStatusMessage.status.");
            }

            probationTime = (double)reader.ReadDouble();
            if (probationTime < 0 || probationTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element of UpdateSelfAgressableStatusMessage.probationTime.");
            }

            roleAvAId = (int)reader.ReadInt();
            pictoScore = (int)reader.ReadInt();
        }

    }
}


