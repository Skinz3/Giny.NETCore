using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SelectedServerRefusedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2315;
        public override ushort MessageId => Id;

        public short serverId;
        public byte error;
        public byte serverStatus;

        public SelectedServerRefusedMessage()
        {
        }
        public SelectedServerRefusedMessage(short serverId,byte error,byte serverStatus)
        {
            this.serverId = serverId;
            this.error = error;
            this.serverStatus = serverStatus;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element serverId.");
            }

            writer.WriteVarShort((short)serverId);
            writer.WriteByte((byte)error);
            writer.WriteByte((byte)serverStatus);
        }
        public override void Deserialize(IDataReader reader)
        {
            serverId = (short)reader.ReadVarUhShort();
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element of SelectedServerRefusedMessage.serverId.");
            }

            error = (byte)reader.ReadByte();
            if (error < 0)
            {
                throw new System.Exception("Forbidden value (" + error + ") on element of SelectedServerRefusedMessage.error.");
            }

            serverStatus = (byte)reader.ReadByte();
            if (serverStatus < 0)
            {
                throw new System.Exception("Forbidden value (" + serverStatus + ") on element of SelectedServerRefusedMessage.serverStatus.");
            }

        }


    }
}








