using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightDefenderAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 3949;
        public override ushort MessageId => Id;

        public short subAreaId;
        public short fightId;
        public CharacterMinimalPlusLookInformations defender;

        public PrismFightDefenderAddMessage()
        {
        }
        public PrismFightDefenderAddMessage(short subAreaId,short fightId,CharacterMinimalPlusLookInformations defender)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.defender = defender;
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
            writer.WriteShort((short)defender.TypeId);
            defender.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightDefenderAddMessage.subAreaId.");
            }

            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of PrismFightDefenderAddMessage.fightId.");
            }

            uint _id3 = (uint)reader.ReadUShort();
            defender = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id3);
            defender.Deserialize(reader);
        }


    }
}








