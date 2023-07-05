using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ChallengeTargetInformation
    {
        public const ushort Id = 3951;
        public virtual ushort TypeId => Id;

        public double targetId;
        public short targetCell;

        public ChallengeTargetInformation()
        {
        }
        public ChallengeTargetInformation(double targetId, short targetCell)
        {
            this.targetId = targetId;
            this.targetCell = targetCell;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (targetCell < -1 || targetCell > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCell + ") on element targetCell.");
            }

            writer.WriteShort((short)targetCell);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            targetId = (double)reader.ReadDouble();
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of ChallengeTargetInformation.targetId.");
            }

            targetCell = (short)reader.ReadShort();
            if (targetCell < -1 || targetCell > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCell + ") on element of ChallengeTargetInformation.targetCell.");
            }

        }


    }
}


