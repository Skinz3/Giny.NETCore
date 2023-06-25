using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectAveragePricesMessage : NetworkMessage  
    { 
        public  const ushort Id = 2074;
        public override ushort MessageId => Id;

        public short[] ids;
        public long[] avgPrices;

        public ObjectAveragePricesMessage()
        {
        }
        public ObjectAveragePricesMessage(short[] ids,long[] avgPrices)
        {
            this.ids = ids;
            this.avgPrices = avgPrices;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ids.Length);
            for (uint _i1 = 0;_i1 < ids.Length;_i1++)
            {
                if (ids[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ids[_i1] + ") on element 1 (starting at 1) of ids.");
                }

                writer.WriteVarShort((short)ids[_i1]);
            }

            writer.WriteShort((short)avgPrices.Length);
            for (uint _i2 = 0;_i2 < avgPrices.Length;_i2++)
            {
                if (avgPrices[_i2] < 0 || avgPrices[_i2] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + avgPrices[_i2] + ") on element 2 (starting at 1) of avgPrices.");
                }

                writer.WriteVarLong((long)avgPrices[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            double _val2 = double.NaN;
            uint _idsLen = (uint)reader.ReadUShort();
            ids = new short[_idsLen];
            for (uint _i1 = 0;_i1 < _idsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of ids.");
                }

                ids[_i1] = (short)_val1;
            }

            uint _avgPricesLen = (uint)reader.ReadUShort();
            avgPrices = new long[_avgPricesLen];
            for (uint _i2 = 0;_i2 < _avgPricesLen;_i2++)
            {
                _val2 = (double)reader.ReadVarUhLong();
                if (_val2 < 0 || _val2 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of avgPrices.");
                }

                avgPrices[_i2] = (long)_val2;
            }

        }


    }
}








