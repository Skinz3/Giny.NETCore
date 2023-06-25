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
        public  const ushort Id = 1944;
        public override ushort MessageId => Id;

        public byte status;
        public int probationTime;

        public UpdateSelfAgressableStatusMessage()
        {
        }
        public UpdateSelfAgressableStatusMessage(byte status,int probationTime)
        {
            this.status = status;
            this.probationTime = probationTime;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)status);
            if (probationTime < 0)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element probationTime.");
            }

            writer.WriteInt((int)probationTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            status = (byte)reader.ReadByte();
            if (status < 0)
            {
                throw new System.Exception("Forbidden value (" + status + ") on element of UpdateSelfAgressableStatusMessage.status.");
            }

            probationTime = (int)reader.ReadInt();
            if (probationTime < 0)
            {
                throw new System.Exception("Forbidden value (" + probationTime + ") on element of UpdateSelfAgressableStatusMessage.probationTime.");
            }

        }


    }
}








