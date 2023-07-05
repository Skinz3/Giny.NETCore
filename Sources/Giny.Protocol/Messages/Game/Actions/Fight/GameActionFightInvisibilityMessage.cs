using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameActionFightInvisibilityMessage : AbstractGameActionMessage
    {
        public new const ushort Id = 4958;
        public override ushort MessageId => Id;

        public double targetId;
        public byte state;

        public GameActionFightInvisibilityMessage()
        {
        }
        public GameActionFightInvisibilityMessage(double targetId, byte state, short actionId, double sourceId)
        {
            this.targetId = targetId;
            this.state = state;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            writer.WriteByte((byte)state);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightInvisibilityMessage.targetId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of GameActionFightInvisibilityMessage.state.");
            }

        }

    }
}


