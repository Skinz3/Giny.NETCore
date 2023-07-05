using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class SetCharacterRestrictionsMessage : NetworkMessage
    {
        public const ushort Id = 8088;
        public override ushort MessageId => Id;

        public double actorId;
        public ActorRestrictionsInformations restrictions;

        public SetCharacterRestrictionsMessage()
        {
        }
        public SetCharacterRestrictionsMessage(double actorId, ActorRestrictionsInformations restrictions)
        {
            this.actorId = actorId;
            this.restrictions = restrictions;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            restrictions.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            actorId = (double)reader.ReadDouble();
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of SetCharacterRestrictionsMessage.actorId.");
            }

            restrictions = new ActorRestrictionsInformations();
            restrictions.Deserialize(reader);
        }

    }
}


