using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightSpellCastMessage : AbstractGameActionFightTargetedAbilityMessage  
    { 
        public new const ushort Id = 65;
        public override ushort MessageId => Id;

        public short spellId;
        public short spellLevel;
        public short[] portalsIds;

        public GameActionFightSpellCastMessage()
        {
        }
        public GameActionFightSpellCastMessage(short spellId,short spellLevel,short[] portalsIds,short actionId,double sourceId,double targetId,short destinationCellId,byte critical,bool silentCast,bool verboseCast)
        {
            this.spellId = spellId;
            this.spellLevel = spellLevel;
            this.portalsIds = portalsIds;
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
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            if (spellLevel < 1 || spellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + spellLevel + ") on element spellLevel.");
            }

            writer.WriteShort((short)spellLevel);
            writer.WriteShort((short)portalsIds.Length);
            for (uint _i3 = 0;_i3 < portalsIds.Length;_i3++)
            {
                writer.WriteShort((short)portalsIds[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val3 = 0;
            base.Deserialize(reader);
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of GameActionFightSpellCastMessage.spellId.");
            }

            spellLevel = (short)reader.ReadShort();
            if (spellLevel < 1 || spellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + spellLevel + ") on element of GameActionFightSpellCastMessage.spellLevel.");
            }

            uint _portalsIdsLen = (uint)reader.ReadUShort();
            portalsIds = new short[_portalsIdsLen];
            for (uint _i3 = 0;_i3 < _portalsIdsLen;_i3++)
            {
                _val3 = (int)reader.ReadShort();
                portalsIds[_i3] = (short)_val3;
            }

        }


    }
}








