using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PrismFightAttackerAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 5938;
        public override ushort MessageId => Id;

        public short subAreaId;
        public short fightId;
        public CharacterMinimalPlusLookInformations attacker;

        public PrismFightAttackerAddMessage()
        {
        }
        public PrismFightAttackerAddMessage(short subAreaId,short fightId,CharacterMinimalPlusLookInformations attacker)
        {
            this.subAreaId = subAreaId;
            this.fightId = fightId;
            this.attacker = attacker;
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
            writer.WriteShort((short)attacker.TypeId);
            attacker.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PrismFightAttackerAddMessage.subAreaId.");
            }

            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of PrismFightAttackerAddMessage.fightId.");
            }

            uint _id3 = (uint)reader.ReadUShort();
            attacker = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>((short)_id3);
            attacker.Deserialize(reader);
        }


    }
}








