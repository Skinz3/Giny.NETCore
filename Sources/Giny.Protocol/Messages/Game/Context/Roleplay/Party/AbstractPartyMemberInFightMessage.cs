using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AbstractPartyMemberInFightMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 1165;
        public override ushort MessageId => Id;

        public byte reason;
        public long memberId;
        public int memberAccountId;
        public string memberName;
        public short fightId;
        public short timeBeforeFightStart;

        public AbstractPartyMemberInFightMessage()
        {
        }
        public AbstractPartyMemberInFightMessage(byte reason,long memberId,int memberAccountId,string memberName,short fightId,short timeBeforeFightStart,int partyId)
        {
            this.reason = reason;
            this.memberId = memberId;
            this.memberAccountId = memberAccountId;
            this.memberName = memberName;
            this.fightId = fightId;
            this.timeBeforeFightStart = timeBeforeFightStart;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)reason);
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            if (memberAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + memberAccountId + ") on element memberAccountId.");
            }

            writer.WriteInt((int)memberAccountId);
            writer.WriteUTF((string)memberName);
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element fightId.");
            }

            writer.WriteVarShort((short)fightId);
            writer.WriteVarShort((short)timeBeforeFightStart);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            reason = (byte)reader.ReadByte();
            if (reason < 0)
            {
                throw new System.Exception("Forbidden value (" + reason + ") on element of AbstractPartyMemberInFightMessage.reason.");
            }

            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of AbstractPartyMemberInFightMessage.memberId.");
            }

            memberAccountId = (int)reader.ReadInt();
            if (memberAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + memberAccountId + ") on element of AbstractPartyMemberInFightMessage.memberAccountId.");
            }

            memberName = (string)reader.ReadUTF();
            fightId = (short)reader.ReadVarUhShort();
            if (fightId < 0)
            {
                throw new System.Exception("Forbidden value (" + fightId + ") on element of AbstractPartyMemberInFightMessage.fightId.");
            }

            timeBeforeFightStart = (short)reader.ReadVarShort();
        }


    }
}








