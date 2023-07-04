using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class KohScore
    {
        public const ushort Id = 8765;
        public virtual ushort TypeId => Id;

        public byte avaScoreTypeEnum;
        public int roundScores;
        public int cumulScores;

        public KohScore()
        {
        }
        public KohScore(byte avaScoreTypeEnum, int roundScores, int cumulScores)
        {
            this.avaScoreTypeEnum = avaScoreTypeEnum;
            this.roundScores = roundScores;
            this.cumulScores = cumulScores;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)avaScoreTypeEnum);
            writer.WriteInt((int)roundScores);
            writer.WriteInt((int)cumulScores);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            avaScoreTypeEnum = (byte)reader.ReadByte();
            if (avaScoreTypeEnum < 0)
            {
                throw new System.Exception("Forbidden value (" + avaScoreTypeEnum + ") on element of KohScore.avaScoreTypeEnum.");
            }

            roundScores = (int)reader.ReadInt();
            cumulScores = (int)reader.ReadInt();
        }


    }
}


