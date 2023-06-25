using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightSpellImmunityMessage : AbstractGameActionMessage  
    { 
        public new const ushort Id = 7073;
        public override ushort MessageId => Id;

        public double targetId;
        public short spellId;

        public GameActionFightSpellImmunityMessage()
        {
        }
        public GameActionFightSpellImmunityMessage(double targetId,short spellId,short actionId,double sourceId)
        {
            this.targetId = targetId;
            this.spellId = spellId;
            this.actionId = actionId;
            this.sourceId = sourceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameActionFightSpellImmunityMessage.targetId.");
            }

            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of GameActionFightSpellImmunityMessage.spellId.");
            }

        }


    }
}








