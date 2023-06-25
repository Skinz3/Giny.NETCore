using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationDungeonMessage : PartyInvitationMessage  
    { 
        public new const ushort Id = 2600;
        public override ushort MessageId => Id;

        public short dungeonId;

        public PartyInvitationDungeonMessage()
        {
        }
        public PartyInvitationDungeonMessage(short dungeonId,int partyId,byte partyType,string partyName,byte maxParticipants,long fromId,string fromName,long toId)
        {
            this.dungeonId = dungeonId;
            this.partyId = partyId;
            this.partyType = partyType;
            this.partyName = partyName;
            this.maxParticipants = maxParticipants;
            this.fromId = fromId;
            this.fromName = fromName;
            this.toId = toId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of PartyInvitationDungeonMessage.dungeonId.");
            }

        }


    }
}








