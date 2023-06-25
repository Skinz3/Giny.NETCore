using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChallengeTargetsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 1506;
        public override ushort MessageId => Id;

        public double[] targetIds;
        public short[] targetCells;

        public ChallengeTargetsListMessage()
        {
        }
        public ChallengeTargetsListMessage(double[] targetIds,short[] targetCells)
        {
            this.targetIds = targetIds;
            this.targetCells = targetCells;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)targetIds.Length);
            for (uint _i1 = 0;_i1 < targetIds.Length;_i1++)
            {
                if (targetIds[_i1] < -9.00719925474099E+15 || targetIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + targetIds[_i1] + ") on element 1 (starting at 1) of targetIds.");
                }

                writer.WriteDouble((double)targetIds[_i1]);
            }

            writer.WriteShort((short)targetCells.Length);
            for (uint _i2 = 0;_i2 < targetCells.Length;_i2++)
            {
                if (targetCells[_i2] < -1 || targetCells[_i2] > 559)
                {
                    throw new System.Exception("Forbidden value (" + targetCells[_i2] + ") on element 2 (starting at 1) of targetCells.");
                }

                writer.WriteShort((short)targetCells[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            int _val2 = 0;
            uint _targetIdsLen = (uint)reader.ReadUShort();
            targetIds = new double[_targetIdsLen];
            for (uint _i1 = 0;_i1 < _targetIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9.00719925474099E+15 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of targetIds.");
                }

                targetIds[_i1] = (double)_val1;
            }

            uint _targetCellsLen = (uint)reader.ReadUShort();
            targetCells = new short[_targetCellsLen];
            for (uint _i2 = 0;_i2 < _targetCellsLen;_i2++)
            {
                _val2 = (int)reader.ReadShort();
                if (_val2 < -1 || _val2 > 559)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of targetCells.");
                }

                targetCells[_i2] = (short)_val2;
            }

        }


    }
}








