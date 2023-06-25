using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class NamedPartyTeam  
    { 
        public const ushort Id = 8422;
        public virtual ushort TypeId => Id;

        public byte teamId;
        public string partyName;

        public NamedPartyTeam()
        {
        }
        public NamedPartyTeam(byte teamId,string partyName)
        {
            this.teamId = teamId;
            this.partyName = partyName;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)teamId);
            writer.WriteUTF((string)partyName);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            teamId = (byte)reader.ReadByte();
            if (teamId < 0)
            {
                throw new System.Exception("Forbidden value (" + teamId + ") on element of NamedPartyTeam.teamId.");
            }

            partyName = (string)reader.ReadUTF();
        }


    }
}








