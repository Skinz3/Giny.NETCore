using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class QuestStepInfoMessage : NetworkMessage  
    { 
        public  const ushort Id = 6202;
        public override ushort MessageId => Id;

        public QuestActiveInformations infos;

        public QuestStepInfoMessage()
        {
        }
        public QuestStepInfoMessage(QuestActiveInformations infos)
        {
            this.infos = infos;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)infos.TypeId);
            infos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            infos = ProtocolTypeManager.GetInstance<QuestActiveInformations>((short)_id1);
            infos.Deserialize(reader);
        }


    }
}








