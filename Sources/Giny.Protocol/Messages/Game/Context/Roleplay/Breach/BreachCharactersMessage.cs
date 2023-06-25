using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachCharactersMessage : NetworkMessage  
    { 
        public  const ushort Id = 253;
        public override ushort MessageId => Id;

        public long[] characters;

        public BreachCharactersMessage()
        {
        }
        public BreachCharactersMessage(long[] characters)
        {
            this.characters = characters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)characters.Length);
            for (uint _i1 = 0;_i1 < characters.Length;_i1++)
            {
                if (characters[_i1] < 0 || characters[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + characters[_i1] + ") on element 1 (starting at 1) of characters.");
                }

                writer.WriteVarLong((long)characters[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            uint _charactersLen = (uint)reader.ReadUShort();
            characters = new long[_charactersLen];
            for (uint _i1 = 0;_i1 < _charactersLen;_i1++)
            {
                _val1 = (double)reader.ReadVarUhLong();
                if (_val1 < 0 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of characters.");
                }

                characters[_i1] = (long)_val1;
            }

        }


    }
}








