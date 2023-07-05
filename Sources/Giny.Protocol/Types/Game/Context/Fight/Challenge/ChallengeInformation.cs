using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ChallengeInformation
    {
        public const ushort Id = 5736;
        public virtual ushort TypeId => Id;

        public int challengeId;
        public ChallengeTargetInformation[] targetsList;
        public int dropBonus;
        public int xpBonus;
        public byte state;

        public ChallengeInformation()
        {
        }
        public ChallengeInformation(int challengeId, ChallengeTargetInformation[] targetsList, int dropBonus, int xpBonus, byte state)
        {
            this.challengeId = challengeId;
            this.targetsList = targetsList;
            this.dropBonus = dropBonus;
            this.xpBonus = xpBonus;
            this.state = state;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element challengeId.");
            }

            writer.WriteVarInt((int)challengeId);
            writer.WriteShort((short)targetsList.Length);
            for (uint _i2 = 0; _i2 < targetsList.Length; _i2++)
            {
                writer.WriteShort((short)(targetsList[_i2] as ChallengeTargetInformation).TypeId);
                (targetsList[_i2] as ChallengeTargetInformation).Serialize(writer);
            }

            if (dropBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonus + ") on element dropBonus.");
            }

            writer.WriteVarInt((int)dropBonus);
            if (xpBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonus + ") on element xpBonus.");
            }

            writer.WriteVarInt((int)xpBonus);
            writer.WriteByte((byte)state);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            ChallengeTargetInformation _item2 = null;
            challengeId = (int)reader.ReadVarUhInt();
            if (challengeId < 0)
            {
                throw new System.Exception("Forbidden value (" + challengeId + ") on element of ChallengeInformation.challengeId.");
            }

            uint _targetsListLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _targetsListLen; _i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<ChallengeTargetInformation>((short)_id2);
                _item2.Deserialize(reader);
                targetsList[_i2] = _item2;
            }

            dropBonus = (int)reader.ReadVarUhInt();
            if (dropBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + dropBonus + ") on element of ChallengeInformation.dropBonus.");
            }

            xpBonus = (int)reader.ReadVarUhInt();
            if (xpBonus < 0)
            {
                throw new System.Exception("Forbidden value (" + xpBonus + ") on element of ChallengeInformation.xpBonus.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of ChallengeInformation.state.");
            }

        }


    }
}


