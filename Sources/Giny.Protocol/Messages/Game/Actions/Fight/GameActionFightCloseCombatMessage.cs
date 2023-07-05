using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameActionFightCloseCombatMessage : AbstractGameActionFightTargetedAbilityMessage
    {
        public new const ushort Id = 1826;
        public override ushort MessageId => Id;

        public int weaponGenericId;

        public GameActionFightCloseCombatMessage()
        {
        }
        public GameActionFightCloseCombatMessage(int weaponGenericId, short actionId, double sourceId, double targetId, short destinationCellId, byte critical, bool silentCast, bool verboseCast)
        {
            this.weaponGenericId = weaponGenericId;
            this.actionId = actionId;
            this.sourceId = sourceId;
            this.targetId = targetId;
            this.destinationCellId = destinationCellId;
            this.critical = critical;
            this.silentCast = silentCast;
            this.verboseCast = verboseCast;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (weaponGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + weaponGenericId + ") on element weaponGenericId.");
            }

            writer.WriteVarInt((int)weaponGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            weaponGenericId = (int)reader.ReadVarUhInt();
            if (weaponGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + weaponGenericId + ") on element of GameActionFightCloseCombatMessage.weaponGenericId.");
            }

        }

    }
}


