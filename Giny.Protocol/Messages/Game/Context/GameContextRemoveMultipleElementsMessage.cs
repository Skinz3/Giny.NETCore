using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameContextRemoveMultipleElementsMessage : NetworkMessage  
    { 
        public  const ushort Id = 3528;
        public override ushort MessageId => Id;

        public double[] elementsIds;

        public GameContextRemoveMultipleElementsMessage()
        {
        }
        public GameContextRemoveMultipleElementsMessage(double[] elementsIds)
        {
            this.elementsIds = elementsIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)elementsIds.Length);
            for (uint _i1 = 0;_i1 < elementsIds.Length;_i1++)
            {
                if (elementsIds[_i1] < -9.00719925474099E+15 || elementsIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + elementsIds[_i1] + ") on element 1 (starting at 1) of elementsIds.");
                }

                writer.WriteDouble((double)elementsIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            uint _elementsIdsLen = (uint)reader.ReadUShort();
            elementsIds = new double[_elementsIdsLen];
            for (uint _i1 = 0;_i1 < _elementsIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9.00719925474099E+15 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of elementsIds.");
                }

                elementsIds[_i1] = (double)_val1;
            }

        }


    }
}








