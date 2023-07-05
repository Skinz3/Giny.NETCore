using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ApplySpellModifierMessage : NetworkMessage
    {
        public const ushort Id = 9665;
        public override ushort MessageId => Id;

        public double actorId;
        public SpellModifierMessage modifier;

        public ApplySpellModifierMessage()
        {
        }
        public ApplySpellModifierMessage(double actorId, SpellModifierMessage modifier)
        {
            this.actorId = actorId;
            this.modifier = modifier;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            modifier.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            actorId = (double)reader.ReadDouble();
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of ApplySpellModifierMessage.actorId.");
            }

            modifier = new SpellModifierMessage();
            modifier.Deserialize(reader);
        }

    }
}


