using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightTriggerGlyphTrapMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 7749;
        public override ushort MessageId => Id;

        public short markId;
        public short markImpactCell;
        public double triggeringCharacterId;
        public short triggeredSpellId;

        public GameActionFightTriggerGlyphTrapMessage()
        {
        }
        public GameActionFightTriggerGlyphTrapMessage(short markId,short markImpactCell,double triggeringCharacterId,short triggeredSpellId,short actionId,double sourceId)
        {
            this.markId = markId;
            this.markImpactCell = markImpactCell;
            this.triggeringCharacterId = triggeringCharacterId;
            this.triggeredSpellId = triggeredSpellId;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)markId);
            if (markImpactCell < 0)
            {
                throw new System.Exception("Forbidden value (" + markImpactCell + ") on element markImpactCell.");
            }

            writer.WriteVarShort((short)markImpactCell);
            if (triggeringCharacterId < -9.00719925474099E+15 || triggeringCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + triggeringCharacterId + ") on element triggeringCharacterId.");
            }

            writer.WriteDouble((double)triggeringCharacterId);
            if (triggeredSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + triggeredSpellId + ") on element triggeredSpellId.");
            }

            writer.WriteVarShort((short)triggeredSpellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            markId = (short)reader.ReadShort();
            markImpactCell = (short)reader.ReadVarUhShort();
            if (markImpactCell < 0)
            {
                throw new System.Exception("Forbidden value (" + markImpactCell + ") on element of GameActionFightTriggerGlyphTrapMessage.markImpactCell.");
            }

            triggeringCharacterId = (double)reader.ReadDouble();
            if (triggeringCharacterId < -9.00719925474099E+15 || triggeringCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + triggeringCharacterId + ") on element of GameActionFightTriggerGlyphTrapMessage.triggeringCharacterId.");
            }

            triggeredSpellId = (short)reader.ReadVarUhShort();
            if (triggeredSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + triggeredSpellId + ") on element of GameActionFightTriggerGlyphTrapMessage.triggeredSpellId.");
            }

        }


    }
}








