using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightAttackerRemoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 3537;
        public override ushort MessageId => Id;

        public short subAreaId;
        public short fightId;
        public long fighterToRemoveId;

        public PrismFightAttackerRemoveMessage()
        {
        }
        public PrismFightAttackerRemoveMessage(short subAreaId,short fightId,long fighterToRemoveId)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.fighterToRemoveId = fighterToRemoveId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            if (fighterToRemoveId < 0 || fighterToRemoveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fighterToRemoveId + ") on element fighterToRemoveId.");
            }

            writer.WriteVarLong((long)fighterToRemoveId);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightAttackerRemoveMessage.subAreaId.");
            }

            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of PrismFightAttackerRemoveMessage.fightId.");
            }

            fighterToRemoveId = (long)reader.ReadVarUhLong();
            if (fighterToRemoveId < 0 || fighterToRemoveId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + fighterToRemoveId + ") on element of PrismFightAttackerRemoveMessage.fighterToRemoveId.");
            }

        }


    }
}








