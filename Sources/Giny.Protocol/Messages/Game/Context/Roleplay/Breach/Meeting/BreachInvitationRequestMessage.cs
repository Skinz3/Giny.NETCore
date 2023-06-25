using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachInvitationRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3060;
        public override ushort MessageId => Id;

        public long[] guests;

        public BreachInvitationRequestMessage()
        {
        }
        public BreachInvitationRequestMessage(long[] guests)
        {
            this.guests = guests;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)guests.Length);
            for (uint _i1 = 0;_i1 < guests.Length;_i1++)
            {
                if (guests[_i1] < 0 || guests[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + guests[_i1] + ") on element 1 (starting at 1) of guests.");
                }

                writer.WriteVarLong((long)guests[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            uint _guestsLen = (uint)reader.ReadUShort();
            guests = new long[_guestsLen];
            for (uint _i1 = 0;_i1 < _guestsLen;_i1++)
            {
                _val1 = (double)reader.ReadVarUhLong();
                if (_val1 < 0 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of guests.");
                }

                guests[_i1] = (long)_val1;
            }

        }


    }
}








