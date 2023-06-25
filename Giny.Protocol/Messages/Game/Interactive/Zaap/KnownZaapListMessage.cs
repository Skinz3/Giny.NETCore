using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class KnownZaapListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1272;
        public override ushort MessageId => Id;

        public double[] destinations;

        public KnownZaapListMessage()
        {
        }
        public KnownZaapListMessage(double[] destinations)
        {
            this.destinations = destinations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)destinations.Length);
            for (uint _i1 = 0;_i1 < destinations.Length;_i1++)
            {
                if (destinations[_i1] < 0 || destinations[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + destinations[_i1] + ") on element 1 (starting at 1) of destinations.");
                }

                writer.WriteDouble((double)destinations[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            uint _destinationsLen = (uint)reader.ReadUShort();
            destinations = new double[_destinationsLen];
            for (uint _i1 = 0;_i1 < _destinationsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < 0 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of destinations.");
                }

                destinations[_i1] = (double)_val1;
            }

        }


    }
}








