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
        public new const ushort Id = 8107;
        public override ushort MessageId => Id;

        public short weaponGenericId;

        public GameActionFightCloseCombatMessage()
        {
        }
        public GameActionFightCloseCombatMessage(short weaponGenericId,short actionId,double sourceId,double targetId,short destinationCellId,byte critical,bool silentCast,bool verboseCast)
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

            writer.WriteVarShort((short)weaponGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            weaponGenericId = (short)reader.ReadVarUhShort();
            if (weaponGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + weaponGenericId + ") on element of GameActionFightCloseCombatMessage.weaponGenericId.");
            }

        }


    }
}








