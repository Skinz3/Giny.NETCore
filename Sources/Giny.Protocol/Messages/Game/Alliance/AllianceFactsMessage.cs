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
        public const ushort Id = 3798;
        public override ushort MessageId => Id;

        public AllianceFactSheetInformation infos;
        public CharacterMinimalSocialPublicInformations[] members;
        public short[] controlledSubareaIds;
        public long leaderCharacterId;
        public string leaderCharacterName;

        public AllianceFactsMessage()
        {
        }
        public AllianceFactsMessage(AllianceFactSheetInformation infos, CharacterMinimalSocialPublicInformations[] members, short[] controlledSubareaIds, long leaderCharacterId, string leaderCharacterName)
        {
            this.infos = infos;
            this.members = members;
            this.controlledSubareaIds = controlledSubareaIds;
            this.leaderCharacterId = leaderCharacterId;
            this.leaderCharacterName = leaderCharacterName;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)infos.TypeId);
            infos.Serialize(writer);
            writer.WriteShort((short)members.Length);
            for (uint _i2 = 0; _i2 < members.Length; _i2++)
            {
                (members[_i2] as CharacterMinimalSocialPublicInformations).Serialize(writer);
            }

            writer.WriteShort((short)controlledSubareaIds.Length);
            for (uint _i3 = 0; _i3 < controlledSubareaIds.Length; _i3++)
            {
                if (controlledSubareaIds[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + controlledSubareaIds[_i3] + ") on element 3 (starting at 1) of controlledSubareaIds.");
                }

                writer.WriteVarShort((short)controlledSubareaIds[_i3]);
            }

            if (leaderCharacterId < 0 || leaderCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + leaderCharacterId + ") on element leaderCharacterId.");
            }

            writer.WriteVarLong((long)leaderCharacterId);
            writer.WriteUTF((string)leaderCharacterName);
        }
        public override void Deserialize(IDataReader reader)
        {
            CharacterMinimalSocialPublicInformations _item2 = null;
            uint _val3 = 0;
            uint _id1 = (uint)reader.ReadUShort();
            infos = ProtocolTypeManager.GetInstance<AllianceFactSheetInformation>((short)_id1);
            infos.Deserialize(reader);
            uint _membersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _membersLen; _i2++)
            {
                _item2 = new CharacterMinimalSocialPublicInformations();
                _item2.Deserialize(reader);
                members[_i2] = _item2;
            }

            uint _controlledSubareaIdsLen = (uint)reader.ReadUShort();
            controlledSubareaIds = new short[_controlledSubareaIdsLen];
            for (uint _i3 = 0; _i3 < _controlledSubareaIdsLen; _i3++)
            {
                _val3 = (uint)reader.ReadVarUhShort();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of controlledSubareaIds.");
                }

                controlledSubareaIds[_i3] = (short)_val3;
            }

            leaderCharacterId = (long)reader.ReadVarUhLong();
            if (leaderCharacterId < 0 || leaderCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + leaderCharacterId + ") on element of AllianceFactsMessage.leaderCharacterId.");
            }

            leaderCharacterName = (string)reader.ReadUTF();
        }

    }
}


