using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceFactsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7769;
        public override ushort MessageId => Id;

        public AllianceFactSheetInformations infos;
        public GuildInAllianceInformations[] guilds;
        public short[] controlledSubareaIds;
        public long leaderCharacterId;
        public string leaderCharacterName;

        public AllianceFactsMessage()
        {
        }
        public AllianceFactsMessage(AllianceFactSheetInformations infos,GuildInAllianceInformations[] guilds,short[] controlledSubareaIds,long leaderCharacterId,string leaderCharacterName)
        {
            this.infos = infos;
            this.guilds = guilds;
            this.controlledSubareaIds = controlledSubareaIds;
            this.leaderCharacterId = leaderCharacterId;
            this.leaderCharacterName = leaderCharacterName;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)infos.TypeId);
            infos.Serialize(writer);
            writer.WriteShort((short)guilds.Length);
            for (uint _i2 = 0;_i2 < guilds.Length;_i2++)
            {
                (guilds[_i2] as GuildInAllianceInformations).Serialize(writer);
            }

            writer.WriteShort((short)controlledSubareaIds.Length);
            for (uint _i3 = 0;_i3 < controlledSubareaIds.Length;_i3++)
            {
                if (controlledSubareaIds[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + controlledSubareaIds[_i3] + ") on element 3 (starting at 1) of controlledSubareaIds.");
                }

                writer.WriteVarShort((short)controlledSubareaIds[_i3]);
            }

            if (leaderCharacterId < 0 || leaderCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderCharacterId + ") on element leaderCharacterId.");
            }

            writer.WriteVarLong((long)leaderCharacterId);
            writer.WriteUTF((string)leaderCharacterName);
        }
        public override void Deserialize(IDataReader reader)
        {
            GuildInAllianceInformations _item2 = null;
            uint _val3 = 0;
            uint _id1 = (uint)reader.ReadUShort();
            infos = ProtocolTypeManager.GetInstance<AllianceFactSheetInformations>((short)_id1);
            infos.Deserialize(reader);
            uint _guildsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _guildsLen;_i2++)
            {
                _item2 = new GuildInAllianceInformations();
                _item2.Deserialize(reader);
                guilds[_i2] = _item2;
            }

            uint _controlledSubareaIdsLen = (uint)reader.ReadUShort();
            controlledSubareaIds = new short[_controlledSubareaIdsLen];
            for (uint _i3 = 0;_i3 < _controlledSubareaIdsLen;_i3++)
            {
                _val3 = (uint)reader.ReadVarUhShort();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of controlledSubareaIds.");
                }

                controlledSubareaIds[_i3] = (short)_val3;
            }

            leaderCharacterId = (long)reader.ReadVarUhLong();
            if (leaderCharacterId < 0 || leaderCharacterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderCharacterId + ") on element of AllianceFactsMessage.leaderCharacterId.");
            }

            leaderCharacterName = (string)reader.ReadUTF();
        }


    }
}








