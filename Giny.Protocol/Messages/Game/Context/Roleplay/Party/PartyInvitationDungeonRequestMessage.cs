using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationDungeonRequestMessage : PartyInvitationRequestMessage  
    { 
        public new const ushort Id = 3384;
        public override ushort MessageId => Id;

        public short dungeonId;

        public PartyInvitationDungeonRequestMessage()
        {
        }
        public PartyInvitationDungeonRequestMessage(short dungeonId,AbstractPlayerSearchInformation target)
        {
            this.dungeonId = dungeonId;
            this.target = target;
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
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of PartyInvitationDungeonRequestMessage.dungeonId.");
            }

        }


    }
}








