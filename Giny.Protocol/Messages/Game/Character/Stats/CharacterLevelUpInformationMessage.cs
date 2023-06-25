using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterLevelUpInformationMessage : CharacterLevelUpMessage  
    { 
        public new const ushort Id = 9230;
        public override ushort MessageId => Id;

        public string name;
        public long id;

        public CharacterLevelUpInformationMessage()
        {
        }
        public CharacterLevelUpInformationMessage(string name,long id,short newLevel)
        {
            this.name = name;
            this.id = id;
            this.newLevel = newLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of CharacterLevelUpInformationMessage.id.");
            }

        }


    }
}








