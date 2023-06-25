using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayShowActorWithEventMessage : GameRolePlayShowActorMessage  
    { 
        public new const ushort Id = 4614;
        public override ushort MessageId => Id;

        public byte actorEventId;

        public GameRolePlayShowActorWithEventMessage()
        {
        }
        public GameRolePlayShowActorWithEventMessage(byte actorEventId,GameRolePlayActorInformations informations)
        {
            this.actorEventId = actorEventId;
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (actorEventId < 0)
            {
                throw new System.Exception("Forbidden value (" + actorEventId + ") on element actorEventId.");
            }

            writer.WriteByte((byte)actorEventId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            actorEventId = (byte)reader.ReadByte();
            if (actorEventId < 0)
            {
                throw new System.Exception("Forbidden value (" + actorEventId + ") on element of GameRolePlayShowActorWithEventMessage.actorEventId.");
            }

        }


    }
}








