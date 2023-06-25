using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShowCellSpectatorMessage : ShowCellMessage  
    { 
        public new const ushort Id = 4547;
        public override ushort MessageId => Id;

        public string playerName;

        public ShowCellSpectatorMessage()
        {
        }
        public ShowCellSpectatorMessage(string playerName,double sourceId,short cellId)
        {
            this.playerName = playerName;
            this.sourceId = sourceId;
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)playerName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerName = (string)reader.ReadUTF();
        }


    }
}








