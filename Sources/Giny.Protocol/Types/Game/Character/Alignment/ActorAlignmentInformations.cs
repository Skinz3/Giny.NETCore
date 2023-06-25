using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ActorAlignmentInformations  
    { 
        public const ushort Id = 560;
        public virtual ushort TypeId => Id;

        public byte alignmentSide;
        public byte alignmentValue;
        public byte alignmentGrade;
        public double characterPower;

        public ActorAlignmentInformations()
        {
        }
        public ActorAlignmentInformations(byte alignmentSide,byte alignmentValue,byte alignmentGrade,double characterPower)
        {
            this.alignmentSide = alignmentSide;
            this.alignmentValue = alignmentValue;
            this.alignmentGrade = alignmentGrade;
            this.characterPower = characterPower;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)alignmentSide);
            if (alignmentValue < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentValue + ") on element alignmentValue.");
            }

            writer.WriteByte((byte)alignmentValue);
            if (alignmentGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentGrade + ") on element alignmentGrade.");
            }

            writer.WriteByte((byte)alignmentGrade);
            if (characterPower < -9.00719925474099E+15 || characterPower > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterPower + ") on element characterPower.");
            }

            writer.WriteDouble((double)characterPower);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            alignmentSide = (byte)reader.ReadByte();
            alignmentValue = (byte)reader.ReadByte();
            if (alignmentValue < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentValue + ") on element of ActorAlignmentInformations.alignmentValue.");
            }

            alignmentGrade = (byte)reader.ReadByte();
            if (alignmentGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + alignmentGrade + ") on element of ActorAlignmentInformations.alignmentGrade.");
            }

            characterPower = (double)reader.ReadDouble();
            if (characterPower < -9.00719925474099E+15 || characterPower > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + characterPower + ") on element of ActorAlignmentInformations.characterPower.");
            }

        }


    }
}








