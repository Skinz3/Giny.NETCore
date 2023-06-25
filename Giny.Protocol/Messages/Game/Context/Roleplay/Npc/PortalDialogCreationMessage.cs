using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PortalDialogCreationMessage : NpcDialogCreationMessage  
    { 
        public new const ushort Id = 2360;
        public override ushort MessageId => Id;

        public int type;

        public PortalDialogCreationMessage()
        {
        }
        public PortalDialogCreationMessage(int type,double mapId,int npcId)
        {
            this.type = type;
            this.mapId = mapId;
            this.npcId = npcId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            type = (int)reader.ReadInt();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of PortalDialogCreationMessage.type.");
            }

        }


    }
}








