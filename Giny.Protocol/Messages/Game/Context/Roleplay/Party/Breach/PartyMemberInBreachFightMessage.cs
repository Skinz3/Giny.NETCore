using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyMemberInBreachFightMessage : AbstractPartyMemberInFightMessage  
    { 
        public new const ushort Id = 1630;
        public override ushort MessageId => Id;

        public int floor;
        public byte room;

        public PartyMemberInBreachFightMessage()
        {
        }
        public PartyMemberInBreachFightMessage(int floor,byte room,int partyId,byte reason,long memberId,int memberAccountId,string memberName,short fightId,short timeBeforeFightStart)
        {
            this.floor = floor;
            this.room = room;
            this.partyId = partyId;
            this.reason = reason;
            this.memberId = memberId;
            this.memberAccountId = memberAccountId;
            this.memberName = memberName;
            this.fightId = fightId;
            this.timeBeforeFightStart = timeBeforeFightStart;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (floor < 0)
            {
                throw new System.Exception("Forbidden value (" + floor + ") on element floor.");
            }

            writer.WriteVarInt((int)floor);
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element room.");
            }

            writer.WriteByte((byte)room);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            floor = (int)reader.ReadVarUhInt();
            if (floor < 0)
            {
                throw new System.Exception("Forbidden value (" + floor + ") on element of PartyMemberInBreachFightMessage.floor.");
            }

            room = (byte)reader.ReadByte();
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element of PartyMemberInBreachFightMessage.room.");
            }

        }


    }
}








