using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameActionFightReflectDamagesMessage : AbstractGameActionMessage
    {
        public new const ushort Id = 2532;
        public override ushort MessageId => Id;

        public double targetId;

        public GameActionFightReflectDamagesMessage()
        {
        }
        public GameActionFightReflectDamagesMessage(double targetId, short actionId, double sourceId)
        {
            this.targetId = targetId;
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
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightReflectDamagesMessage.targetId.");
            }

        }

    }
}


