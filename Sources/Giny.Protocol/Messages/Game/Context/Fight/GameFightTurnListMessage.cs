using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightTurnListMessage : NetworkMessage  
    { 
        public  const ushort Id = 9689;
        public override ushort MessageId => Id;

        public double[] ids;
        public double[] deadsIds;

        public GameFightTurnListMessage()
        {
        }
        public GameFightTurnListMessage(double[] ids,double[] deadsIds)
        {
            this.ids = ids;
            this.deadsIds = deadsIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ids.Length);
            for (uint _i1 = 0;_i1 < ids.Length;_i1++)
            {
                if (ids[_i1] < -9.00719925474099E+15 || ids[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + ids[_i1] + ") on element 1 (starting at 1) of ids.");
                }

                writer.WriteDouble((double)ids[_i1]);
            }

            writer.WriteShort((short)deadsIds.Length);
            for (uint _i2 = 0;_i2 < deadsIds.Length;_i2++)
            {
                if (deadsIds[_i2] < -9.00719925474099E+15 || deadsIds[_i2] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + deadsIds[_i2] + ") on element 2 (starting at 1) of deadsIds.");
                }

                writer.WriteDouble((double)deadsIds[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            double _val2 = double.NaN;
            uint _idsLen = (uint)reader.ReadUShort();
            ids = new double[_idsLen];
            for (uint _i1 = 0;_i1 < _idsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9.00719925474099E+15 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of ids.");
                }

                ids[_i1] = (double)_val1;
            }

            uint _deadsIdsLen = (uint)reader.ReadUShort();
            deadsIds = new double[_deadsIdsLen];
            for (uint _i2 = 0;_i2 < _deadsIdsLen;_i2++)
            {
                _val2 = (double)reader.ReadDouble();
                if (_val2 < -9.00719925474099E+15 || _val2 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of deadsIds.");
                }

                deadsIds[_i2] = (double)_val2;
            }

        }


    }
}








