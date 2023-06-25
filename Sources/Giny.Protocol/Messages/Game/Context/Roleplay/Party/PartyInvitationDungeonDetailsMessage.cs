using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyInvitationDungeonDetailsMessage : PartyInvitationDetailsMessage  
    { 
        public new const ushort Id = 9163;
        public override ushort MessageId => Id;

        public short dungeonId;
        public bool[] playersDungeonReady;

        public PartyInvitationDungeonDetailsMessage()
        {
        }
        public PartyInvitationDungeonDetailsMessage(short dungeonId,bool[] playersDungeonReady,int partyId,byte partyType,string partyName,long fromId,string fromName,long leaderId,PartyInvitationMemberInformations[] members,PartyGuestInformations[] guests)
        {
            this.dungeonId = dungeonId;
            this.playersDungeonReady = playersDungeonReady;
            this.partyId = partyId;
            this.partyType = partyType;
            this.partyName = partyName;
            this.fromId = fromId;
            this.fromName = fromName;
            this.leaderId = leaderId;
            this.members = members;
            this.guests = guests;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            writer.WriteShort((short)playersDungeonReady.Length);
            for (uint _i2 = 0;_i2 < playersDungeonReady.Length;_i2++)
            {
                writer.WriteBoolean((bool)playersDungeonReady[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            bool _val2 = false;
            base.Deserialize(reader);
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of PartyInvitationDungeonDetailsMessage.dungeonId.");
            }

            uint _playersDungeonReadyLen = (uint)reader.ReadUShort();
            playersDungeonReady = new bool[_playersDungeonReadyLen];
            for (uint _i2 = 0;_i2 < _playersDungeonReadyLen;_i2++)
            {
                _val2 = (bool)reader.ReadBoolean();
                playersDungeonReady[_i2] = (bool)_val2;
            }

        }


    }
}








