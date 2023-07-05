using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ChallengeTargetWithAttackerInformation : ChallengeTargetInformation
    {
        public new const ushort Id = 9326;
        public override ushort TypeId => Id;

        public double[] attackersIds;

        public ChallengeTargetWithAttackerInformation()
        {
        }
        public ChallengeTargetWithAttackerInformation(double[] attackersIds, double targetId, short targetCell)
        {
            this.attackersIds = attackersIds;
            this.targetId = targetId;
            this.targetCell = targetCell;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)attackersIds.Length);
            for (uint _i1 = 0; _i1 < attackersIds.Length; _i1++)
            {
                if (attackersIds[_i1] < -9007199254740992 || attackersIds[_i1] > 9007199254740992)
                {
                    throw new System.Exception("Forbidden value (" + attackersIds[_i1] + ") on element 1 (starting at 1) of attackersIds.");
                }

                writer.WriteDouble((double)attackersIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            base.Deserialize(reader);
            uint _attackersIdsLen = (uint)reader.ReadUShort();
            attackersIds = new double[_attackersIdsLen];
            for (uint _i1 = 0; _i1 < _attackersIdsLen; _i1++)
            {
                _val1 = (double)reader.ReadDouble();
                if (_val1 < -9007199254740992 || _val1 > 9007199254740992)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of attackersIds.");
                }

                attackersIds[_i1] = (double)_val1;
            }

        }


    }
}


