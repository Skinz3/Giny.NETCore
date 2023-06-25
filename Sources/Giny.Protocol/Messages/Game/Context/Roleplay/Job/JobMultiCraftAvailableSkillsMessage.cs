using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobMultiCraftAvailableSkillsMessage : JobAllowMultiCraftRequestMessage  
    { 
        public new const ushort Id = 5866;
        public override ushort MessageId => Id;

        public long playerId;
        public short[] skills;

        public JobMultiCraftAvailableSkillsMessage()
        {
        }
        public JobMultiCraftAvailableSkillsMessage(long playerId,short[] skills,bool enabled)
        {
            this.playerId = playerId;
            this.skills = skills;
            this.enabled = enabled;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteShort((short)skills.Length);
            for (uint _i2 = 0;_i2 < skills.Length;_i2++)
            {
                if (skills[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + skills[_i2] + ") on element 2 (starting at 1) of skills.");
                }

                writer.WriteVarShort((short)skills[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of JobMultiCraftAvailableSkillsMessage.playerId.");
            }

            uint _skillsLen = (uint)reader.ReadUShort();
            skills = new short[_skillsLen];
            for (uint _i2 = 0;_i2 < _skillsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of skills.");
                }

                skills[_i2] = (short)_val2;
            }

        }


    }
}








