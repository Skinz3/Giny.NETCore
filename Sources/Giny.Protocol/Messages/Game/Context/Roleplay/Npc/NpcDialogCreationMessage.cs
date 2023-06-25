using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NpcDialogCreationMessage : NetworkMessage  
    { 
        public  const ushort Id = 8508;
        public override ushort MessageId => Id;

        public double mapId;
        public int npcId;

        public NpcDialogCreationMessage()
        {
        }
        public NpcDialogCreationMessage(double mapId,int npcId)
        {
            this.mapId = mapId;
            this.npcId = npcId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteInt((int)npcId);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of NpcDialogCreationMessage.mapId.");
            }

            npcId = (int)reader.ReadInt();
        }


    }
}








